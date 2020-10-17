using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels
{
    public class StudentSmViewModel
    {
        [Required(ErrorMessage ="Student Name is required")]
        [Display(Name ="Student Name")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Student Gender is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Student Image is required")]
        [Display(Name = "Image")]
        public IFormFile UrlImage { get; set; }
        public int SemesterId { get; set; }
    }
}
