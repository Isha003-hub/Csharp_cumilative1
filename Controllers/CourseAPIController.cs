using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Assignment__cumilative_1_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        // This is dependancy injection
        private readonly SchoolDbContext _context;
        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The third link on our webpage directs to the Students API (Swagger UI), 
        /// which redirects to a Swagger page displaying information about students and their courses 
        /// retrieved from the database.
        /// </summary>
        /// <example>
        /// GET api/Student/Courses -> [{"Course_Id": 0, "Course_Code": "string", "Teacher_Id": 0, "C_Start_Date": "2024-11-16T03:27:01.671Z", "C_End_Date": "2024-11-16T03:27:01.671Z", Course_Name": "string"}]
        /// GET api/Student/Courses -> [{"Teacher_Id": 2, "First_Name": "Caitlin", "Last_Name": "Cummings", "HireDate": "2014-06-10T00:00:00", "Emp_Num": "T381", "Salary": 62.77}]
        /// </example>
        /// <returns>
        /// A list of all students and related course information retrieved from the database.
        /// </returns>


        [HttpGet]
        [Route(template: "LCourses")]
        public List<Course> Lcourses()
        {
            // Create a list of course
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

                        // Accessing the information of course using the Column name as an index
                        int c_id = Convert.ToInt32(ResultSet["Courseid"]);
                        string c_code = ResultSet["coursecode"].ToString();
                        int t_id = Convert.ToInt32(ResultSet["teacherid"]);
                        DateTime sdate = Convert.ToDateTime(ResultSet["startdate"]);
                        DateTime edate = Convert.ToDateTime(ResultSet["finishdate"]);
                        string c_name = ResultSet["coursename"].ToString();


                        // Assigning short names for properties of the course
                        Course course_details = new Course()
                        {
                            Course_Id = c_id,
                            Course_Code = c_code,
                            Teacher_Id = t_id,
                            C_Start_Date = sdate,
                            C_End_Date = edate,
                            Course_Name = c_name,
                        };
                        // Adding all the values of properties of Course_details in student List
                        Courses.Add(course_details);

                    }
                }
            }

            //Return the final list of course 
            return Courses;
        }

        /// <summary>
        /// When we click on a course name, it redirects to a new webpage that shows details of that course
        /// same wayf for API once we give an input of our id it shows details of that course
        /// </summary>
        /// <remarks>
        /// it will select the ID of the course when you click (or give it as an input in swagger ui) on its name and it selects the data from the database from the selected id
        /// </remarks>
        /// <example>
        /// GET api/Teacher/TeacherInfo/1 -> {"Course_Id": 1,"Course_Code": "http5101","Teacher_Id": 1,"C_Start_Date": "2018-09-04T00:00:00","e_Date": "2018-12-14T00:00:00","Course_Name": "Web Application Development"}
        /// </example>
        /// <returns>
        /// list of all Information about the Selected course from their database
        /// </returns>



        [HttpGet]
        [Route(template: "CourseInfo/{id}")]
        public Course CourseInfo(int id)
        {

            // Created an object "SelTeachers" using course definition defined as Class in Models
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

                        // Accessing the information of course using the Column name as an index
                        int c_id = Convert.ToInt32(ResultSet["Courseid"]);
                        string c_code = ResultSet["coursecode"].ToString();
                        int t_id = Convert.ToInt32(ResultSet["teacherid"]);
                        DateTime sdate = Convert.ToDateTime(ResultSet["startdate"]);
                        DateTime edate = Convert.ToDateTime(ResultSet["finishdate"]);
                        string c_name = ResultSet["coursename"].ToString();


                        // Accessing the information of the properties of course and then assigning it to the short names 
                        // created above for all properties of the course
                        SelCourse.Course_Id = c_id;
                        SelCourse.Course_Code = c_code;
                        SelCourse.Teacher_Id = t_id;
                        SelCourse.C_Start_Date = sdate;
                        SelCourse.C_End_Date = edate;
                        SelCourse.Course_Name = c_name;
                    }
                }
            }


            //Return the Information of the SelCourse
            return SelCourse;
        }

        /// <summary>
        /// Clicking on a course name redirects to a new webpage displaying detailed information about that course. 
        /// Similarly, using the API, providing the course ID as input retrieves the course details.
        /// </summary>
        /// <remarks>
        /// The course ID is either selected upon clicking the course name on the webpage or provided as input in the Swagger UI. 
        /// This ID is used to fetch the corresponding course data from the database.
        /// </remarks>
        /// <example>
        /// GET api/Course/CourseInfo/1 -> {"Course_Id": 1, "Course_Code": "http5101", "Teacher_Id": 1, "Course_Start_Date": "2018-09-04T00:00:00", "Course_End_Date": "2018-12-14T00:00:00", "Course_Name": "Web Application Development"}
        /// </example>
        /// <returns>
        /// Detailed information about the selected course retrieved from the database.
        /// </returns>




        [HttpPost(template: "AddCourse")]
        public int AddCourse([FromBody] Course CourseData)
        {
            // 'using' keyword is used that will close the connection by itself after executing the code given inside
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Opening the Connection
                Connection.Open();

                // Establishing a new query for our database
                MySqlCommand Command = Connection.CreateCommand();

                // It contains the SQL query to insert a new course into the courses table            
                Command.CommandText = "INSERT INTO courses (coursecode, teacherid, startdate, finishdate, coursename) VALUES (@coursecode, @teacherid, @startdate, @finishdate, @coursename)";

                Command.Parameters.AddWithValue("@coursecode", CourseData.Course_Code);
                Command.Parameters.AddWithValue("@teacherid", CourseData.Teacher_Id);
                Command.Parameters.AddWithValue("@startdate", CourseData.C_Start_Date);
                Command.Parameters.AddWithValue("@finishdate", CourseData.C_End_Date);
                Command.Parameters.AddWithValue("@coursename", CourseData.Course_Name);

                // It runs the query against the database and the new record is inserted
                Command.ExecuteNonQuery();

                // It fetches the ID of the newly inserted course record and converts it to an integer to be returned
                return Convert.ToInt32(Command.LastInsertedId);


            }

        }

        /// <summary>
        /// This method deletes a course from the database using the course's unique ID provided in the request URL. 
        /// It returns the number of rows affected by the deletion operation.
        /// </summary>
        /// <param name="CourseId">
        /// The unique ID of the course to be deleted.
        /// </param>
        /// <returns>
        /// The number of rows deleted from the database.
        /// </returns>
        /// <example>
        /// DELETE: api/CourseAPI/DeleteCourse/11 -> 1  
        /// (Indicating that one record was successfully deleted.)
        /// </example>


        [HttpDelete(template: "DeleteCourse/{CourseId}")]

        public int DeleteCourse(int CourseId)
        {
            // 'using' keyword is used that will close the connection by itself after executing the code given inside
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Opening the Connection
                Connection.Open();

                // Establishing a new query for our database
                MySqlCommand Command = Connection.CreateCommand();

                // It contains the SQL query to delete a record from the courses table based on the course's ID
                Command.CommandText = "DELETE FROM courses WHERE courseid=@id";
                Command.Parameters.AddWithValue("@id", CourseId);

                // It runs the DELETE query and the number of affected rows is returned.
                return Command.ExecuteNonQuery();

            }

        }

        /// <summary>
        /// Updates an Course in the database. Data is Course object, request query contains ID
        /// </summary>
        /// <param name="CourseData">Course Object</param>
        /// <param name="CourseId">The Course ID primary key</param>
        /// <example>
        /// PUT: api/Course/UpdateCourse/4
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// { "Course_Code":"http5121", "C_Start_Date":2024-12-05,"C_End_Date":2024-12-04,"Course_Name":"WebDev"} -> 
        /// {"Course_Id":4,"Course_Code":"http5121", "C_Start_Date":2024-12-05 12:00:00 AM,"C_End_Date":2024-12-04 12:00:00 AM,"Course_Name":"WebDev" }
        /// </example>
        /// <returns>
        /// The updated Course object
        /// </returns>
        [HttpPut(template: "UpdateCourse/{CourseId}")]
        public Course UpdateCourse(int CourseId, [FromBody] Course CourseData)
        {
            // 'using' will close the connection after the code executes
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                //Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();

                // parameterize query
                Command.CommandText = "update courses set coursecode=@coursecode, startdate = @startdate, finishdate = @finishdate, coursename = @coursename, teacherid = @teacherid where courseid=@id";
                Command.Parameters.AddWithValue("@coursecode", CourseData.Course_Code);
                Command.Parameters.AddWithValue("@startdate", CourseData.C_Start_Date);
                Command.Parameters.AddWithValue("@finishdate", CourseData.C_End_Date);
                Command.Parameters.AddWithValue("@coursename", CourseData.Course_Name);
                Command.Parameters.AddWithValue("@teacherid", CourseData.Teacher_Id);

                Command.Parameters.AddWithValue("@id", CourseId);

                Command.ExecuteNonQuery();
            }
            return CourseInfo(CourseId);
        }
    }
}