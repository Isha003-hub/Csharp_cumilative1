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
		/// link 3, which is added to our webpage connects to the Teachers API (Swagger UI), which redirects user to a swgger page and display list of teachers stored in the database.
		/// </summary>
		/// <example>
		/// GET api/Teacher/LTeachers -> [{"FirstName":"Alexander", "LastName":"Bennett"},{"FirstName":"Caitlin", "LastName":"Cummings"},.............]  
		/// GET api/Teacher/LTeachers -> [{"FirstName":"Linda", "LastName":""},{"FirstName":"Lauren", "LastName":"Smith"},.............]  
		/// </example>
		/// <returns>
		/// All teacher records retrieved from the 'teachers' table in the school database.
		/// </returns>



		[HttpGet]
		[Route(template: "Listeach")]
		public List<Teacher> Listeach()
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
						int teach_id = Convert.ToInt32(ResultSet["teacherid"]);
						string first_name = ResultSet["teacherfname"].ToString();
						string last_name = ResultSet["teacherlname"].ToString();
						string e_number = ResultSet["employeenumber"].ToString();
						DateTime hire_date = Convert.ToDateTime(ResultSet["hiredate"]);
						decimal salary = Convert.ToDecimal(ResultSet["salary"]);


						// Assigning short names for properties of the Teacher
						Teacher t_details = new Teacher()
						{
							tid = teach_id,
							fname = first_name,
							lname = last_name,
							hiredt = hire_date,
							enumber = e_number,
							salary = salary
						};


						// Adding all the values of properties of t_details in Teachers List
						Teachers.Add(t_details);

					}
				}
			}


			//Return the final list of Teachers 
			return Teachers;
		}

		/// <summary>
		/// Clicking on a teacher's name redirects to a new webpage displaying detailed information about that teacher.  
		/// Similarly, in the API, providing a teacher's ID as input returns their details.
		/// </summary>
		/// <remarks>
		/// The teacher's ID is selected either by clicking on their name on the webpage or by entering the ID in the Swagger UI.  
		/// This ID is used to fetch the teacher's data from the database.
		/// </remarks>
		/// <example>
		/// GET api/Teacher/TeacherInfo/3 ->  
		/// {  
		///   "TeacherId": 3,  
		///   "TeacherFname": "Caitlin",  
		///   "TeacherLName": "Cummings",  
		///   "EmployeeNumber": "T381",  
		///   "HireDate": "2014-6-10",  
		///   "Salary": "62.77"  
		/// }  
		/// </example>
		/// <returns>
		/// A detailed record - containing all information about the selected teacher from the database.
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
						SelTeachers.tid = t_id;
						SelTeachers.fname = fn;
						SelTeachers.lname = ln;
						SelTeachers.hiredt = hdate;
						SelTeachers.enumber = empnum;
						SelTeachers.salary = salary;
					}
				}
			}


			//Return the Information of the SelTeachers
			return SelTeachers;
		}

		//Students

		[HttpGet]
		[Route(template: "liststudent")]
		public List<Student> liststudent()
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
							student_id = s_id,
							student_fname = fn,
							student_lname = ln,
							student_number = snum,
							student_enroll_dt = endate,
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
		/// Clicking on a teacher's name redirects the user to a webpage displaying detailed information about that teacher.  
		/// Similarly, using the API, providing a teacher's ID as input retrieves their details.
		/// </summary>
		/// <remarks>
		/// The system identifies the teacher's ID either by selecting their name on the webpage or by inputting the ID in the Swagger UI.  
		/// This ID is then used to fetch the relevant teacher's data from the database.
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
		/// A detailed object containing all available information about the selected teacher from the database.
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


						// Access the information of the properties of Teacher , then assigning it to the short names 
						// created above for all properties of the Teacher
						SelStudent.student_id = s_id;
						SelStudent.student_fname = fn;
						SelStudent.student_lname = ln;
						SelStudent.student_number = snum;
						SelStudent.student_enroll_dt = endate;
					}
				}
			}


			//Return the Information of the SelTeachers
			return SelStudent;
		}

		//Courses

		[HttpGet]
		[Route(template: "listcourse")]
		public List<Course> listcourse()
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
							Course_id = c_id,
							Course_code = c_code,
							teacher_id = t_id,
							start_date = sdate,
							end_date = edate,
							course_name = c_name,
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
		/// Selecting a teacher's name navigates the user to a webpage displaying the teacher's detailed information.  
		/// Similarly, in the API, providing a teacher's ID as input fetches their details.
		/// </summary>
		/// <remarks>
		/// The system uses the teacher's ID, either obtained by clicking on the name in the interface or by entering the ID in the Swagger UI,  
		/// to query the database and retrieve the corresponding teacher's details.
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
		/// An object containing all available information about the specified teacher from the database.
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


						// Access the information of the properties of Teacher and then assigning it to the short names 
						// created above for all properties of the Teacher
						SelCourse.Course_id = c_id;
						SelCourse.Course_code = c_code;
						SelCourse.teacher_id = t_id;
						SelCourse.start_date = sdate;
						SelCourse.end_date = edate;
						SelCourse.course_name = c_name;
					}
				}
			}


			//Return the Information of the SelTeachers
			return SelCourse;
		}
	}
}
