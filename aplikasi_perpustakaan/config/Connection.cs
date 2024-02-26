using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace aplikasi_perpustakaan.utils
{
 
    internal class Connection
    {
        public MySqlConnection conn;
        private MySqlDataReader reader;
        public Connection()
        {
            this.conn = new MySqlConnection();
            connectDb();
        }

        private void connectDb()
        {
            try
            {
                string conn = "server=localhost;uid=root;pwd='';database=perpustakaan_db";
                this.conn.ConnectionString = conn;
                this.conn.Open();
                //MessageBox.Show("Success connection", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void quer()
    }

}
