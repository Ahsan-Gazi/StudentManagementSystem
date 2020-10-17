using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.InterfaceAndRepository
{
   public interface ITeacherRepository
    {
        IEnumerable<Teacher> GetTeachers();
        Teacher AddTeacher(Teacher objTeacher);
        Teacher GetTeacherById(int id);
        Teacher updateTeacher(Teacher changeTeacher);
        void DeleteTeacher(int id);
        IEnumerable<Department> GetDepartments();
        Teacher Details(int? id);
    }
}
