namespace Assignment__cumilative_1_csharp.Models
{
	public class Student
	{
		// Unique identifier for each student. It is used as the primary key in a database.
		public int S_Id { get; set; }

		// First name of the student. It stores the student's first name as a string.
		public string S_FName { get; set; }

		// Last name of the student. It stores the student's last name as a string.
		public string S_LName { get; set; }

		// The student roll number. it is stored as string.
		public string S_Num { get; set; }

		// The date when the Student was enrolled. It is Stored as DateTime.
		public DateTime S_E_Date { get; set; }
	}
}
