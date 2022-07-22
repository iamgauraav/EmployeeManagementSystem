using DataBaseLayer.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IEmployeeRL
    {
       Task AddEmployee( EmployeePostModel EmployeePostModel); 
       Task UpdateEmployee( int EmployeeId, UpdateModel updateModel);

        Task DeleteEmployee( int EmployeeId );
    }
}
