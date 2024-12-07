using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Assignment__cumilative_1_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {

        // This is dependancy injection
        private readonly SchoolDbContext _context;
        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The third link on our webpage directs to the Students API (Swagger UI), 
        /// which redirects to a Swagger page displaying a list of all students in the database.
        /// </summary>
        /// <example>
        /// GET api/Student/LStudents -> [{"Student_Id": 0, "First_Name": "string", "Last_Name": "string", "Student_Num": "string", "S_Enroll_Date": "2024-11-16T03:21:24.340Z"}]
        /// GET api/Student/LStudents -> [{"Student_Id": 2, "Firat_Name": "Caitlin", "Last_Name": "Cummings", Student_Num": "T381", "S_Enroll_Date": "2014-06-10T00:00:00"}]
        /// </example>
        /// <returns>
        /// A list of all students from the "Student" table in the "school" database.
        /// </returns>


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
                            Student_Id = s_id,
                            First_Name = fn,
                            Last_Name = ln,
                            Student_Num = snum,
                            S_Enroll_Date = endate,
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
        /// Clicking on a student's name redirects to a new webpage displaying detailed information about that student. 
        /// Similarly, using the API, providing the student's ID as input retrieves the student's details.
        /// </summary>
        /// <remarks>
        /// The student ID is either selected upon clicking the student's name on the webpage or provided as input in the Swagger UI. 
        /// This ID is used to fetch the corresponding student's data from the database.
        /// </remarks>
        /// <example>
        /// GET api/Student/StudentInfo/1 -> {"Student_Id": 1, "First_Name": "Sarah", "Last_Name": "Valdez", "Student_Num": "N1678", "Student_Enrollment_Date": "2018-06-18T00:00:00"}
        /// </example>
        /// <returns>
        /// Detailed information about the selected student retrieved from the database.
        /// </returns>




        [HttpGet]
        [Route(template: "StudentInfo/{id}")]
        public Student StudentInfo(int id)
        {

            // Created an object "SelStudent" using student definition defined as Class in Models
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

                        // Accessing the information of student using the Column name as an index
                        int s_id = Convert.ToInt32(ResultSet["studentid"]);
                        string fn = ResultSet["studentfname"].ToString();
                        string ln = ResultSet["studentlname"].ToString();
                        string snum = ResultSet["studentnumber"].ToString();
                        DateTime endate = Convert.ToDateTime(ResultSet["enroldate"]);


                        // Accessing the information of the properties of student and then assigning it to the short names 
                        // created above for all properties of the student
                        SelStudent.Student_Id = s_id;
                        SelStudent.First_Name = fn;
                        SelStudent.Last_Name = ln;
                        SelStudent.Student_Num = snum;
                        SelStudent.S_Enroll_Date = endate;
                    }
                }
            }


            //Return the Information of the SelStudent(selected student)
            return SelStudent;
        }

        /// <summary>
        /// This method adds a new student to the database by inserting a record into the students table and returns the ID of the newly inserted student.
        /// </summary>
        /// <param name="StudentData">
        /// An object containing the details of the student to be added, such as first name, last name, student number, enrollment date, etc.
        /// </param>
        /// <returns>
        /// The ID of the newly inserted student record.
        /// </returns>
        /// <example>
        /// POST: api/StudentAPI/AddStudent -> 11
        /// (Indicating that the 11th student record was successfully added.)
        /// </example>




        [HttpPost(template: "AddStudent")]
        public int AddStudent([FromBody] Student StudentData)
        {
            // 'using' keyword is used that will close the connection by itself after executing the code given inside
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Opening the Connection
                Connection.Open();

                // Establishing a new query for our database
                MySqlCommand Command = Connection.CreateCommand();

                // It contains the SQL query to insert a new student into the students table            
                Command.CommandText = "INSERT INTO students (studentfname, studentlname, studentnumber, enroldate) VALUES (@studentfname, @studentlname, @studentnumber, @enroldate)";

                Command.Parameters.AddWithValue("@studentfname", StudentData.First_Name);
                Command.Parameters.AddWithValue("@studentlname", StudentData.Last_Name);
                Command.Parameters.AddWithValue("@studentnumber", StudentData.Student_Num);
                Command.Parameters.AddWithValue("@enroldate", StudentData.S_Enroll_Date);

                // It runs the query against the database and the new record is inserted
                Command.ExecuteNonQuery();

                // It fetches the ID of the newly inserted student record and converts it to an integer to be returned
                return Convert.ToInt32(Command.LastInsertedId);


            }

        }



        /// <summary>
        /// This method deletes a student from the database using the student's unique ID provided in the request URL. 
        /// It returns the number of rows affected by the delete operation.
        /// </summary>
        /// <param name="StudentId">
        /// The unique ID of the student to be deleted.
        /// </param>
        /// <returns>
        /// The number of rows affected by the DELETE operation (usually 1 if the student was successfully deleted).
        /// </returns>
        /// <example>
        /// DELETE: api/StudentAPI/DeleteStudent/11 -> 1
        /// (Indicating that one record was successfully deleted.)
        /// </example>


        [HttpDelete(template: "DeleteStudent/{StudentId}")]

        public int DeleteStudent(int StudentId)
        {
            // 'using' keyword is used that will close the connection by itself after executing the code given inside
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Opening the Connection
                Connection.Open();

                // Establishing a new query for our database
                MySqlCommand Command = Connection.CreateCommand();

                // It contains the SQL query to delete a record from the students table based on the student's ID
                Command.CommandText = "DELETE FROM students WHERE studentid=@id";
                Command.Parameters.AddWithValue("@id", StudentId);

                // It runs the DELETE query and the number of affected rows is returned.
                return Command.ExecuteNonQuery();

            }

        }

        /// <summary>
        /// Updates an Student in the database. Data is Student object, request query contains ID
        /// </summary>
        /// <param name="StudentData">Student Object</param>
        /// <param name="StudentId">The Student ID primary key</param>
        /// <example>
        /// PUT: api/Student/UpdateStudent/14
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// { "First_name":"Isha", "Last_name":"Shah", "Student_Num" :1221, StudentEnrolmentDate":2024-12-05} -> 
        /// {"Student_Id":14, "First_name":"Isha", "Last_Name":"Shah", "Student_Num" :1221, S_Enroll_Date":2024-12-05}
        /// </example>
        /// <returns>
        /// The updated Student object
        /// </returns>
        [HttpPut(template: "UpdateStudent/{StudentId}")]
        public Student UpdateStudent(int StudentId, [FromBody] Student StudentData)
        {
            // 'using' will close the connection after the code executes
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                //Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();

                // parameterize query
                Command.CommandText = "update students set studentfname=@studentfname, studentlname=@studentlname, studentnumber = @studentnumber, enroldate = @enroldate where studentid=@id";
                Command.Parameters.AddWithValue("@studentfname", StudentData.First_Name);
                Command.Parameters.AddWithValue("@studentlname", StudentData.Last_Name);
                Command.Parameters.AddWithValue("@studentnumber", StudentData.Student_Num);
                Command.Parameters.AddWithValue("@enroldate", StudentData.S_Enroll_Date);

                Command.Parameters.AddWithValue("@id", StudentId);

                Command.ExecuteNonQuery();

            }

            return StudentInfo(StudentId);
        }

    }
}