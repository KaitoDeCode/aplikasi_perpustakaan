using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using aplikasi_perpustakaan.screen;
using aplikasi_perpustakaan.utils;
using MySql.Data.MySqlClient;

namespace aplikasi_perpustakaan
{
    public partial class LoginForm : Form
    {
 
        private Connection server;
        public LoginForm()
        {
            InitializeComponent();
            this.server = new Connection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            login(email.Text, password.Text);
        }

        private void login(String email, String password)
        {
            try
            {
                String query = "select * from users where email=@email and password=@password";
                MySqlCommand cmd = this.server.query(query);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    MessageBox.Show("Berhasil login", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Hide();
                    reader.Close();
                }
                else
                {
                    // Data tidak ditemukan, lakukan tindakan yang sesuai
                    MessageBox.Show("Data tidak ditemukan", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            email.Text = "";
            password.Text = "";
        }
    }
}
