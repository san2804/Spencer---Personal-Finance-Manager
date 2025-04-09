using System;
using System.Data;
using System.Data.SqlClient;

namespace PersonnelFinanceManager.Helpers
{
    class DatabaseManagerExpense
    {
        private string connectionString;

        public DatabaseManagerExpense()
        {
            // Direct connection string to the local .mdf database
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\PersonnelFinanceManager\PersonnelFinanceManager\FInanceManager.mdf;Integrated Security=True;";
        }

        public bool InsertExpense(int userId, string month, string amount, string category, string description)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Expenses (userid, month, amount, category, description) " +
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
        public DataTable GetExpenseDetails(int userId)
        {
            DataTable expenseDetails = new DataTable();
            string query = "SELECT Id AS 'Expense ID', month AS 'Month', amount AS 'Amount', category AS 'Category', description AS 'Description' " +
                           "FROM Expenses WHERE userid = @userid ORDER BY Id DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(expenseDetails);
                }
            }
            return expenseDetails;
        }

        public IncomeDetails GetExpenseDetailsById(int expenseId, int userId)
        {
            // Example implementation to fetch income details from the database
            string query = "SELECT month, amount, category, description FROM Expenses " +
                           "WHERE Id = @Id AND userid = @userid";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", expenseId);
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
        public DataTable GetExpenseCategoriesByUser(int userid)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT name FROM ExpenseCategories WHERE userid = @userid";

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

        public bool UpdateExpense(int expenseId, string month, string amount, string category, string description)
        {
            try
            {
                string query = "UPDATE Expenses " +
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
                        cmd.Parameters.AddWithValue("@Id", expenseId);

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
                throw new Exception($"An error occurred while updating the expense record: {ex.Message}", ex);
            }


        }
        public bool DeleteExpense(int expenseId)
        {
            try
            {
                string query = "DELETE FROM Expenses WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Id", expenseId);

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
                throw new Exception($"An error occurred while deleting the expense record: {ex.Message}", ex);
            }
        }
    }
}
