using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonnelFinanceManager.Helpers;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace PersonnelFinanceManager
{
    public class DatabaseManager
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\PersonnelFinanceManager\PersonnelFinanceManager\FinanceManager.mdf;Integrated Security=True";

        public AccountDetails GetAccountDetailsById(int userId)
        {
            // Query to fetch account details from the Users table
            string query = "SELECT name, contact, city, username, password FROM Users WHERE Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                // Add parameter to prevent SQL injection
                command.Parameters.AddWithValue("@Id", userId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Map the database fields to the AccountDetails object
                        return new AccountDetails
                        {
                            Name = reader["name"].ToString(),
                            Contact = reader["contact"].ToString(),
                            City = reader["city"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString()
                        };
                    }
                }
            }

            return null; // Return null if no record is found
        }

        public int ValidateUser(string username, string password, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id FROM Users WHERE username = @username AND password = @password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        SqlDataReader Newreader = command.ExecuteReader();

                        if (Newreader.Read())
                        {
                            id = Newreader.GetInt32(0);
                            Newreader.Close();
                            return (id);
                        }

                        object result = command.ExecuteScalar();

                        // If a result is found, return the user ID; otherwise, return 0
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating user: {ex.Message}");
                return 0;
            }
        }

        public (string username, int userId) GetUserDetails(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT username, Id FROM Users WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", userId);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string username = reader["username"].ToString();
                                int id = Convert.ToInt32(reader["Id"]);
                                return (username, id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user details: {ex.Message}");
            }

            return (null, 0); // Return default values if no user is found
        }

        public bool UpdateAccounts(int userId, string name, string contact, string city, string username, string password)
        {
            try
            {
                string query = "UPDATE Users " +
                               "SET name = @name, contact = @contact, city = @city, username = @username, password = @password " +
                               "WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@Id", userId);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();

                        // Return true if at least one row was updated
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception($"An error occurred while updating the Account details: {ex.Message}", ex);
            }
        }

        public bool DeleteAccount(int userid)
        {
            try
            {
                string query = "DELETE FROM Users WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Id", userid);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();

                        // Return true if at least one row was deleted
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception($"An error occurred while deleting the income record: {ex.Message}", ex);
            }
        }

        public DataTable GetAccountDetails()
        {
            DataTable incomeDetails = new DataTable();
            string query = "SELECT Id AS 'User ID', name AS 'Name', contact AS 'Contact no.', city AS 'City', username AS 'Username', password AS 'Password' " +
                           "FROM Users ORDER BY Id ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(incomeDetails);
                }
            }
            return incomeDetails;
        }
        
    }
}