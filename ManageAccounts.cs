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
    public partial class ManageAccounts : Form
    {
        public ManageAccounts()
        {
            InitializeComponent();
        }

        private void LoadAccountDetails()
        {
            try
            {
                DatabaseManager dbManager = new DatabaseManager();
                // Replace GlobalState.LoggedInUserId with your actual way of obtaining the logged-in user's ID
                

                DataTable incomeData = dbManager.GetAccountDetails();

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
        private void ManageAccounts_Load(object sender, EventArgs e)
        {
            LoadAccountDetails();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddAccount addAccount = new AddAccount();
            addAccount.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateAccounts updateAccounts = new UpdateAccounts();
            updateAccounts.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form15 form15 = new Form15();
            form15.Show();
            this.Hide();
        }
    }
}
