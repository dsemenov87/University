using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;

using University.Models;
using University.Models.DAL;
using Ninject;
using PagedList;

namespace University.Areas.Dean.Controllers
{
    public class PagingData
    {
        public int PageNumber { get; set; }
        public int TotalPagesCount { get; set; }
    }

    public class ListResult
    {
        public IEnumerable<Course> Data { get; set; }
        public PagingData Paging { get; set; }
    }
    
    public class CoursesController : Controller
    {
        [Inject]
        public SchoolContext db { get; set; }

        /*[HttpGet]
        public ActionResult Index()
        {
            return View();  
        }*/

        [HttpPost]
        public ActionResult List(int pageNumber = 1)
        {
            var data = new
            {
                Data = db.Courses
                .Select(i => new 
                { 
                    FirstName = i.InstructorFirstName,
                    LastName = i.InstructorLastName,
                    Course = i.CourseName
                })
                .ToList(),
                Paging = new PagingData
                {
                    PageNumber = pageNumber,
                    TotalPagesCount = db.Courses.Count() / 3 + 1
                }
            };

            return Json(data);
        }

        // GET: Courses
        [HttpGet]
        public ActionResult Index(string currentFilter, string searchString, int? page, string sortOrder = "instructor")
        {
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }

            searchString = searchString ?? currentFilter;
            ViewBag.CurrentFilter = searchString;

            ViewBag.InstructorSortParm = "instructor";
            ViewBag.CourseNameSortParm = "CourseName"; 

            var courses = db.Courses
                .Where(c =>
                    String.IsNullOrEmpty(searchString)
                        || c.CourseName.Contains(searchString)
                        || c.InstructorFirstName.Contains(searchString)
                );

            if (sortOrder == "instructor")
            {
                ViewBag.InstructorSortParm = "instructor_desc";
                courses = courses.OrderBy(c => c.InstructorFirstName);
            }
            else if (sortOrder == "CourseName")
            {
                ViewBag.CourseNameSortParm = "CourseName_desc";
                courses = courses.OrderBy(c => c.CourseName);
            }
            else if (sortOrder == "instructor_desc")
            {
                courses = courses.OrderByDescending(c => c.InstructorFirstName);
            }
            else if (sortOrder == "CourseName_desc")
            {
                courses = courses.OrderByDescending(c => c.CourseName);
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(courses.ToPagedList(pageNumber, pageSize));
        }

        // GET: Courses/CreateOrUpdate/5
        //[Authorize(Roles = "dean")]
        [HttpGet]
        public ActionResult CreateOrUpdate(int? id)
        {
            Course model = null;
            if (id != null)
            {
                model = db.Courses.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Title = "Edit instructor";
            }
            else
            {
                ViewBag.Title = "Create instructor";
            }
            
            return PartialView("CreateOrUpdate", model ?? new Course());
        }

        // POST: Courses/CreateOrUpdate/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "dean")]
        public ActionResult CreateOrUpdate([Bind(Include = "CourseID,CourseName,InstructorLastName,InstructorFirstName")] Course model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = model.CourseID == 0;
                    if (isNew) {
                        db.Courses.Add(model);
                    }
                    else {
                        db.Entry(model).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    return Json(new
                    {
                        isValid = true,
                        id = model.CourseID,
                        lastName = model.InstructorLastName,
                        firstName = model.InstructorFirstName,
                        courseName = model.CourseName,
                        isNew = isNew
                    });
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return Json(new Object() /*TO DO*/);
        }

        // GET: Courses/Delete/5
        //[Authorize(Roles = "dean")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course model = db.Courses.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", model);
            
        }

        // POST: Courses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "dean")]
        public ActionResult Delete(Course model)
        {
            try
            {
                Course course = db.Courses.Find(model.CourseID);
                db.Courses.Remove(course);
                db.SaveChanges();
                return Json(new
                {
                    isValid = true,
                    id = model.CourseID
                });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return Json(new Object() /*TO DO*/);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
