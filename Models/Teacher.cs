namespace Assignment__cumilative_1_csharp.Models
{
	// Created a Teacher Class that defines different properties of the Teacher table which is used to create objects to represents each row in the table.
	// The properties of each object are accessed and sent to the View for display on the respective webpages.
	// The Teacher class contains the following 6 properties: 
	// - tid
	// - fName
	// - lName
	// - hiredt
	// - enumber
	// - salary
	// These properties are accessed by Controllers and then retrieved by the View 
	// to display the information on the web page.


	public class Teacher
	{
		// Unique identifier for each teacher. It is used as the primary key in a database.
		public int tid { get; set; }

		// First name of the teacher. It stores the teacher's first name as a string.
		public string fname { get; set; }

		// Last name of the teacher. It stores the teacher's last name as a string.
		public string lname{ get; set; }

		// The date when the teacher was hired. It is used to track employment start date.
		public DateTime hiredt { get; set; }

		// It is a unique employee number assigned to each teacher. 
		public string enumber { get; set; }

		// It is the salary of the teacher. It is stored as a decimal to accommodate monetary values.
		public decimal salary { get; set; }
	}
}


