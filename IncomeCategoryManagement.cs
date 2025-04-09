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
    public partial class IncomeCategoryManagement : Form
    {
        public IncomeCategoryManagement()
        {
            InitializeComponent();
        }

        private void LoadIncomeCategories()
        {
            try
            {
                IncomeCategoryManager dbManager = new IncomeCategoryManager();
                // Replace GlobalState.LoggedInUserId with your actual way of obtaining the logged-in user's ID
                int loggedInUserId = GlobalState.LoggedInUserId;

                // Get the income categories from the database
                DataTable incomeCategoryData = dbManager.GetIncomeCategoriesByUser(loggedInUserId);

                // Check if data is available
                if (incomeCategoryData.Rows.Count > 0)
                {
                    // Set the DataGridView's data source to display the income categories
                    dataGridView1.DataSource = incomeCategoryData;
                }
                else
                {
                    MessageBox.Show("No income categories found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Display error message if any exception occurs
                MessageBox.Show($"Error loading income categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddIncomeCategory()
        {
            try
            {
                int userId = GlobalState.LoggedInUserId; // Replace with actual logged-in user ID
                string categoryName = incomecategory_txt.Text.Trim(); // Get the category name from the textbox

                // Validate the category name
                if (string.IsNullOrEmpty(categoryName))
                {
                    MessageBox.Show("Please enter a category name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IncomeCategoryManager manager = new IncomeCategoryManager();

                // Insert the category into the database
                bool result = manager.AddIncomeCategory(userId, categoryName);

                if (result)
                {
                    MessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadIncomeCategories(); // Reload the income categories to refresh the data grid
                }
                else
                {
                    MessageBox.Show("Error adding category. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IncomeCategoryManagement_Load(object sender, EventArgs e)
        {
            if (GlobalState.LoggedInUserId > 0)
            {
                var databaseManager = new DatabaseManager();
                var (username, userId) = databaseManager.GetUserDetails(GlobalState.LoggedInUserId);

                if (username != null)
                {
                    label6.Text = $"{username}";
                    label9.Text = $"User ID:{userId}";
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
            LoadIncomeCategories();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void incomecategory_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddIncomeCategory();
        }

        private void userid_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
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
