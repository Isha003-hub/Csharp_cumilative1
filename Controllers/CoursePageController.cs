using Microsoft.AspNetCore.Mvc;
using Assignment__cumilative_1_csharp.Models;

namespace Assignment__cumilative_1_csharp.Controllers
{
    public class CoursePageController : Controller
    {
        // API handles gathering all the data from
        // the Database and MVC is responsible for creating an HTTP response
        // and showing it on a web page that displays the data from database
        // to the View.

        private readonly CourseAPIController _api;

        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        public IActionResult CList()
        {
            List<Course> courses = _api.Lcourses();

            return View(courses);
        }

        public IActionResult CShow(int id)
        {
            Course SelCourse = _api.CourseInfo(id);
            return View(SelCourse);
        }

        // GET : CoursePage/NewCourse
        [HttpGet]
        public IActionResult CNew(int id)
        {
            return View();
        }


        // POST: CoursePage/CreateCourse
        [HttpPost]
        public IActionResult CreateCourse(Course NewCourse)
        {
            int CourseId = _api.AddCourse(NewCourse);

            // redirects to "Show" action on "Course" cotroller with id parameter supplied
            return RedirectToAction("CShow", new { id = CourseId });
        }

        // GET : CoursePage/DeleteConfirmCourse/{id}
        [HttpGet]
        public IActionResult CDeleteConfirm(int id)
        {
            Course SelectedCourse = _api.CourseInfo(id);
            return View(SelectedCourse);
        }


        // POST: CoursePage/DeleteCourse/{id}
        [HttpPost]
        public IActionResult DeleteCourse(int id)
        {
            int CourseId = _api.DeleteCourse(id);
            // redirects to list action
            return RedirectToAction("CList");
        }
    }
}