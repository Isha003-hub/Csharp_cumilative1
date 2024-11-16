namespace Assignment__cumilative_1_csharp.Models
{
	public class Course
	{
		// Unique identifier for each Course. It is used as the primary key in a database.
		public int C_Id { get; set; }

		// Name of the teacher. It stores the Course's name as a string.
		public string CCode { get; set; }

		// we are using inner join to connet teacher table using teacher_id abd displaying teacher fname for each
		// courses(linked to teacher table).
		public int T_Id { get; set; }

		// The Start date and end date of a course. It is used to track course duration.
		// Stored as datetime
		public DateTime S_Date { get; set; }
		public DateTime E_Date { get; set; }

		// It is a Course name. stored as a string
		public string CName { get; set; }
	}
}
