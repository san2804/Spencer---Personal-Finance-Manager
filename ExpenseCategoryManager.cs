using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace PersonnelFinanceManager.Helpers
{
    class ExpenseCategoryManager
    {
        private string connectionString;

        public ExpenseCategoryManager()
        {
            // Direct connection string to the local .mdf database
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\PersonnelFinanceManager\PersonnelFinanceManager\FInanceManager.mdf;Integrated Security=True;";
        }
        public bool AddExpenseCategory(int userId, string categoryName)
        {
            try
            {
                // Define the SQL query to insert the new category
                string query = "INSERT INTO ExpenseCategories (userid, name) VALUES (@userid, @name)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters for userId and category name
                        command.Parameters.AddWithValue("@userid", userId);
                        command.Parameters.AddWithValue("@name", categoryName);

                        // Open connection and execute the command
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Expense category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true; // Return true to indicate success
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the expense category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false; // Return false to indicate failure
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public DataTable GetExpenseCategoriesByUser(int userid)
        {
            DataTable expenseCategoryData = new DataTable();
            string query = "SELECT Id, name FROM ExpenseCategories WHERE userid = @userid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", userid);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(expenseCategoryData);
                }
            }
            return expenseCategoryData;
        }

    }
}
