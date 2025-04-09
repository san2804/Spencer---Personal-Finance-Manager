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
using System.Data.SqlClient;
using PersonnelFinanceManager.Helpers;


namespace PersonnelFinanceManager
{
    public partial class Form1 : Form
    {

        private DatabaseManager databaseManager;
        public Form1()
        {
            InitializeComponent();
            databaseManager = new DatabaseManager();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form Loaded Successfully.");
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            CustomControls.ApplyRoundedCorners(sender as Button, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();

            // Show Form2
            form2.Show();
            this.Close();


        }

        private void signin_btn_Click(object sender, EventArgs e)
        {
            int id = 0;
            string username = username_txt.Text.ToString();
            string password = password_txt.Text.ToString();

            // Check if the user is the admin
            if (username == "admin" && password == "admin123")
            {
                // Redirect to Admin.cs form
                MessageBox.Show("Welcome, Admin!", "Admin Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Admin adminForm = new Admin(); // Assuming Admin.cs form is named "Admin"
                adminForm.Show();
                this.Hide();
                return; // Exit the method to prevent further processing
            }

            // Validate regular user
            int userId = databaseManager.ValidateUser(username, password, id);

            if (userId > 0)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GlobalState.LoggedInUserId = userId;

                Form3 form3 = new Form3(); 
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void password_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
