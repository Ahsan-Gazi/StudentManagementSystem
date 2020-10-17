using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse",controller:"Account")]
        [ValidEmailDomain(allowedDomain:"yahoo.com",ErrorMessage = "Email domain must be yahoo.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and ConfirmPassword are not matched.")]
        public string ConfirmPassword { get; set; }

        public string Gender { get; set; }
    }
}
