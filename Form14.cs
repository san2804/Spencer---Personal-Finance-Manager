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

namespace PersonnelFinanceManager
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void expenseid_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillExpenseDetails(); // Call the method to populate the text boxes
                e.Handled = true; // Prevents further processing of the Enter key
                e.SuppressKeyPress = true; // Stops the "ding" sound
            }
        }

        private void FillExpenseDetails()
        {
            try
            {
                // Validate and parse the Income ID
                if (!int.TryParse(expenseid_txt.Text, out int incomeId))
                {
                    MessageBox.Show($"Invalid Expense ID: {expenseid_txt.Text}. Please provide a valid ID.");
                    return;
                }

                // Retrieve income details from the database
                var databaseManager = new DatabaseManagerExpense();
                var incomeDetails = databaseManager.GetExpenseDetailsById(incomeId, GlobalState.LoggedInUserId);

                if (incomeDetails != null)
                {
                    // Populate the text boxes with the retrieved details
                    month_txt.Text = incomeDetails.Month;
                    amount_txt.Text = incomeDetails.Amount.ToString();
                    category_txt.Text = incomeDetails.Category;
                    description_txt.Text = incomeDetails.Description;
                    MessageBox.Show("Income details loaded successfully.");
                }
                else
                {
                    MessageBox.Show("No income record found with the provided ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching income details: {ex.Message}");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure the Income ID field is filled
                if (string.IsNullOrEmpty(expenseid_txt.Text))
                {
                    MessageBox.Show("Please enter an Expense ID to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse the Income ID
                if (!int.TryParse(expenseid_txt.Text, out int expenseId))
                {
                    MessageBox.Show("Invalid Expense ID. Please enter a valid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create an instance of the database manager and attempt to delete
                DatabaseManagerExpense databaseManager = new DatabaseManagerExpense();
                bool isDeleted = databaseManager.DeleteExpense(expenseId);

                // Show success or failure message
                if (isDeleted)
                {
                    MessageBox.Show("Expense record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields(); // Optional: Clear the input fields
                }
                else
                {
                    MessageBox.Show("Failed to delete expense record. Please check the Expense ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show($"An error occurred while deleting the expense record: {ex.Message}");
            }
        }
        private void ClearFields()
        {
            expenseid_txt.Clear();
            month_txt.Clear();
            amount_txt.Clear();
            category_txt.Clear();
            description_txt.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExpenseForm expenseForm = new ExpenseForm();
            expenseForm.Show();
            this.Hide();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();

        }

        private void Form14_Load(object sender, EventArgs e)
        {
            if (GlobalState.LoggedInUserId > 0)
            {
                var databaseManager = new DatabaseManager();
                var (username, userId) = databaseManager.GetUserDetails(GlobalState.LoggedInUserId);

                if (username != null)
                {
                    label6.Text = $"{username}";
                    label9.Text = $"User ID: {userId}";
                }
                else
                {
                    MessageBox.Show("Failed to retrieve user details.");
                }
            }
            else
            {
                MessageBox.Show("User is not logged in.");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.Show();
            this.Hide();
        }
    }
}
