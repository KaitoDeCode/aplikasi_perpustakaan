using aplikasi_perpustakaan.utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aplikasi_perpustakaan.screen
{
    public partial class Peminjam : Form
    {
        private Connection server;
        public Peminjam()
        {
            InitializeComponent();
            this.server = new Connection();
            GetPeminjam();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            inputNama.Text = gridPeminjaman.Rows[e.RowIndex].Cells[1].Value.ToString();
            inputEmail.Text = gridPeminjaman.Rows[e.RowIndex].Cells[2].Value.ToString();
            inputAlamat.Text = gridPeminjaman.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createPeminjaman(inputNama.Text,inputEmail.Text,inputAlamat.Text);
        }

        private void GetPeminjam()
        {
            String query = "select * from peminjam";
            MySqlCommand cmd = this.server.query(query);
            MySqlDataReader reader = cmd.ExecuteReader();
           
            DataTable data = new DataTable();
            data.Load(reader);
            gridPeminjaman.DataSource = data;
            reader.Close();
        }



        private void createPeminjaman(String nama,String email,String alamat)
        {
            try
            {
                string query = "insert into peminjam (nama,email,alamat) values(@nama,@email,@alamat)";
                string query2 = "select * from peminjam where email=@email";

                MySqlCommand cmd2 = this.server.query(query2);
                cmd2.Parameters.AddWithValue("@email", email);
                MySqlDataReader reader2 = cmd2.ExecuteReader();

                if (reader2.HasRows)
                {
                    DialogResult result = MessageBox.Show("Data email sudah digunakan, Apakah anda ingin memperbarui data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    reader2.Close();
                    string queryUpdate = "update peminjam set nama=@nama, alamat=@alamat where email=@email";
                    MySqlCommand cmdUpdate = this.server.query(queryUpdate);
                    cmdUpdate.Parameters.AddWithValue("@nama", nama);
                    cmdUpdate.Parameters.AddWithValue("@alamat", alamat);
                    cmdUpdate.Parameters.AddWithValue("@email", email);
                    MySqlDataReader readerUpdate = cmdUpdate.ExecuteReader();
                    MessageBox.Show("Berhasil Mengupdate Peminjam", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    readerUpdate.Close();
                    GetPeminjam();
                }
                else
                {
                    reader2.Close();
                    MySqlCommand cmd = this.server.query(query);
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@alamat", alamat);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Berhasil Membuat Peminjam", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    GetPeminjam();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Apakah anda ingin melanjutkan ini, apabila data ditemukan akan dihapus secara permanen?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
                String query = "delete from peminjam where email=@email";
                MySqlCommand cmd = this.server.query(query);
                cmd.Parameters.AddWithValue("@email",inputEmail.Text);
                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                  MessageBox.Show("Berhasil menghapus peminjam", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tidak ada data yang terhapus", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                GetPeminjam();
            }catch(Exception ex)
            {
               MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
