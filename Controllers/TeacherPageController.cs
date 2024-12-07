using Microsoft.AspNetCore.Mvc;
using Assignment__cumilative_1_csharp.Models;

namespace Assignment__cumilative_1_csharp.Controllers
{
    public class TeacherPageController : Controller
    {
        // The API retrieves all necessary data from the database, 
        // while the MVC architecture ensures the data is processed 
        // and sent as an HTTP response. The response is rendered 
        // on a web page, presenting the database information in the View.

        private readonly TeacherAPIController _api;

        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        /// <summary>
        /// The fourth link in the layout.cshtml navigates to the Teachers page. 
        /// This link redirects users to a .cshtml page displaying a list of all teachers 
        /// retrieved from the database.
        /// </summary>
        /// <example>
        /// GET api/Teacher/LTeachers -> Alexander Bennett, Caitlin Cummings, Linda Chan, Lauren Smith, Jessica Morris, etc.
        /// </example>
        /// <returns>
        /// A complete list of teachers from the "teachers" table in the "school" database.
        /// </returns>


        public IActionResult List()
        {
            List<Teacher> Teachers = _api.LTeachers();
            return View(Teachers);
        }

        /// <summary>
        /// Clicking on a teacher's name redirects to a new webpage displaying detailed information about the selected teacher.
        /// </summary>
        /// <remarks>
        /// The system captures the teacher's ID upon clicking their name, retrieves the corresponding data from the database, and displays it on the page.
        /// </remarks>
        /// <example>
        /// GET api/Teacher/TeacherInfo/3 -> {"Teacher_Id":3, "First_Name":"Caitlin", "Last_Name":"Cummings", "Emp_Num":"T381", "HireDate":"2014-06-10", "Salary":"62.77"}
        /// </example>
        /// <returns>
        /// Detailed information about the selected teacher from the database.
        /// </returns>

        public IActionResult Show(int id)
        {
            Teacher SelTeachers = _api.TeacherInfo(id);
            return View(SelTeachers);
        }

        // GET : TeacherPage/New
        [HttpGet]
        public IActionResult New(int id)
        {
            return View();
        }

        // POST: TeacherPage/Create
        [HttpPost]
        public IActionResult Create(Teacher NewTeacher)
        {
            int TeacherId = _api.AddTeacher(NewTeacher);

            // redirects to "Show" action on "Teacher" cotroller with id parameter supplied
            return RedirectToAction("Show", new { id = TeacherId });
        }



        // GET : TeacherPage/DeleteConfirm/{id}
        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Teacher SelectedTeacher = _api.TeacherInfo(id);
            return View(SelectedTeacher);
        }


        // POST: TeacherPage/Delete/{id}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            int TeacherId = _api.DeleteTeacher(id);
            // redirects to list action
            return RedirectToAction("List");
        }

        // GET : TeacherPage/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Teacher SelectedTeacher = _api.TeacherInfo(id);
            return View(SelectedTeacher);
        }

        // POST: TeacherPage/Update/{id}
        [HttpPost]
        public IActionResult Update(int id, string First_Name, string Last_Name, decimal Salary, string Emp_Num, DateTime HireDate)
        {
            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.First_Name = First_Name;
            UpdatedTeacher.Last_Name = Last_Name;
            UpdatedTeacher.Salary = Salary;
            UpdatedTeacher.Emp_Num = Emp_Num;
            UpdatedTeacher.HireDate = HireDate;


            // not doing anything with the response
            _api.UpdateTeacher(id, UpdatedTeacher);


            // redirects to show teacher
            return RedirectToAction("Show", new { id });
        }

    }
}