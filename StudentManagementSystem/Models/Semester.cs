using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class Semester
    {
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
