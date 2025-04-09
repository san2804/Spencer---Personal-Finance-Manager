using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using PersonnelFinanceManager.Helpers;

namespace PersonnelFinanceManager
{
    public partial class Form5 : Form
    {
        private DatabaseManagerIncome databaseManagerIncome;
        public Form5()
        {
            InitializeComponent();
            databaseManagerIncome = new DatabaseManagerIncome();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            userid_txt.ReadOnly = false;

            // Optionally pre-fill the User ID if GlobalState.LoggedInUserId is valid
            if (GlobalState.LoggedInUserId > 0)
            {
                userid_txt.Text = GlobalState.LoggedInUserId.ToString();
            }
            else
            {
                MessageBox.Show("Please manually enter the User ID.");
            }

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            // Define the corner radius
            int cornerRadius = 15;

            // Create a GraphicsPath to define rounded corners
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;

            // Get the panel dimensions
            int width = panel3.Width;
            int height = panel3.Height;

            // Top-left arc
            path.AddArc(0, 0, diameter, diameter, 180, 90);
            // Top-right arc
            path.AddArc(width - diameter, 0, diameter, diameter, 270, 90);
            // Bottom-right arc
            path.AddArc(width - diameter, height - diameter, diameter, diameter, 0, 90);
            // Bottom-left arc
            path.AddArc(0, height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            // Set the region of the panel to the GraphicsPath
            panel3.Region = new Region(path);

            // Optional: Draw a border
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }
         private void ClearForm()
           {
                // Clear the form fields
                if (GlobalState.LoggedInUserId == 0)
                {
                    userid_txt.Clear();
                }
                month_txt.SelectedIndex = -1;
                amount_txt.Clear();
                category_txt.SelectedIndex = -1;
                description_txt.Clear();
           }

        private void userid_txt_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Incomedate_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void amount_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void category_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void description_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate if all input fields are filled
                if (string.IsNullOrEmpty(userid_txt.Text) ||
                    string.IsNullOrEmpty(month_txt.Text) ||
                    string.IsNullOrEmpty(amount_txt.Text) ||
                    string.IsNullOrEmpty(category_txt.Text) ||
                    string.IsNullOrEmpty(description_txt.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate User ID
                if (!int.TryParse(userid_txt.Text, out int userId))
                {
                    MessageBox.Show($"Invalid User ID: {userid_txt.Text}. Please enter a numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate Income Date
                string month = month_txt.SelectedItem.ToString();

                // Validate Amount
                if (!decimal.TryParse(amount_txt.Text, out decimal amount))
                {
                    MessageBox.Show($"Invalid Amount: {amount_txt.Text}. Please enter a numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Retrieve Category and Description
                string category = category_txt.Text.Trim();
                string description = description_txt.Text.Trim();

                // Insert income record into the database
                bool isInserted = databaseManagerIncome.InsertIncome(
                    userId,
                    month, 
                    amount.ToString(),                // Convert amount to string
                    category,
                    description
                );

                if (isInserted)
                {
                    MessageBox.Show("Income record inserted successfully!","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm(); // Optionally clear the form after success
                }
                else
                {
                    MessageBox.Show("Failed to insert income record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load_1(object sender, EventArgs e)
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

            month_txt.Items.AddRange(new string[]
    {
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    });
            month_txt.DropDownStyle = ComboBoxStyle.DropDownList;
            

            // Load income categories into the comboBox
            LoadIncomeCategories();
        }

        private void label9_Click(object sender, EventArgs e)
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

        private void month_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void month_txt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void category_txt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadIncomeCategories()
        {
            try
            {
                // Get the logged-in user ID
                int userId = GlobalState.LoggedInUserId;

                // Validate User ID
                if (userId <= 0)
                {
                    MessageBox.Show("Invalid User ID. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Fetch categories from the database
                DataTable incomeCategories = databaseManagerIncome.GetIncomeCategoriesByUser(userId);

                // Clear existing items in the comboBox
                category_txt.Items.Clear();

                // Add fetched categories to the comboBox
                foreach (DataRow row in incomeCategories.Rows)
                {
                    category_txt.Items.Add(row["name"].ToString());
                }

                // Set default text for the comboBox
                if (category_txt.Items.Count > 0)
                {
                    category_txt.SelectedIndex = 0; // Set to the first category by default
                }
                else
                {
                    MessageBox.Show("No income categories found for the user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading income categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

