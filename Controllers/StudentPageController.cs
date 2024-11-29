using Microsoft.AspNetCore.Mvc;
using Assignment__cumilative_1_csharp.Models;
using Assignment__cumilative_1_csharp.Controllers;


namespace Assignment__cumilative_1_csharp.Controllers
{
    public class StudentPageController : Controller
    {

        // API handles gathering all the data from
        // the Database and MVC is responsible for creating an HTTP response
        // and showing it on a web page that displays the data from database
        // to the View.

        private readonly StudentAPIController _api;

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }

        public IActionResult SList()
        {
            List<Student> students = _api.LStudents();

            return View(students);
        }

        public IActionResult SShow(int id)
        {
            Student SelStudents = _api.StudentInfo(id);
            return View(SelStudents);
        }

        // GET : StudentPage/SNew
        [HttpGet]
        public IActionResult SNew(int id)
        {
            return View();
        }



        // POST: StudentPage/CreateStudent
        [HttpPost]
        public IActionResult CreateStudent(Student SNew)
        {
            int StudentId = _api.AddStudent(SNew);

            // redirects to "Show" action on "Student" cotroller with id parameter supplied
            return RedirectToAction("SShow", new { id = StudentId });
        }




        // GET : StudentPage/SDeleteConfirmcshtml/{id}
        [HttpGet]
        public IActionResult SDeleteConfirmcshtml(int id)
        {
            Student SelectedStudent = _api.StudentInfo(id);
            return View(SelectedStudent);
        }


        // POST: StudentPage/DeleteStudent/{id}
        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            int StudentId = _api.DeleteStudent(id);
            // redirects to list action
            return RedirectToAction("SList");
        }
    }
}