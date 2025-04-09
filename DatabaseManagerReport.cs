using System;
using System.Data;
using System.Data.SqlClient;

namespace PersonnelFinanceManager.Helpers
{
    public class DatabaseManagerReport
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\PersonnelFinanceManager\PersonnelFinanceManager\FinanceManager.mdf;Integrated Security=True";

        public DataTable GetIncomeDetails(int userId)
        {
            DataTable dataTable = new DataTable();
            string query = @"
            SELECT 
                
                month,
                amount,
                category
                
            FROM 
                Income
            WHERE 
                userid = @userid
            ORDER BY 
                month, Id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable GetExpenseDetails(int userId)
        {
            DataTable dataTable = new DataTable();
            string query = @"
            SELECT 
                
                month,
                amount,
                category
                
            FROM 
                Expenses
            WHERE 
                userid = @userid
            ORDER BY 
                month, Id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }

    }

}
