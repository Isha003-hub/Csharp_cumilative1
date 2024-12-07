using Microsoft.AspNetCore.Http;
using System;
using Assignment__cumilative_1_csharp.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

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
        /// The third link on our webpage directs to the Teachers API (Swagger UI), 
        /// providing access to a Swagger page that displays a list of all teachers 
        /// available in the database.
        /// </summary>
        /// <example>
        /// GET api/Teacher/List -> [{"Teacher_Id": 0, "First_Name": "string", "Last_Name": "string", "HireDate": "2024-11-16T03:13:31.904Z", "Emp_Num": "string", "Salary": 0}]
        /// GET api/Teacher/List -> [{"Teacher_Id": 2, "First_Name": "Caitlin", "Last_Name": "Cummings", "HireDate": "2014-06-10T00:00:00", "Emp_Num": "T381", "Salary": 62.77}]
        /// </example>
        /// <returns>
        /// A list of all teachers retrieved from the "teachers" table in the "school" database.
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
                            Teacher_Id = t_id,
                            First_Name = fn,
                            Last_Name = ln,
                            HireDate = hdate,
                            Emp_Num = empnum,
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
        /// GET api/Teacher/TeacherInfo/3 -> {"Teacher_Id":3,"First_Name":"Caitlin","Last_Name":"Cummings", "Emp_Num" : "T381", "HireDate" : "2014-6-10", "Salary" : "62.77"}
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
                        SelTeachers.Teacher_Id = t_id;
                        SelTeachers.First_Name = fn;
                        SelTeachers.Last_Name = ln;
                        SelTeachers.HireDate = hdate;
                        SelTeachers.Emp_Num = empnum;
                        SelTeachers.Salary = salary;
                    }
                }
            }


            //Return the Information of the SelTeachers
            return SelTeachers;
        }

        /// <summary>
        /// This method adds a new teacher to the database by inserting a record into the "teachers" table 
        /// and returns the unique ID of the newly added teacher.
        /// </summary>
        /// <param name="TeacherData">
        /// An object containing the teacher's details, including first name, last name, employee number, salary, and hire date.
        /// </param>
        /// <returns>
        /// The unique ID of the newly inserted teacher record.
        /// </returns>
        /// <example>
        /// POST: api/TeacherAPI/AddTeacher -> 11  
        /// (Assuming the 11th record has been successfully added.)
        /// </example>



        [HttpPost(template: "AddTeacher")]
        public int AddTeacher([FromBody] Teacher TeacherData)
        {
            // 'using' keyword is used that will close the connection by itself after executing the code given inside
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Opening the Connection
                Connection.Open();

                // Establishing a new query for our database
                MySqlCommand Command = Connection.CreateCommand();

                // It contains the SQL query to insert a new teacher into the teachers table            
                Command.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, salary, hiredate) VALUES (@teacherfname, @teacherlname, @employeenumber, @salary, @hiredate)";

                Command.Parameters.AddWithValue("@teacherfname", TeacherData.First_Name);
                Command.Parameters.AddWithValue("@teacherlname", TeacherData.Last_Name);
                Command.Parameters.AddWithValue("@hiredate", TeacherData.HireDate);
                Command.Parameters.AddWithValue("@salary", TeacherData.Salary);
                Command.Parameters.AddWithValue("@employeenumber", TeacherData.Emp_Num);

                // It runs the query against the database and the new record is inserted
                Command.ExecuteNonQuery();

                // It fetches the ID of the newly inserted teacher record and converts it to an integer to be returned
                return Convert.ToInt32(Command.LastInsertedId);


            }

        }



        /// <summary>
        /// This method removes a teacher from the database based on the teacher's unique ID provided in the request URL. 
        /// It returns the number of rows affected by the deletion.
        /// </summary>
        /// <param name="TeacherId">
        /// The unique ID of the teacher to be removed.
        /// </param>
        /// <returns>
        /// The number of rows successfully deleted from the database.
        /// </returns>
        /// <example>
        /// DELETE: api/TeacherAPI/DeleteTeacher/11 -> 1  
        /// (Indicating that one record was successfully deleted.)
        /// </example>


        [HttpDelete(template: "DeleteTeacher/{TeacherId}")]

        public int DeleteTeacher(int TeacherId)
        {
            // 'using' keyword is used that will close the connection by itself after executing the code given inside
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Opening the Connection
                Connection.Open();

                // Establishing a new query for our database
                MySqlCommand Command = Connection.CreateCommand();

                // It contains the SQL query to delete a record from the teachers table based on the teacher's ID
                Command.CommandText = "DELETE FROM teachers WHERE teacherid=@id";
                Command.Parameters.AddWithValue("@id", TeacherId);

                // It runs the DELETE query and the number of affected rows is returned.
                return Command.ExecuteNonQuery();

            }

        }

        /// <summary>
        /// Updates an Teacher in the database. Data is Teacher object, request query contains ID
        /// </summary>
        /// <param name="TeacherData">Teacher Object</param>
        /// <param name="TeacherId">The Teacher ID primary key</param>
        /// <example>
        /// PUT: api/Teacher/UpdateTeacher/4
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// { "First_Name":"Isha", "Last_Name":"Shah", "Salary":12000, "HireDate":2024-12-05 12:00:00 AM,"Emp_Num":1002} -> 
        /// {"Teacher_Id":15, "First_Name":"Isha", "Last_Name":"shah", "Salary":12000, "HireDate":2024-12-05 12:00:00 AM,"Emp_Num":1002}
        /// </example>
        /// <returns>
        /// The updated Teacher object
        /// </returns>
        [HttpPut(template: "UpdateTeacher/{TeacherId}")]
        public Teacher UpdateTeacher(int TeacherId, [FromBody] Teacher TeacherData)
        {
            // 'using' will close the connection after the code executes
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                //Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();

                // parameterize query
                Command.CommandText = "update teachers set teacherfname=@teacherfname, teacherlname=@teacherlname, salary=@salary, employeenumber=@employeenumber, hiredate=@hiredate where teacherid=@id";
                Command.Parameters.AddWithValue("@teacherfname", TeacherData.First_Name);
                Command.Parameters.AddWithValue("@teacherlname", TeacherData.Last_Name);
                Command.Parameters.AddWithValue("@hiredate", TeacherData.HireDate);
                Command.Parameters.AddWithValue("@salary", TeacherData.Salary);
                Command.Parameters.AddWithValue("@employeenumber", TeacherData.Emp_Num);

                Command.Parameters.AddWithValue("@id", TeacherId);

                Command.ExecuteNonQuery();
            }
            return TeacherInfo(TeacherId);
        }
    }
}