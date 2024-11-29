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
        /// The 3th link added to our web page is to teachers API (Swagger UI), 
        /// it redirects to a swagger apge that shows a list of all teachers in our created database.
        /// </summary>
        /// <example>
        /// GET api/Teacher/LTeachers -> [{"t_Id": 0,"fName": "string","lName": "string","hireDate": "2024-11-16T03:13:31.904Z","e_Number": "string","salary": 0}]
        /// GET api/Teacher/LTeachers -> [{"t_Id": 2,"fName": "Caitlin","lName": "Cummings","hireDate": "2014-06-10T00:00:00","e_Number": "T381","salary": 62.77}]
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
        /// The method adds a new teacher to the database by inserting a record into the teachers table and returns the ID of the inserted teacher
        /// </summary>
        /// <param name="TeacherData"> An object containing the details of the teacher to be added, including first name, last name, employee number, salary, and hire date </param>
        /// <returns>
        /// The ID of the newly inserted teacher record
        /// </returns>
        /// <example> 
        /// POST: api/TeacherAPI/AddTeacher -> 11
        /// assuming that 11th record is added
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
        /// The method deletes a teacher from the database using the teacher's ID provided in the request URL. It returns the number of rows affected.
        /// </summary>
        /// <param name="TeacherId"> The unique ID of the teacher to be deleted </param>
        /// <returns>
        /// The number of rows affected by the DELETE operation
        /// </returns>
        /// <example>
        /// DELETE: api/TeacherAPI/DeleteTeacher/11 -> 1
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

    }


}