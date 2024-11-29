namespace Assignment__cumilative_1_csharp.Models
{

    // Created a Student Class that defines different properties of the Student table
    // This definition is used to create different objects for each row in that table
    // The properties of that each object is then accessed and are sent to view to display on the respective webpages
    // Here, Student 'Class' has 5 properties (Student_Id, First_Name, Last_Name, Student_Num, S_Enroll_Date)
    // these are accessed by Controllers and then retrived to View to display that properties information on the web page
    public class Student
    {
        // Unique identifier for each student. It is used as the primary key in a database.
        public int Student_Id { get; set; }

        // First name of the student. It stores the student's first name as a string.
        public string First_Name { get; set; }

        // Last name of the student. It stores the student's last name as a string.
        public string Last_Name { get; set; }

        // The student roll number. it is stored as string.
        public string Student_Num { get; set; }

        // The date when the Student was enrolled. It is Stored as DateTime.
        public DateTime S_Enroll_Date { get; set; }
    }
}