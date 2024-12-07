using Microsoft.AspNetCore.Mvc;
using Assignment__cumilative_1_csharp.Models;
using Assignment__cumilative_1_csharp.Controllers;


namespace Assignment__cumilative_1_csharp.Controllers
{
    public class StudentPageController : Controller
    {

        // The API retrieves all necessary data from the database, 
        // while the MVC architecture ensures the data is processed 
        // and sent as an HTTP response. The response is rendered 
        // on a web page, presenting the database information in the View.

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

        // GET : StudentPage/SEdit/{id}
        [HttpGet]
        public IActionResult SEdit(int id)
        {
            Student SelectedStudent = _api.StudentInfo(id);
            return View(SelectedStudent);
        }



        // POST: StudentPage/Update/{id}
        [HttpPost]
        public IActionResult Update(int id, string First_Name, string Last_Name, string Student_Num, DateTime S_Enroll_Date)
        {
            Student UpdatedStudent = new Student();
            UpdatedStudent.First_Name = First_Name;
            UpdatedStudent.Last_Name = Last_Name;
            UpdatedStudent.Student_Num = Student_Num;
            UpdatedStudent.S_Enroll_Date = S_Enroll_Date;


            // not doing anything with the response
            _api.UpdateStudent(id, UpdatedStudent);

            // redirects to show teacher
            return RedirectToAction("SShow", new { id });
        }
    }
}