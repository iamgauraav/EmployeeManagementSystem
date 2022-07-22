using BussinessLayer.Interface;
using DataBaseLayer.Users;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        IAdminRL adminRL;
        public AdminBL(IAdminRL adminRl)
        {
            this.adminRL = adminRl;
        }

        public void AddAdmin(AdminPostModel adminPostModel)
        {
            try
            {
                this.adminRL.AddAdmin(adminPostModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ForgetPassword(string Emails)
        {
            try
            {
                return this.adminRL.ForgetPassword(Emails);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string LoginAdmin(string Email, string Password)
        {
            try
            {
                return this.adminRL.LoginAdmin(Email, Password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
