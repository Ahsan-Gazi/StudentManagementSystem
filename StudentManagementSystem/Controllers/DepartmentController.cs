using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    [Authorize]
    public class DepartmentController:Controller
    {
        private readonly StudentSemTeacherDepContext _depcon;

        public DepartmentController(StudentSemTeacherDepContext depcon)
        {
            _depcon = depcon;
        }
        public ViewResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DepartmentList()
        {
            var data = _depcon.Departments.ToList();
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [HttpPost]
        public JsonResult AddDepartment([FromBody]Department department)
        {
            _depcon.Departments.Add(department);
            _depcon.SaveChanges();
            var dp = _depcon.Departments.ToList();
            return Json(dp, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [HttpPost]
        public JsonResult UpdateDepartment([FromBody]Department department)
        {
            var updepartment = _depcon.Departments.FirstOrDefault(d => d.DepartmentId == department.DepartmentId);
            updepartment.DepartmentName = department.DepartmentName;
            _depcon.Entry(updepartment).State = EntityState.Modified;
            _depcon.SaveChanges();
            var dpm = _depcon.Departments.ToList();
            return Json(dpm, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public string DeleteDepartment(int? id)
        {
            var data = _depcon.Departments
                .Where(d => d.DepartmentId == id)
                .Select(d => d)
                .FirstOrDefault();
            if (data!=null)
            {
                _depcon.Departments.Remove(data);
               
            }
            _depcon.SaveChanges();
            return "Delete Successful";
        }
    }
}
