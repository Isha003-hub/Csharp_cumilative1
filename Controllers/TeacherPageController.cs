using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Http;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Assignment__cumilative_1_csharp.Controllers
{
	public class TeacherPageController : Controller
	{
		//The API retrieves all the data from the database, while the MVC framework processes this data to generate an HTTP response.
		//The response is then rendered on a web page, presenting the database information within the View component.
		//isha
		private readonly TeacherAPIController _api;

		public TeacherPageController(TeacherAPIController api)
		{
			_api = api;
		}

		/// <summary>
		/// The fourth link of the `layout.cshtml` page redirects to the Teachers page.  
		/// This link navigates to a `.cshtml` page displaying all teachers stored in the database.
		/// </summary>
		/// <example>
		/// GET api/Teacher/LTeachers ->  
		/// Alexander Bennett, Caitlin Cummings, Linda Chan, Lauren Smith, Jessica Morris, ......  
		/// </example>
		/// <returns>
		/// A list containing the names of all teachers from the `teachers` table in the school database.
		/// </returns>


		public IActionResult List()
		{
			List<Teacher> Teachers = _api.Listeach();
			return View(Teachers);
		}
		
		/// <summary>
		/// Clicking on a teacher's name redirects the user to a webpage displaying detailed information about that specific teacher.
		/// </summary>
		/// <remarks>
		/// The system gives teacher's ID based on the selected name and uses it to query the database, fetching the related teacher's information.
		/// </remarks>
		/// <example>
		/// GET api/Teacher/TeacherInfo/3 ->  
		/// {  
		///   "TeacherId": 3,  
		///   "TeacherFname": "Caitlin",  
		///   "TeacherLName": "Cummings",  
		///   "EmployeeNumber": "T381",  
		///   "HireDate": "2014-06-10",  
		///   "Salary": "62.77"  
		/// }  
		/// </example>
		/// <returns>
		/// An object which contains detailed information about the selected teacher from the database.
		/// </returns>

		public IActionResult Show(int id)
		{
			Teacher SelTeachers = _api.TeacherInfo(id);
			return View(SelTeachers);
		}

	}
}

