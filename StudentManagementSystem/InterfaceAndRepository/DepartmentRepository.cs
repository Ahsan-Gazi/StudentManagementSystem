using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.InterfaceAndRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly StudentSemTeacherDepContext _depContext;

        public DepartmentRepository(StudentSemTeacherDepContext depContext)
        {
            _depContext = depContext;
        }
        public IEnumerable<Department> GetDepartments()
        {
            return _depContext.Departments;
        }
    }
}
