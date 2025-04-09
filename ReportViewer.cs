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
    public partial class ReportViewer : Form
    {
        public ReportViewer()
        {
            InitializeComponent();
        }
        private void LoadIncomeDetails()
        {
            try
            {
                DatabaseManagerReport dbManager = new DatabaseManagerReport();
                int loggedInUserId = GlobalState.LoggedInUserId; // Replace with your actual way of obtaining the user ID

                DataTable incomeData = dbManager.GetIncomeDetails(loggedInUserId);

                if (incomeData.Rows.Count > 0)
                {
                    dataGridViewIncome.DataSource = incomeData;

                    // Customize column headers for income table
                    
                    dataGridViewIncome.Columns["month"].HeaderText = "Month";
                    dataGridViewIncome.Columns["amount"].HeaderText = "Amount";
                    dataGridViewIncome.Columns["category"].HeaderText = "Category";
                    
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

        private void LoadExpenseDetails()
        {
            try
            {
                DatabaseManagerReport dbManager = new DatabaseManagerReport();
                int loggedInUserId = GlobalState.LoggedInUserId; // Replace with your actual way of obtaining the user ID

                DataTable expenseData = dbManager.GetExpenseDetails(loggedInUserId);

                if (expenseData.Rows.Count > 0)
                {
                    dataGridViewExpense.DataSource = expenseData;

                    // Customize column headers for expense table
                    
                    dataGridViewExpense.Columns["month"].HeaderText = "Month";
                    dataGridViewExpense.Columns["amount"].HeaderText = "Amount";
                    dataGridViewExpense.Columns["category"].HeaderText = "Category";
                    
                }
                else
                {
                    MessageBox.Show("No expense records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expense details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // Add a legend to the chart
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


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (GlobalState.LoggedInUserId > 0)
            {
                var databaseManager = new DatabaseManager();
                var (username, userId) = databaseManager.GetUserDetails(GlobalState.LoggedInUserId);

                if (username != null)
                {
                    label6.Text = $"{username}";
                    label2.Text = $"User ID:{userId}";
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
            LoadExpenseDetails();
            LoadIncomePieChart();
            LoadExpensesPieChart();

        }

        private void dataGridViewIncome_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewExpense_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chartIncome_Click(object sender, EventArgs e)
        {

        }

        private void chartExpenses_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
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
    }
}
