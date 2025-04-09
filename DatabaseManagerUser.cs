
using System;
using System.Data.SqlClient;

namespace PersonnelFinanceManager.Helpers
{
    public class DatabaseManagerUser
    {
        private readonly string connectionString;

        public DatabaseManagerUser()
        {
            // Direct connection string to the local .mdf database
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\PersonnelFinanceManager\PersonnelFinanceManager\FInanceManager.mdf;Integrated Security=True;";
        }

        // Method to get username and user ID
        public (string username, int userId) GetUserInfo(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT username, id FROM Users WHERE id = @userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (reader["username"].ToString(), Convert.ToInt32(reader["id"]));
                        }
                    }
                }
            }
            return (null, 0); // Return null or 0 if no user is found
        }
    }
}
