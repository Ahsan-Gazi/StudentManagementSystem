using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Student Name is required")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public string UrlImage { get; set; }
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
    }
}
