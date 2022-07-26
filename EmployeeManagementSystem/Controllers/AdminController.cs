﻿using BussinessLayer.Interface;
using DataBaseLayer.Users;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Linq;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        EmployeeManagementContext employeeManagementContext;
        IAdminBL adminBL;

        public AdminController(EmployeeManagementContext employeeManagementContext, IAdminBL adminBL)
        {
            this.employeeManagementContext = employeeManagementContext;
            this.adminBL = adminBL;
        }
        [HttpPost("Register")]
        public IActionResult AddAdmin(AdminPostModel admin)
        {
            try
            {
                this.adminBL.AddAdmin(admin);
                return this.Ok(new { success = true, message="Registration Successfull" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost("Login/{Email}/{Password}")]
        public ActionResult LoginAdmin(string Email, string Password)
        {
            try
            {
                var user = employeeManagementContext.Admin.FirstOrDefault(u => u.Email == Email);

                if (user == null)
                {
                    return this.BadRequest(new { success = false, message = "Email does not Exists " });
                }
                var user1 = employeeManagementContext.Admin.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if (user1 == null)
                {
                    return this.BadRequest(new { success = false, message = "Password is Invalid " });
                }
                string token = this.adminBL.LoginAdmin(Email, Password);
                return this.Ok(new { success = true, message = "Login Sucessfull", token = token });
            }
            catch (Exception )
            {

                throw ;
            }
        }
        [HttpPost("ForgetPassword/{Email}")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var admin = employeeManagementContext.Admin.FirstOrDefault(u => u.Email == Email);
                if (admin == null)
                {
                    return this.BadRequest(new { success = false, message = "Email does not Exists" });

                }
                bool result = this.adminBL.ForgetPassword(Email);
                return this.Ok(new { success = true, message = "Email has sent" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
