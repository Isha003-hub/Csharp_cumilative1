using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Http;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Assignment__cumilative_1_csharp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeacherAPIController : ControllerBase
	{
		// This is dependancy injection
		private readonly SchoolDbContext _context;
		public TeacherAPIController(SchoolDbContext context)
		{
			_context = context;
		}


		/// <summary>
		/// The 3th link added to our web page is to teachers API (Swagger UI), 
		/// it redirects to a swagger apge that shows a list of all teachers in our created database.
		/// </summary>
		/// <example>
		/// GET api/Teacher/LTeachers -> [{"TeacherFname":"Alexander", "TeacherLName":"Bennett"},{"TeacherFname":"Caitlin", "TeacherLName":"Cummings"},.............]
		/// GET api/Teacher/LTeachers -> [{"TeacherFname":"Linda", "Chan":""},{"TeacherFname":"Lauren", "TeacherLName":"Smith"},.............]
		/// </example>
		/// <returns>
		/// A list all the teachers from teachers table in the database school
		/// </returns>


		[HttpGet]
		[Route(template: "LTeachers")]
		public List<Teacher> LTeachers()
		{
			// Create a list of Teachers
			List<Teacher> Teachers = new List<Teacher>();

			// 'using' keyword automatically closes the connection by itself after executing the code given inside
			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				// Opening the connection
				Connection.Open();


				// Establishing a new query for our database 
				MySqlCommand Command = Connection.CreateCommand();


				// Writing the SQL Query we want to give to database to access information
				Command.CommandText = "select * from teachers";


				// Storing the Result Set query in a variable
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					// While loop is used to loop through each row in the ResultSet 
					while (ResultSet.Read())
					{

						// Accessing the information of Teacher using the Column name as an index
						int t_id = Convert.ToInt32(ResultSet["teacherid"]);
						string fn = ResultSet["teacherfname"].ToString();
						string ln = ResultSet["teacherlname"].ToString();
						string empnum = ResultSet["employeenumber"].ToString();
						DateTime hdate = Convert.ToDateTime(ResultSet["hiredate"]);
						decimal salary = Convert.ToDecimal(ResultSet["salary"]);


						// Assigning short names for properties of the Teacher
						Teacher teacher_details = new Teacher()
						{
							T_Id = t_id,
							FName = fn,
							LName = ln,
							HireDate = hdate,
							E_Number = empnum,
							Salary = salary
						};


						// Adding all the values of properties of teacher_details in Teachers List
						Teachers.Add(teacher_details);

					}
				}
			}


			//Return the final list of Teachers 
			return Teachers;
		}


		/// <summary>
		/// When we click on a teacher name, it redirects to a new webpage that shows details of that teacher
		/// same wayf for API once we give an input of our id it shows details of that teacher
		/// </summary>
		/// <remarks>
		/// it will select the ID of the teacher when you click (or give it as an input in swagger ui) on its name and it selects the data from the database from the selected id
		/// </remarks>
		/// <example>
		/// GET api/Teacher/TeacherInfo/3 -> {"TeacherId":3,"TeacherFname":"Caitlin","TeacherLName":"Cummings", "Employee Number" : "T381", "Hire Date" : "2014-6-10", "Salary" : "62.77"}
		/// </example>
		/// <returns>
		/// list of all Information about the Selected Teacher from their database
		/// </returns>



		[HttpGet]
		[Route(template: "TeacherInfo/{id}")]
		public Teacher TeacherInfo(int id)
		{

			// Created an object "SelTeachers" using Teacher definition defined as Class in Models
			Teacher SelTeachers = new Teacher();


			// 'using' keyword automatically closes the connection by itself after executing the code given inside
			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				// Opening the Connection
				Connection.Open();

				// Establishing a new query for our database 
				MySqlCommand Command = Connection.CreateCommand();


				// @id is replaced with a 'sanitized'(masked) id so that id can be referenced
				// without revealing the actual @id
				Command.CommandText = "select * from teachers where teacherid=@id";
				Command.Parameters.AddWithValue("@id", id);


				// Storing the Result Set query in a variable
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					// While loop is used to loop through each row in the ResultSet 
					while (ResultSet.Read())
					{

						// Accessing the information of Teacher using the Column name as an index
						int t_id = Convert.ToInt32(ResultSet["teacherid"]);
						string fn = ResultSet["teacherfname"].ToString();
						string ln = ResultSet["teacherlname"].ToString();
						string empnum = ResultSet["employeenumber"].ToString();
						DateTime hdate = Convert.ToDateTime(ResultSet["hiredate"]);
						decimal salary = Convert.ToDecimal(ResultSet["salary"]);


						// Accessing the information of the properties of Teacher and then assigning it to the short names 
						// created above for all properties of the Teacher
						SelTeachers.T_Id = t_id;
						SelTeachers.FName = fn;
						SelTeachers.LName = ln;
						SelTeachers.HireDate = hdate;
						SelTeachers.E_Number = empnum;
						SelTeachers.Salary = salary;
					}
				}
			}


			//Return the Information of the SelTeachers
			return SelTeachers;
		}

		// ---------------------------------------------------------Students-----------------------------------------------------------------------

		[HttpGet]
		[Route(template: "LStudents")]
		public List<Student> LStudents()
		{
			// Create a list of Students
			List<Student> Students = new List<Student>();

			// 'using' keyword automatically closes the connection by itself after executing the code given inside
			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				// Opening the connection
				Connection.Open();


				// Establishing a new query for our database 
				MySqlCommand Command = Connection.CreateCommand();


				// Writing the SQL Query we want to give to database to access information
				Command.CommandText = "select * from students";


				// Storing the Result Set query in a variable
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					// While loop is used to loop through each row in the ResultSet 
					while (ResultSet.Read())
					{

						// Accessing the information of Students using the Column name as an index
						int s_id = Convert.ToInt32(ResultSet["studentid"]);
						string fn = ResultSet["studentfname"].ToString();
						string ln = ResultSet["studentlname"].ToString();
						string snum = ResultSet["studentnumber"].ToString();
						DateTime endate = Convert.ToDateTime(ResultSet["enroldate"]);


						// Assigning short names for properties of the Students
						Student student_details = new Student()
						{
							S_Id = s_id,
							S_FName = fn,
							S_LName = ln,
							S_Num = snum,
							S_E_Date = endate,
						};


						// Adding all the values of properties of Students_details in student List
						Students.Add(student_details);

					}
				}
			}


			//Return the final list of Students 
			return Students;
		}


		/// <summary>
		/// When we click on a teacher name, it redirects to a new webpage that shows details of that teacher
		/// same wayf for API once we give an input of our id it shows details of that teacher
		/// </summary>
		/// <remarks>
		/// it will select the ID of the teacher when you click (or give it as an input in swagger ui) on its name and it selects the data from the database from the selected id
		/// </remarks>
		/// <example>
		/// GET api/Teacher/TeacherInfo/3 -> {"TeacherId":3,"TeacherFname":"Caitlin","TeacherLName":"Cummings", "Employee Number" : "T381", "Hire Date" : "2014-6-10", "Salary" : "62.77"}
		/// </example>
		/// <returns>
		/// list of all Information about the Selected Teacher from their database
		/// </returns>



		[HttpGet]
		[Route(template: "StudentInfo/{id}")]
		public Student StudentInfo(int id)
		{

			// Created an object "SelTeachers" using Teacher definition defined as Class in Models
			Student SelStudent = new Student();


			// 'using' keyword automatically closes the connection by itself after executing the code given inside
			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				// Opening the Connection
				Connection.Open();

				// Establishing a new query for our database 
				MySqlCommand Command = Connection.CreateCommand();


				// @id is replaced with a 'sanitized'(masked) id so that id can be referenced
				// without revealing the actual @id
				Command.CommandText = "select * from students where studentid=@id";
				Command.Parameters.AddWithValue("@id", id);


				// Storing the Result Set query in a variable
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					// While loop is used to loop through each row in the ResultSet 
					while (ResultSet.Read())
					{

						// Accessing the information of Teacher using the Column name as an index
						int s_id = Convert.ToInt32(ResultSet["studentid"]);
						string fn = ResultSet["studentfname"].ToString();
						string ln = ResultSet["studentlname"].ToString();
						string snum = ResultSet["studentnumber"].ToString();
						DateTime endate = Convert.ToDateTime(ResultSet["enroldate"]);


						// Accessing the information of the properties of Teacher and then assigning it to the short names 
						// created above for all properties of the Teacher
						SelStudent.S_Id = s_id;
						SelStudent.S_FName = fn;
						SelStudent.S_LName = ln;
						SelStudent.S_Num = snum;
						SelStudent.S_E_Date = endate;
					}
				}
			}


			//Return the Information of the SelTeachers
			return SelStudent;
		}

		// -------------------------------------------------Courses--------------------------------------------------------------------------------------

		[HttpGet]
		[Route(template: "LCourses")]
		public List<Course> Lcourses()
		{
			// Create a list of Students
			List<Course> Courses = new List<Course>();

			// 'using' keyword automatically closes the connection by itself after executing the code given inside
			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				// Opening the connection
				Connection.Open();


				// Establishing a new query for our database 
				MySqlCommand Command = Connection.CreateCommand();


				// Writing the SQL Query we want to give to database to access information
				Command.CommandText = "SELECT * FROM courses;";


				// Storing the Result Set query in a variable
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					// While loop is used to loop through each row in the ResultSet 
					while (ResultSet.Read())
					{

						// Accessing the information of Students using the Column name as an index
						int c_id = Convert.ToInt32(ResultSet["Courseid"]);
						string c_code = ResultSet["coursecode"].ToString();
						int t_id = Convert.ToInt32(ResultSet["teacherid"]);
						DateTime sdate = Convert.ToDateTime(ResultSet["startdate"]);
						DateTime edate = Convert.ToDateTime(ResultSet["finishdate"]);
						string c_name = ResultSet["coursename"].ToString();


						// Assigning short names for properties of the Students
						Course course_details = new Course()
						{
							C_Id = c_id,
							CCode = c_code,
							T_Id = t_id,
							S_Date = sdate,
							E_Date = edate,
							CName = c_name,
						};


						// Adding all the values of properties of Students_details in student List
						Courses.Add(course_details);

					}
				}
			}


			//Return the final list of Students 
			return Courses;
		}


		/// <summary>
		/// When we click on a teacher name, it redirects to a new webpage that shows details of that teacher
		/// same wayf for API once we give an input of our id it shows details of that teacher
		/// </summary>
		/// <remarks>
		/// it will select the ID of the teacher when you click (or give it as an input in swagger ui) on its name and it selects the data from the database from the selected id
		/// </remarks>
		/// <example>
		/// GET api/Teacher/TeacherInfo/3 -> {"TeacherId":3,"TeacherFname":"Caitlin","TeacherLName":"Cummings", "Employee Number" : "T381", "Hire Date" : "2014-6-10", "Salary" : "62.77"}
		/// </example>
		/// <returns>
		/// list of all Information about the Selected Teacher from their database
		/// </returns>



		[HttpGet]
		[Route(template: "CourseInfo/{id}")]
		public Course CourseInfo(int id)
		{

			// Created an object "SelTeachers" using Teacher definition defined as Class in Models
			Course SelCourse = new Course();


			// 'using' keyword automatically closes the connection by itself after executing the code given inside
			using (MySqlConnection Connection = _context.AccessDatabase())
			{

				// Opening the Connection
				Connection.Open();

				// Establishing a new query for our database 
				MySqlCommand Command = Connection.CreateCommand();


				// @id is replaced with a 'sanitized'(masked) id so that id can be referenced
				// without revealing the actual @id
				Command.CommandText = "SELECT * FROM courses WHERE courseid = @id;";
				Command.Parameters.AddWithValue("@id", id);


				// Storing the Result Set query in a variable
				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{

					// While loop is used to loop through each row in the ResultSet 
					while (ResultSet.Read())
					{

						// Accessing the information of Students using the Column name as an index
						int c_id = Convert.ToInt32(ResultSet["Courseid"]);
						string c_code = ResultSet["coursecode"].ToString();
						int t_id = Convert.ToInt32(ResultSet["teacherid"]);
						DateTime sdate = Convert.ToDateTime(ResultSet["startdate"]);
						DateTime edate = Convert.ToDateTime(ResultSet["finishdate"]);
						string c_name = ResultSet["coursename"].ToString();


						// Accessing the information of the properties of Teacher and then assigning it to the short names 
						// created above for all properties of the Teacher
						SelCourse.C_Id = c_id;
						SelCourse.CCode = c_code;
						SelCourse.T_Id = t_id;
						SelCourse.S_Date = sdate;
						SelCourse.E_Date = edate;
						SelCourse.CName = c_name;
					}
				}
			}


			//Return the Information of the SelTeachers
			return SelCourse;
		}
	}
}
