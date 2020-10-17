using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.InterfaceAndRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentSemTeacherDepContext _StContext;

        public StudentRepository(StudentSemTeacherDepContext StContext)
        {
            _StContext = StContext;
        }
        public Student AddStudent(Student objStudent)
        {
            _StContext.Students.Add(objStudent);
            _StContext.SaveChanges();
            return objStudent;
        }

        public void DeleteStudent(int id)
        {
            var data = GetStudentById(id);
            if (data!=null)
            {
                _StContext.Remove(data);
            }
            _StContext.SaveChanges();
        }

        public Student Details(int? id)
        {
            return _StContext.Students
                .Include(st => st.Semester).FirstOrDefault();
        }

        public IEnumerable<Semester> GetSemesters()
        {
            var data = _StContext.Semesters.ToList();
            return data;
        }

        public Student GetStudentById(int id)
        {
            Student student = _StContext.Students.FirstOrDefault(st => st.StudentId == id);
            return student;
        }

        public IEnumerable<Student> GetStudents()
        {
            var data = _StContext.Students.Select(st => new Student
            {
                StudentId=st.StudentId,
                StudentName=st.StudentName,
                Gender=st.Gender,
                SemesterId=st.SemesterId,
                UrlImage=st.UrlImage,
                Semester=_StContext.Semesters.Where(sm=>sm.SemesterId==st.SemesterId).FirstOrDefault()
            }).ToList();
            return data;
        }

        public Student UpdateStudent(Student changeStudent)
        {
            var student = _StContext.Students.Attach(changeStudent);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _StContext.SaveChanges();
            return changeStudent;
        }
    }
}
