using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.InterfaceAndRepository
{
   public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student AddStudent(Student objStudent);
        Student GetStudentById(int id);
        Student UpdateStudent(Student changeStudent);
        void DeleteStudent(int id);
        IEnumerable<Semester> GetSemesters();
        Student Details(int? id);
    }
}
