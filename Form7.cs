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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
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

        private void Form7_Load(object sender, EventArgs e)
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void description_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void userid_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void incomedate_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void amount_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void category_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void incomeid_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillIncomeDetails(); // Call the method to populate the text boxes
                e.Handled = true; // Prevents further processing of the Enter key
                e.SuppressKeyPress = true; // Stops the "ding" sound
            }
        }

        private void incomeid_txt_TextChanged(object sender, EventArgs e)
        {

        }
        private void FillIncomeDetails()
        {
            try
            {
                // Validate and parse the Income ID
                if (!int.TryParse(incomeid_txt.Text, out int incomeId))
                {
                    MessageBox.Show($"Invalid Income ID: {incomeid_txt.Text}. Please provide a valid ID.");
                    return;
                }

                // Retrieve income details from the database
                var databaseManager = new DatabaseManagerIncome();
                var incomeDetails = databaseManager.GetIncomeDetailsById(incomeId, GlobalState.LoggedInUserId);

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
                
                int incomeId = int.Parse(incomeid_txt.Text);
                string month = month_txt.Text;
                string amount = amount_txt.Text;
                string category = category_txt.Text;
                string description = description_txt.Text;

                // Update income details in the database
                DatabaseManagerIncome databaseManager = new DatabaseManagerIncome();
                bool isUpdated = databaseManager.UpdateIncome(incomeId, month, amount, category, description);

                if (isUpdated)
                {
                    MessageBox.Show("Income details updated successfully.","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update income details. Please check the Income ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void month_txt_TextChanged(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
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
