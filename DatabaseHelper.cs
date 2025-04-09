using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PersonnelFinanceManager
{
    class DatabaseHelper
    {
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\PersonnelFinanceManager\PersonnelFinanceManager\FInanceManager.mdf;Integrated Security=True";

        public static bool InsertUser(string name, DateTime dob, string contact, string city, string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (name, dob, contact, city, username, password) VALUES (@name, @dob, @contact, @city, @username, @password)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@dob", dob);
                        command.Parameters.AddWithValue("@contact", contact);
                        command.Parameters.AddWithValue("@city", city);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
    }
}
