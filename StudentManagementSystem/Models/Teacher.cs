using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Email { get; set; }
        public string TUrlImage { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
