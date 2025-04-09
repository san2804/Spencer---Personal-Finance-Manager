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
    public partial class AddAccount : Form
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = name_txt.Text.Trim();
            DateTime dob;
            if (!DateTime.TryParse(dob_txt.Text, out dob))
            {
                MessageBox.Show("Please enter a valid date of birth.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string contact = contact_txt.Text.Trim();
            string city = city_txt.Text.Trim();
            string username = username_txt.Text.Trim();
            string password = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isInserted = DatabaseHelper.InsertUser(name, dob, contact, city, username, password);

            if (isInserted)
            {
                MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFields()
        {
            name_txt.Clear();
            dob_txt.Clear();
            contact_txt.Clear();
            city_txt.Clear();
            username_txt.Clear();
            textBox1.Clear();
        }
    }
}
