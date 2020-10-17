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
    public class StudentController:Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StudentController(IStudentRepository studentRepository,IHostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public IActionResult Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
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
            var data = _studentRepository.GetStudents();
            //var sts = from stt in data select stt;
            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(st => st.StudentName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderBy(st => st.StudentName);
                    break;
                default:
                    data = data.OrderByDescending(st => st.StudentName);
                    break;
            }
            int pageSize = 10;
            return View(PaginatedList<Student>.Create(data.AsQueryable<Student>(), pageNumber??1, pageSize));
        }
        [HttpGet]
        
        public IActionResult Create()
        {
            SemesterDDL();
            return View();
        }
        [HttpPost]
       
        public IActionResult Create(StudentSmViewModel stModel)
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
                var data = new Student()
                {
                    StudentName = stModel.StudentName,
                    Gender = stModel.Gender,
                    SemesterId = stModel.SemesterId,
                    UrlImage = urlImage
                };
                _studentRepository.AddStudent(data);
                return RedirectToAction("Index");
            }

            SemesterDDL();
            return View();
        }
        [HttpGet]
        
        public IActionResult Edit(int id)
        {
            var student = _studentRepository.GetStudentById(id);
            SemesterDDL();
            return View(student);
        }
        [HttpPost]
        
        public IActionResult Edit(int id,Student changeStudent)
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
                var data = _studentRepository.GetStudentById(id);
                data.StudentName = changeStudent.StudentName;
                data.Gender = changeStudent.Gender;
                data.SemesterId = changeStudent.SemesterId;
                if (data.UrlImage!=null)
                {
                    string fp = Path.Combine(_hostingEnvironment.WebRootPath, "Image", data.UrlImage);
                    System.IO.File.Delete(fp);
                }
                data.UrlImage = urlImage;
                _studentRepository.UpdateStudent(data);
                return RedirectToAction("Index");
            }
            SemesterDDL();
            return View();
        }
      
        private void SemesterDDL(object semeseterSelect=null)
        {
            var semesters = _studentRepository.GetSemesters();
            ViewBag.ListOfSemester = new SelectList(semesters, "SemesterId", "SemesterName", semeseterSelect);
        }
        public IActionResult Delete(int id)
        {
            _studentRepository.DeleteStudent(id);
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult Details(int?id)
        {
            Student data = _studentRepository.Details(id);
            if (data==null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
    }
}
