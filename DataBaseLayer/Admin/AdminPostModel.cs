using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.Users
{
    public class AdminPostModel
    {
        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{3,}$", ErrorMessage ="Name start with Cap and has minimum 3 characters")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{3,}$", ErrorMessage = "Name start with Cap and has minimum 3 characters")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
