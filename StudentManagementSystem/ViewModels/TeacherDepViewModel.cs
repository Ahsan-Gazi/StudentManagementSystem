using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels
{
    public class TeacherDepViewModel
    {
        [Required(ErrorMessage = "Teacher Name is required")]
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "Teacher Email is required")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Image is required")]
        [Display(Name = "Image")]
        public IFormFile TUrlImage { get; set; }
        public int DepartmentId { get; set; }
    }
}
