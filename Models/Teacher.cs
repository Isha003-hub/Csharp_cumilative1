namespace Assignment__cumilative_1_csharp.Models
{
    // Created a Teacher Class that defines different properties of the Teacher table
    // This definition is used to create different objects for each row in that table
    // The properties of that each object is then accessed and are sent to view to display on the respective webpages
    // Here, Teacher 'Class' has 6 properties (Teacher_Id, First_Name, Last_Name, HireDate, Emp_Num, Salary)
    // these are accessed by Controllers and then retrived to View to display that properties information on the web page


    public class Teacher
    {
        // Unique identifier for each teacher. It is used as the primary key in a database.
        public int Teacher_Id { get; set; }

        // First name of the teacher. It stores the teacher's first name as a string.
        public string First_Name { get; set; }

        // Last name of the teacher. It stores the teacher's last name as a string.
        public string Last_Name { get; set; }

        // The date when the teacher was hired. It is used to track employment start date.
        public DateTime HireDate { get; set; }

        // It is a unique employee number assigned to each teacher. 
        public string Emp_Num { get; set; }

        // It is the salary of the teacher. It is stored as a decimal to accommodate monetary values.
        public decimal Salary { get; set; }
    }
}