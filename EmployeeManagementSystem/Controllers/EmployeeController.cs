using BussinessLayer.Interface;
using DataBaseLayer.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
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

    }
}
