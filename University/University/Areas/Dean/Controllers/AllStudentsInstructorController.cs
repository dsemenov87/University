using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ninject;
using University.Models.DAL;
using University.ViewModels;

namespace University.Areas.Dean.Controllers 
{
    public class AllStudentsInstructorController : Controller
    {
        [Inject]
        public SchoolContext Db { get; set; }
        
        // GET: AllStudentsInstructor
         [HttpGet]
        public ActionResult Index(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(int pageNumber = 1)
        {
            var instructors
                = from course in Db.Courses
                  where course.Enrollments.Count() == Db.Students.Count()
                  select new AllStudentInstructors() { Instructor = string.Concat(course.InstructorFirstName, course.InstructorLastName) };

            return Json(instructors.ToList());
        }
    }
}