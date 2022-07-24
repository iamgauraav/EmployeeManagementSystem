using BussinessLayer.Interface;
using DataBaseLayer.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        EmployeeManagementContext employeeManagementContext;
        IEmployeeBL employeeBL;

        public EmployeeController(EmployeeManagementContext employeeManagementContext, IEmployeeBL employeeBL)
        {
            this.employeeManagementContext = employeeManagementContext;
            this.employeeBL = employeeBL;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddEmployee")]
        public async Task<ActionResult> AddEmployee(EmployeePostModel employeePostModel)
        {
            try
            {
                
                await this.employeeBL.AddEmployee(employeePostModel);
                return this.Ok(new { success = true, message = $"Employee Added Successful" });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateEmployee/{EmployeeId}")]
        public async Task<ActionResult> UpdateEmployee(int EmployeeId, UpdateModel updateModel)
        {

            try
            {

                await this.employeeBL.UpdateEmployee( EmployeeId, updateModel);
                return this.Ok(new { success = true, message = $"Employee Added Successful" });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("Delete/{EmployeeId}")]
        public async Task<ActionResult> RemoveEmployee(int EmployeeId)
        {
            try
            {
                var employee = employeeManagementContext.Employees.FirstOrDefault(x => x.EmployeeId == EmployeeId);
                if (employee == null)
                {
                    return this.BadRequest(new { sucess = false, message = "Deletion Failed" });

                }
                await this.employeeBL.DeleteEmployee(EmployeeId);
                return this.Ok(new { succes = true, message = "Employee Deleted Successfully" });
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetEmployee")]
        public async Task<ActionResult> GetEmployee()
        {
           
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                Employee employee = await this.employeeBL.GetEmployee(UserId);
                return this.Ok(new { success = true, message = "Required note is:", data = employee });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetAllEmployee")]
        public async Task<ActionResult> GetAllEmployees()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("AdminId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                List<Employee> employee = await this.employeeBL.GetAllEmployee();
                return this.Ok(new { success = true, message="Required note is:", data = employee });
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        [HttpPost("LoginEmployee")]
        public ActionResult LoginEmployee(string Email, string Password)
        {
            try
            {
                var user = employeeManagementContext.Employees.FirstOrDefault(u => u.Email == Email);

                if (user == null)
                {
                    return this.BadRequest(new { success = false, message = "Email does not Exists " });
                }
                var user1 = employeeManagementContext.Employees.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if (user1 == null)
                {
                    return this.BadRequest(new { success = false, message = "Password is Invalid " });
                }
                string token = this.employeeBL.LoginEmployee(Email, Password);
                return this.Ok(new { success = true, message = "Login Sucessfull", token = token });
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
