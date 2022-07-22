using DataBaseLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAdminRL
    {
        public void AddAdmin(AdminPostModel adminPostModel);
        
        public string LoginAdmin(string Email, string Password);

        public bool ForgetPassword(string Emails);
    }
}
