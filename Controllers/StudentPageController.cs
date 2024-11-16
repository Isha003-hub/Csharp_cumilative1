using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Http;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;


namespace Assignment__cumilative_1_csharp.Controllers
{
	public class StudentPageController : Controller
	{
		// The API retrieves all the data from the database,
		// The MVC framework handles generating an HTTP response.
		// and rendering it on a web page to display the database content in the View.

		private readonly TeacherAPIController _api;

		public StudentPageController(TeacherAPIController api)
		{
			_api = api;
		}

		public IActionResult SList()
		{
			List<Student> students = _api.liststudent();

			return View(students);
		}

		public IActionResult SShow(int id)
		{
			Student SelStudents = _api.StudentInfo(id);
			return View(SelStudents);
		}
	}
	}
