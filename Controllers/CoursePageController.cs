using Microsoft.AspNetCore.Mvc;
using Assignment__cumilative_1_csharp.Models;

namespace Assignment__cumilative_1_csharp.Controllers
{
    public class CoursePageController : Controller
    {
        // The API retrieves all necessary data from the database, 
        // while the MVC architecture ensures the data is processed 
        // and sent as an HTTP response. The response is rendered 
        // on a web page, presenting the database information in the View.

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

        // GET : CoursesPage/CEdit/{id}
        [HttpGet]
        public IActionResult CEdit(int id)
        {
            Course SelectedCourse = _api.CourseInfo(id);
            return View(SelectedCourse);
        }



        // POST: CoursesPage/Update/{id}
        [HttpPost]
        public IActionResult Update(int id, string Course_Code, DateTime C_Start_Date, DateTime C_End_Date, string Course_Name, int Teacher_Id)
        {
            Course UpdatedCourse = new Course();
            UpdatedCourse.Course_Code = Course_Code;
            UpdatedCourse.C_Start_Date = C_Start_Date;
            UpdatedCourse.C_End_Date = C_End_Date;
            UpdatedCourse.Course_Name = Course_Name;
            UpdatedCourse.Teacher_Id = Teacher_Id;


            // not doing anything with the response
            _api.UpdateCourse(id, UpdatedCourse);

            // redirects to show teacher
            return RedirectToAction("CShow", new { id });
        }
    }
}