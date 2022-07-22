using DataBaseLayer.Users;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        EmployeeManagementContext employeeManagementContext;
        IConfiguration configuration;

        public AdminRL(EmployeeManagementContext employeeManagementContext, IConfiguration configuration)
        {
            this.employeeManagementContext = employeeManagementContext;
            this.configuration = configuration;
        }

        public void AddAdmin(AdminPostModel adminPostModel)
        {
            try
            {
                Admin admin = new Admin();
                admin.FirstName = adminPostModel.FirstName;
                admin.LastName = adminPostModel.LastName;
                admin.Email = adminPostModel.Email;
                admin.Password = adminPostModel.Password;
                employeeManagementContext.Add(admin);
                employeeManagementContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ForgetPassword(string Email)
        {
            try
            {
                var admin = employeeManagementContext.Admin.FirstOrDefault(u => u.Email == Email);
                if (admin == null)
                {
                    return false;
                }
                MessageQueue messageQueue = new MessageQueue();
                messageQueue.Path = @".\private$\EmployeeManagementQueue";
                //ADD MESSAGE TO QUEUE
                if (MessageQueue.Exists(messageQueue.Path))
                {
                    messageQueue = new MessageQueue(messageQueue.Path);

                }
                else
                {
                    messageQueue = MessageQueue.Create(@".\Private$\EmployeeManagementQueue");
                }
                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = GenerateSecurityToken(Email, admin.AdminId);
                MyMessage.Label = "Forget Password Label";
                messageQueue.Send(MyMessage);
                Message msg = messageQueue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(Email, msg.Body.ToString());
                messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                messageQueue.BeginReceive();
                messageQueue.Close();

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {

                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }


        public string GenerateToken(string Email)
        {
            try
            {
                var admin = employeeManagementContext.Admin.FirstOrDefault(u => u.Email == Email);
                if (admin == null)
                {
                    return null;
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim("Email", Email),

               }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials =
               new SigningCredentials(
                   new SymmetricSecurityKey(tokenKey),
                   SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }


            catch (Exception)
            {

                throw;
            }
        }

        public string LoginAdmin(string Email, string Password)
        {
            try
            {
                var user = employeeManagementContext.Admin.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if (user != null)
                {
                    return GenerateSecurityToken(Email, user.AdminId);
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GenerateSecurityToken(string emailID, int AdminId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("AdminId", AdminId.ToString())
            };
            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
