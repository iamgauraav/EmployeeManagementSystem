using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
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
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
}
