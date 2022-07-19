using DataBaseLayer.Users;
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
                admin.city = adminPostModel.city;
                employeeManagementContext.Add(admin);
                employeeManagementContext.SaveChanges();
                

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
                    return GenerateJWToken(Email, user.AdminId);
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        private string GenerateJWToken(string Email, int userId)
        {
            var user = employeeManagementContext.Admin.FirstOrDefault(u => u.Email == Email);
            if (user == null)
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
                    new Claim("userId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
