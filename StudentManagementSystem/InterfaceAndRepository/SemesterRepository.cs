using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.InterfaceAndRepository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly StudentSemTeacherDepContext _SmContext;

        public SemesterRepository(StudentSemTeacherDepContext SmContext)
        {
            _SmContext = SmContext;
        }
        public IEnumerable<Semester> GetSemesters()
        {
            return _SmContext.Semesters;
        }
    }
}
