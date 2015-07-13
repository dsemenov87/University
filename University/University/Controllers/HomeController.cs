using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using University.Models;
using University.Models.DAL;
using Ninject;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public SchoolContext db { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CountOfInstances()
        {
            return Json(new { 
                students = db.Students.Count(),
                instructors = db.Courses.Count()
            });
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}