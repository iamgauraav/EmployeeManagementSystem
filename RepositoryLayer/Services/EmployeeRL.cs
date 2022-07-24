using DataBaseLayer.Employee;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class EmployeeRL : IEmployeeRL
    {
        EmployeeManagementContext employeeManagementContext;
        IConfiguration configuration;

        public EmployeeRL(EmployeeManagementContext employeeManagementContext, IConfiguration configuration)
        {
            this.employeeManagementContext = employeeManagementContext;
            this.configuration = configuration;

        }

        public async Task AddEmployee( EmployeePostModel EmployeePostModel)
        {
            try
            {
                Employee employee = new Employee();
                employee.Name = EmployeePostModel.Name;
                employee.FatherName = EmployeePostModel.FatherName;
                employee.DateOfBirth = EmployeePostModel.DateOfBirth;
                employee.DateOfJoining = EmployeePostModel.DateOfJoining;
                employee.Designation= EmployeePostModel.Designation;
                employee.CommunicationAddress = EmployeePostModel.CommunicationAddress;
                employee.ParmanentAddress = EmployeePostModel.ParmanentAddress;
                employee.ContactNumber = EmployeePostModel.ContactNumber;
                employee.EmailId = EmployeePostModel.EmailId;
                employee.Salary = EmployeePostModel.Salary;
                employee.Email = EmployeePostModel.Email;
                employee.Password= EmployeePostModel.Password;
                employeeManagementContext.Add(employee);
                await employeeManagementContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteEmployee(int EmployeeId)
        {
            try
            {
                var employee = employeeManagementContext.Employees.FirstOrDefault(u => u.EmployeeId == EmployeeId);
                if (employee != null)
                {
                    employeeManagementContext.Employees.Remove(employee);
                    await employeeManagementContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            };
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            try
            {
                List<Employee> result = new List<Employee>();

                result = await employeeManagementContext.Employees.Where(u => u.EmployeeId == u.EmployeeId).ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employee> GetEmployee(int EmployeeId)
        {
            try
            {
                return await employeeManagementContext.Employees.FirstOrDefaultAsync(u => u.EmployeeId == EmployeeId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string LoginEmployee(string Email, string Password)
        {
            try
            {
                var user = employeeManagementContext.Employees.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if (user != null)
                {
                    return GenerateSecurityToken(Email, user.EmployeeId);
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateSecurityToken(string emailID, int userId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("UserId", userId.ToString())
            };
            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return  new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task UpdateEmployee( int EmployeeId , UpdateModel updateModel)
        {
            try
            {
                var employee = employeeManagementContext.Employees.FirstOrDefault(u =>  u.EmployeeId == EmployeeId);
                if (employee != null)
                {
                    employee.Name = updateModel.Name;
                    employee.FatherName= updateModel.FatherName;
                    employee.DateOfBirth= updateModel.DateOfBirth;
                    employee.DateOfJoining = updateModel.DateOfJoining;
                    employee.Designation = updateModel.Designation;
                    employee.CommunicationAddress= updateModel.CommunicationAddress;
                    employee.ParmanentAddress= updateModel.ParmanentAddress;
                    employee.ContactNumber= updateModel.ContactNumber;
                    employee.EmailId= updateModel.EmailId;
                    employee.Salary= updateModel.Salary;
                    await employeeManagementContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
   
}
