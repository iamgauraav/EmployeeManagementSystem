using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.Users
{
    public class AdminPostModel
    {

        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{3,}$", ErrorMessage = "name starts with Cap and has minimum 3 characters")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{3,}$", ErrorMessage = "name starts with Cap and has minimum 3 characters")]
        public string LastName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "Please Enter a Valid Password")]
        public string Password { get; set; }
        public string city { get; set; }

    }
}
