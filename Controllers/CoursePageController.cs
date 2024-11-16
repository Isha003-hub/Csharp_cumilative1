using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Http;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Assignment__cumilative_1_csharp.Controllers
{
	public class CoursePageController : Controller
	{
			// API handles gathering all the data from
			// the Database and MVC is responsible for creating an HTTP response
			// and showing it on a web page that displays the data from database
			// to the View.

		private readonly TeacherAPIController _api;

		public CoursePageController(TeacherAPIController api)
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
	}
	}

