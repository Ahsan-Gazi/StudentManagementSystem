using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.InterfaceAndRepository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly StudentSemTeacherDepContext _TeachCon;

        public TeacherRepository(StudentSemTeacherDepContext TeachCon)
        {
            _TeachCon = TeachCon;

        }
        public Teacher AddTeacher(Teacher objTeacher)
        {
            _TeachCon.Teachers.Add(objTeacher);
            _TeachCon.SaveChanges();
            return objTeacher;
        }

        public void DeleteTeacher(int id)
        {
            var data = GetTeacherById(id);
            if (data!=null)
            {
                _TeachCon.Remove(data);
            }
            _TeachCon.SaveChanges();
        }

        public Teacher Details(int? id)
        {
            return _TeachCon.Teachers
                .Include(tr => tr.Department).FirstOrDefault();
        }

        public IEnumerable<Department> GetDepartments()
        {
            var data = _TeachCon.Departments.ToList();
            return data;
        }

        public Teacher GetTeacherById(int id)
        {
            Teacher teacher = _TeachCon.Teachers.FirstOrDefault(t => t.TeacherId == id);
            return teacher;
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            var data = _TeachCon.Teachers.Select(t => new Teacher
            {
                TeacherId=t.TeacherId,
                TeacherName=t.TeacherName,
                Email=t.Email,
                TUrlImage=t.TUrlImage,
                DepartmentId=t.DepartmentId,
                Department=_TeachCon.Departments.Where(d=>d.DepartmentId==t.DepartmentId).FirstOrDefault()
            }).ToList();
            return data;
        }

        public Teacher updateTeacher(Teacher changeTeacher)
        {
            var teacher = _TeachCon.Teachers.Attach(changeTeacher);
            teacher.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return changeTeacher;
        }
    }
}
