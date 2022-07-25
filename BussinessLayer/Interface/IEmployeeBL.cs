using DataBaseLayer.Employee;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IEmployeeBL
    {
        Task AddEmployee(EmployeePostModel EmployeePostModel);
        Task UpdateEmployee( int EmployeeId, UpdateModel updateModel);
        Task DeleteEmployee(int EmployeeId);
        Task<Employee> GetEmployee(int EmployeeId);
        Task<List<Employee>> GetAllEmployee();
        public string LoginEmployee(string Email, string Password);
    }
}
