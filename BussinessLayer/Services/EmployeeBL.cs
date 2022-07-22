﻿using BussinessLayer.Interface;
using DataBaseLayer.Employee;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class EmployeeBL : IEmployeeBL
    {
        IEmployeeRL employeeRL;
        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }

        public async Task AddEmployee(EmployeePostModel EmployeePostModel)
        {
            try
            {
                await this.employeeRL.AddEmployee( EmployeePostModel);
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
                await this.employeeRL.DeleteEmployee( EmployeeId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateEmployee(int EmployeeId,UpdateModel updateModel)
        {
            try
            {
                await employeeRL?.UpdateEmployee( EmployeeId, updateModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}