using MySql.Data.MySqlClient;

namespace Assignment__cumilative_1_csharp.Models

{
	public class SchoolDbContext
	{
		// Input the details of the username, password, server, and port number to connect the server to the database
		private static string User { get { return "isha"; } }
		private static string Password { get { return "23ir$data"; } }
		private static string Database { get { return "school"; } }
		private static string Server { get { return "localhost"; } }
		private static string Port { get { return "3306"; } }

		// ConnectionString is a series of credentials which is used to connect to the database
		protected static string ConnectionString
		{
			get
			{
				// The "Convert Zero Datetime" setting in the database connection configuration 
				// ensures that dates with the value "0000-00-00" are returned as NULL. 
				// This allows the date to be properly interpreted in C# applications.

				return "server = " + Server
					+ "; user = " + User
					+ "; database = " + Database
					+ "; port = " + Port
					+ "; password = " + Password
					+ "; convert zero datetime = True";
			}
		}


		/// This method is used to establish a connection to the database.
		/// <summary>
		/// Returns an active connection to the database.
		/// </summary>
		/// <example>
		/// private SchoolDbContext Teachers = new SchoolDbContext();
		/// MySqlConnection Connection = Teachers.AccessDatabase();
		/// </example>
		/// <returns>A MySqlConnection object representing the database connection.</returns>

		public MySqlConnection AccessDatabase()
		{
			// An instance of the SchoolDbContext class is created to initialize an object.
			// This object represents a specific connection to the school database 
			// running on port 3306 of the localhost.
			return new MySqlConnection(ConnectionString);
		}
	}
}
