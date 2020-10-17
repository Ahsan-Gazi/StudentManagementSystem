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
    public class SemesterController:Controller
    {
        private readonly StudentSemTeacherDepContext _SemList;

        public SemesterController(StudentSemTeacherDepContext SemList)
        {
            _SemList = SemList;
        }
        public ViewResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult SemesterList()
        {
            var data = _SemList.Semesters.ToList();
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());

        }
        [HttpPost]
        public JsonResult AddSemester([FromBody]Semester semester)
        {
            _SemList.Semesters.Add(semester);
            _SemList.SaveChanges();
            var sm = _SemList.Semesters.ToList();
            return Json(sm, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public JsonResult UpdateSemester([FromBody]Semester semester)
        {
            var upsemester = _SemList.Semesters.FirstOrDefault(x=>x.SemesterId == semester.SemesterId);
            upsemester.SemesterName = semester.SemesterName;
            _SemList.Entry(upsemester).State = EntityState.Modified;
            _SemList.SaveChanges();
            var sm = _SemList.Semesters.ToList();
            return Json(sm, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public string DeleteSemester(int? id)
        {
            var data = _SemList.Semesters.Where(x => x.SemesterId == id).Select(x => x).FirstOrDefault();
            if (data!=null)
            {
                _SemList.Semesters.Remove(data);
            }
            _SemList.SaveChanges();
            return "Delete successful";
        }
    }
}
