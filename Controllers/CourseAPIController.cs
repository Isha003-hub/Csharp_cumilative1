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

        // -------------------------------------------------Courses--------------------------------------------------------------------------------------

        /// <summary>
        /// The 3th link added to our web page is to student API (Swagger UI), 
        /// it redirects to a swagger apge that shows a list of all teachers in our created database.
        /// </summary>
        /// <example>
        /// GET api/Teacher/LTeachers -> [{"c_Id": 0,"cCode": "string","t_Id": 0,"s_Date": "2024-11-16T03:27:01.671Z","e_Date": "2024-11-16T03:27:01.671Z","cName": "string"}]
        /// GET api/Teacher/LTeachers -> [{"t_Id": 2,"fName": "Caitlin","lName": "Cummings","hireDate": "2014-06-10T00:00:00","e_Number": "T381","salary": 62.77}]
        /// </example>
        /// <returns>
        /// A list all the teachers from teachers table in the database school
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
        /// GET api/Teacher/TeacherInfo/1 -> {"c_Id": 1,"cCode": "http5101","t_Id": 1,"s_Date": "2018-09-04T00:00:00","e_Date": "2018-12-14T00:00:00","cName": "Web Application Development"}
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
        /// The method adds a new course to the database by inserting a record into the courses table and returns the ID of the inserted course
        /// </summary>
        /// <param name="CourseData"> An object containing the details of the course to be added, including first name, last name, employee number, salary, and hire date </param>
        /// <returns>
        /// The ID of the newly inserted course record
        /// </returns>
        /// <example> 
        /// POST: api/CourseAPI/AddCourse -> 11
        /// assuming that 11th record is added
        /// </example>



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
        /// The method deletes a course from the database using the course's ID provided in the request URL. It returns the number of rows affected.
        /// </summary>
        /// <param name="CourseId"> The unique ID of the course to be deleted </param>
        /// <returns>
        /// The number of rows affected by the DELETE operation
        /// </returns>
        /// <example>
        /// DELETE: api/CourseAPI/DeleteCourse/11 -> 1
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
    }
}