using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonnelFinanceManager
{
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
        }

        private void FillAccountDetails()
        {
            try
            {
                // Validate and parse the Income ID
                if (!int.TryParse(userid_txt.Text, out int userId))
                {
                    MessageBox.Show($"Invalid User ID: {userid_txt.Text}. Please provide a valid ID.");
                    return;
                }

                // Retrieve income details from the database
                var databaseManager = new DatabaseManager();
                var accountDetails = databaseManager.GetAccountDetailsById(userId);

                if (accountDetails != null)
                {
                    // Populate the text boxes with the retrieved details
                    name_txt.Text = accountDetails.Name;
                    contact_txt.Text = accountDetails.Contact;
                    city_txt.Text = accountDetails.City;
                    username_txt.Text = accountDetails.Username;
                    password_txt.Text = accountDetails.Password;
                    MessageBox.Show("Account details loaded successfully.");
                }
                else
                {
                    MessageBox.Show("No Account record found with the provided ID.");
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

                int userId = int.Parse(userid_txt.Text);
                string name = name_txt.Text;
                string contact = contact_txt.Text;
                string city = city_txt.Text;
                string username = username_txt.Text;
                string password = password_txt.Text;

                // Update income details in the database
                DatabaseManager databaseManager = new DatabaseManager();
                bool isDeleted = databaseManager.DeleteAccount(userId);



                if (isDeleted)
                {
                    MessageBox.Show("Account details Deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to delete account details. Please check the User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
            



        private void userid_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillAccountDetails(); // Call the method to populate the text boxes
                e.Handled = true; // Prevents further processing of the Enter key
                e.SuppressKeyPress = true; // Stops the "ding" sound
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageAccounts manageAccounts = new ManageAccounts();
            manageAccounts.Show();
            this.Hide();
        }
        private void ClearFields()
        {
            userid_txt.Clear();
            name_txt.Clear();
            contact_txt.Clear();
            city_txt.Clear();
            username_txt.Clear();
            password_txt.Clear();
        }
    }
}
