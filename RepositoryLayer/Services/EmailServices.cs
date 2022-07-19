using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendEmail(string Email, string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("apitestdemo123@gmail.com", "izddfmglbeoesvxp");


                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(Email);
                msgObj.From = new MailAddress("apitestdemo123@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = $"www.EmployeeManagementSystem.com/reset-password/{token}";
                client.Send(msgObj);
            }
        }
    }
}
