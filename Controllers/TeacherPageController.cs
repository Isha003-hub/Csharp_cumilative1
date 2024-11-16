using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Http;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Assignment__cumilative_1_csharp.Controllers
{
	public class TeacherPageController : Controller
	{
		
			 // API handles gathering all the data from
			 // the Database and MVC is responsible for creating an HTTP response
			 // and showing it on a web page that displays the data from database
			 // to the View.

		private readonly TeacherAPIController _api;

		public TeacherPageController(TeacherAPIController api)
		{
			_api = api;
		}


		/// <summary>
		/// The 4th link added to our web page layout.cshtml is to teachers page, 
		/// it redirects to a cshtml apge that shows a list of all teachers in our created database.
		/// </summary>
		/// <example>
		/// GET api/Teacher/LTeachers -> Alexander Bennett, Caitlin Cummings, Linda Chan, Lauren Smith, Jessica Morris......
		/// </example>
		/// <returns>
		/// A list all the teachers from teachers table in the database school
		/// </returns>

		public IActionResult List()
		{
			List<Teacher> Teachers = _api.LTeachers();
			return View(Teachers);
		}



		/// <summary>
		/// When we click on a teacher name, it redirects to a new webpage that shows details of that teacher
		/// </summary>
		/// <remarks>
		/// it will select the ID of the teacher when you click on its name and it selects the data from the database from the selected id
		/// </remarks>
		/// <example>
		/// GET api/Teacher/TeacherInfo/3 -> {"TeacherId":3,"TeacherFname":"Caitlin","TeacherLName":"Cummings", "Employee Number" : "T381", "Hire Date" : "2014-6-10", "Salary" : "62.77"}
		/// </example>
		/// <returns>
		/// list of all Information about the Selected Teacher from their database
		/// </returns>
		public IActionResult Show(int id)
		{
			Teacher SelTeachers = _api.TeacherInfo(id);
			return View(SelTeachers);
		}

	}
}

