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
using System.Windows.Forms.DataVisualization.Charting;

namespace PersonnelFinanceManager
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();


        }
        
        private void LoadIncomePieChart()
        {
            try
            {
                DatabaseManagerReport dbManager = new DatabaseManagerReport();
                int loggedInUserId = GlobalState.LoggedInUserId; // Replace with your actual way of obtaining the user ID

                DataTable incomeData = dbManager.GetIncomeDetails(loggedInUserId);

                if (incomeData.Rows.Count > 0)
                {
                    chartIncome.Series.Clear();
                    chartIncome.ChartAreas.Clear();

                    // Set up chart area
                    ChartArea chartArea = new ChartArea("IncomeArea");
                    chartIncome.ChartAreas.Add(chartArea);

                    // Create series
                    Series series = new Series("Income");
                    series.ChartType = SeriesChartType.Pie;
                    chartIncome.Series.Add(series);

                    // Populate the series with data
                    foreach (DataRow row in incomeData.Rows)
                    {
                        string category = row["category"].ToString();
                        decimal amount = Convert.ToDecimal(row["amount"]);

                        series.Points.AddXY(category, amount);
                    }

                    // Customize chart
                    Legend legend = new Legend("Default");
                    chartIncome.Legends.Add(legend);

                    chartIncome.Titles.Clear();
                    chartIncome.Titles.Add("Income Breakdown by Category");
                }
                else
                {
                    MessageBox.Show("No income records available for the pie chart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading income pie chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void LoadExpensesPieChart()
        {
            try
            {
                DatabaseManagerReport dbManager = new DatabaseManagerReport();
                int loggedInUserId = GlobalState.LoggedInUserId; // Replace with your actual way of obtaining the user ID

                DataTable expensesData = dbManager.GetExpenseDetails(loggedInUserId);

                if (expensesData.Rows.Count > 0)
                {
                    chartExpenses.Series.Clear();
                    chartExpenses.ChartAreas.Clear();
                    chartExpenses.Legends.Clear();

                    // Set up chart area
                    ChartArea chartArea = new ChartArea("ExpensesArea");
                    chartExpenses.ChartAreas.Add(chartArea);

                    // Create series
                    Series series = new Series("Expenses")
                    {
                        ChartType = SeriesChartType.Pie // Change chart type as needed
                    };

                    chartExpenses.Series.Add(series);

                    // Populate the series with data
                    foreach (DataRow row in expensesData.Rows)
                    {
                        string category = row["category"].ToString();
                        decimal amount = Convert.ToDecimal(row["amount"]);

                        series.Points.AddXY(category, amount);
                    }

                    
                    Legend legend = new Legend("Default");
                    chartExpenses.Legends.Add(legend);

                    // Customize chart
                    chartExpenses.Titles.Clear();
                    chartExpenses.Titles.Add("Expenses Breakdown by Category");
                }
                else
                {
                    MessageBox.Show("No expenses records available for the chart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expenses chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {



        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Define the corner radius
            int cornerRadius = 20;

            // Create a GraphicsPath to define rounded corners
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;

            // Get the panel dimensions
            int width = panel2.Width;
            int height = panel2.Height;

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
            panel2.Region = new Region(path);

            // Optional: Draw a border
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
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

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            // Define the corner radius for rounded corners
            int cornerRadius = 30;

            // Create a GraphicsPath to define the rounded corners
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;

            // Get the button dimensions
            int width = btn.Width;
            int height = btn.Height;

            // Add rounded corners to the path
            path.AddArc(0, 0, diameter, diameter, 180, 90); // Top-left
            path.AddArc(width - diameter, 0, diameter, diameter, 270, 90); // Top-right
            path.AddArc(width - diameter, height - diameter, diameter, diameter, 0, 90); // Bottom-right
            path.AddArc(0, height - diameter, diameter, diameter, 90, 90); // Bottom-left
            path.CloseFigure();

            // Set the region of the button to the GraphicsPath to apply rounded corners
            btn.Region = new Region(path);

            // Optional: Draw a border around the button
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }

            // Optional: You can also fill the button background by adding the following:
            // using (Brush brush = new SolidBrush(btn.BackColor))
            // {
            //     e.Graphics.FillPath(brush, path);
            // }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            // Show Form2
            form1.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            int cornerRadius = 15;

            // Create a GraphicsPath to define rounded corners
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;

            // Get the panel dimensions
            int width = panel4.Width;
            int height = panel4.Height;

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
            panel4.Region = new Region(path);

            // Optional: Draw a border
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            int cornerRadius = 15;

            // Create a GraphicsPath to define rounded corners
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;

            // Get the panel dimensions
            int width = panel5.Width;
            int height = panel5.Height;

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
            panel5.Region = new Region(path);

            // Optional: Draw a border
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form3_Load_1(object sender, EventArgs e)
        {
            if (GlobalState.LoggedInUserId > 0)
            {
                var databaseManager = new DatabaseManager();
                var (username, userId) = databaseManager.GetUserDetails(GlobalState.LoggedInUserId);

                if (username != null)
                {
                    lbl.Text = $"{username}";
                    label8.Text = $"User ID:{userId}";
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

            LoadIncomePieChart();
            LoadExpensesPieChart();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.Show();
            this.Hide();
        }

        private void chartIncome_Click(object sender, EventArgs e)
        {

        }

        private void chartExpenses_Click(object sender, EventArgs e)
        {

        }
    }
    
    
}