using System;
using System.Data;
using System.Data.SqlClient;

namespace PersonnelFinanceManager.Helpers
{
    public class DatabaseManagerIncome
    {
        private string connectionString;

        public DatabaseManagerIncome()
        {
            // Direct connection string to the local .mdf database
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\PersonnelFinanceManager\PersonnelFinanceManager\FInanceManager.mdf;Integrated Security=True;";
        }
        


        public DataTable GetIncomeForMonth(int userId, string monthName)
        {
            DataTable incomeData = new DataTable();
            string query = "SELECT category, SUM(amount) AS TotalAmount " +
                           "FROM Income " +
                           "WHERE userid = @userid AND month = @monthName " +
                           "GROUP BY category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@monthName", monthName);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(incomeData);
                }
            }
            return incomeData;
        }

        public IncomeDetails GetIncomeDetailsById(int incomeId, int userId)
        {
            // Example implementation to fetch income details from the database
            string query = "SELECT month, amount, category, description FROM Income " +
                           "WHERE Id = @Id AND userid = @userid";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", incomeId);
                command.Parameters.AddWithValue("@userid", userId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new IncomeDetails
                        {
                            Month = reader["month"].ToString(),
                            Amount = Convert.ToDecimal(reader["amount"]),
                            Category = reader["category"].ToString(),
                            Description = reader["description"].ToString()
                        };
                    }
                }
            }

            return null; // Return null if no record is found
        }

        public bool InsertIncome(int userId, string month, string amount, string category, string description)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Income (userid, month, amount, category, description) " +
                               "VALUES (@userid, @month, @amount, @category, @description)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userId);
                    command.Parameters.AddWithValue("@month", month);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@description", description);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public DataTable GetIncomeDetails(int userId)
        {
            DataTable incomeDetails = new DataTable();
            string query = "SELECT Id AS 'Income ID', month AS 'Month', amount AS 'Amount', category AS 'Category', description AS 'Description' " +
                           "FROM Income WHERE userid = @userid ORDER BY Id DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(incomeDetails);
                }
            }
            return incomeDetails;
        }
        public DataTable GetIncomeCategoriesByUser(int userid)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT name FROM IncomeCategories WHERE userid = @userid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userid);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        public bool UpdateIncome(int incomeId, string month, string amount, string category, string description)
        {
            try
            {
                string query = "UPDATE Income " +
                               "SET month = @month, amount = @amount, category = @category, description = @description " +
                               "WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@Id", incomeId);

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
                throw new Exception($"An error occurred while updating the income record: {ex.Message}", ex);
            }
        }
        public bool DeleteIncome(int incomeId)
        {
            try
            {
                string query = "DELETE FROM Income WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Id", incomeId);

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

        


    }
}


