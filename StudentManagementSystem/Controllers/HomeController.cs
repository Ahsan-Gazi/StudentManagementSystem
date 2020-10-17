using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StudentManagementSystem.Controllers
{
    [AllowAnonymous]
    public class HomeController:Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
