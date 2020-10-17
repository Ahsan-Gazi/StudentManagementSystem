using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.InterfaceAndRepository;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    [Authorize]
    public class TeacherController:Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TeacherController(ITeacherRepository teacherRepository,IHostingEnvironment hostingEnvironment)
        {
            _teacherRepository = teacherRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public ActionResult Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString!=null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var data = _teacherRepository.GetTeachers();
            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(t => t.TeacherName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(t => t.TeacherName);
                    break;
                default:
                    data = (data.OrderBy(t => t.TeacherName));
                    break;
            }
            int pageSize = 3;
            return View(PaginatedList<Teacher>.Create(data.AsQueryable<Teacher>(), pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            DepartmentDDL();
            return View();
        }
        [HttpPost]
        public ActionResult Create(TeacherDepViewModel teacherModel)
        {
            if (ModelState.IsValid)
            {
                string urlImage = "";
                var files = HttpContext.Request.Form.Files;
                foreach (var image in files)
                {
                    if (image!=null && image.Length>0)
                    {
                        var file = image;
                        var uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "Image");
                        if (file.Length>0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("_", "") + file.FileName;
                            using (var fileStream=new FileStream(Path.Combine(uploadFile,fileName),FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                                urlImage = fileName;
                            }
                        }
                    }
                }
                var data = new Teacher()
                {
                    TeacherName = teacherModel.TeacherName,
                    Email = teacherModel.Email,
                    DepartmentId = teacherModel.DepartmentId,
                    TUrlImage = urlImage
                };
                _teacherRepository.AddTeacher(data);
                return RedirectToAction("Index");
            }
            DepartmentDDL();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var teacher = _teacherRepository.GetTeacherById(id);
            DepartmentDDL();
            return View(teacher);
        }
        [HttpPost]
        public ActionResult Edit(int id,Teacher changeTeacher)
        {
            if (ModelState.IsValid)
            {
                string urlImage = "";
                var files = HttpContext.Request.Form.Files;
                foreach (var image in files)
                {
                    if (image!=null && image.Length>0)
                    {
                        var file = image;
                        var uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "Image");
                        if (file.Length>0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("_", "") + file.FileName;
                            using (var fileStream=new FileStream(Path.Combine(uploadFile,fileName),FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                                urlImage = fileName;
                            }
                        }
                    }
                }
                var data = _teacherRepository.GetTeacherById(id);
                data.TeacherName = changeTeacher.TeacherName;
                data.Email = changeTeacher.Email;
                data.DepartmentId = changeTeacher.DepartmentId;
                if (data.TUrlImage!=null)
                {
                    string fp = Path.Combine(_hostingEnvironment.WebRootPath, "Image", data.TUrlImage);
                    System.IO.File.Delete(fp);
                }
                data.TUrlImage = urlImage;
                _teacherRepository.updateTeacher(data);
                return RedirectToAction("Index");
            }
            DepartmentDDL();
            return View();
        }

        private void DepartmentDDL(object departmentSelect=null)
        {
            var depertments = _teacherRepository.GetDepartments();
            ViewBag.ListOfDepartment = new SelectList(depertments, "DepartmentId", "DepartmentName", departmentSelect);
        }
        public ActionResult Delete(int id)
        {
            _teacherRepository.DeleteTeacher(id);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {
            Teacher data = _teacherRepository.Details(id);
            if (data==null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
    }
}
