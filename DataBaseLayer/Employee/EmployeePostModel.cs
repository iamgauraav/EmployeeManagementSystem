using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.Employee
{
    public class EmployeePostModel
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfJoining { get; set; }
        public string Designation { get; set; }
        public string CommunicationAddress { get; set; }
        public string ParmanentAddress { get; set; }
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }
        public int Salary { get; set; }



    }
}
