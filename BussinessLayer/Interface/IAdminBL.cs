using DataBaseLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IAdminBL
    {
        public void AddAdmin(AdminPostModel adminPostModel);
        public string LoginAdmin(string Email, string Password);
    }
}
