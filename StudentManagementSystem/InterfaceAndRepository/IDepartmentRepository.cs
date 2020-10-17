﻿using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.InterfaceAndRepository
{
   public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
    }
}
