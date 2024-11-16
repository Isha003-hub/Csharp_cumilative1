using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Http;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;

namespace Assignment__cumilative_1_csharp.Controllers
{
	public class CoursePageController : Controller
	{
		//The API retrieves data from the database
		//The MVC framework is tasked with generating an HTTP response and presenting it on a webpage.
		//This ensures that the database information is displayed seamlessly in the View.

		private readonly TeacherAPIController _api;
		public CoursePageController(TeacherAPIController api)
		{
			_api = api;
		}
		public IActionResult CList()
		{
			List<Course> courses = _api.listcourse();
			return View(courses);
		}

		public IActionResult CShow(int id)
		{
			Course SelCourse = _api.CourseInfo(id);
			return View(SelCourse);
		}
	}
	}

