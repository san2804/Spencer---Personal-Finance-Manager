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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
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

        private void Form6_Load(object sender, EventArgs e)
        {
            
        }
        private void LoadIncomeDetails()
        {
            try
            {
                DatabaseManagerIncome dbManager = new DatabaseManagerIncome();
                // Replace GlobalState.LoggedInUserId with your actual way of obtaining the logged-in user's ID
                int loggedInUserId = GlobalState.LoggedInUserId;

                DataTable incomeData = dbManager.GetIncomeDetails(loggedInUserId);

                if (incomeData.Rows.Count > 0)
                {
                    dataGridView1.DataSource = incomeData;
                }
                else
                {
                    MessageBox.Show("No income records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading income details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form6_Load_1(object sender, EventArgs e)
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
            
            LoadIncomeDetails();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
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
