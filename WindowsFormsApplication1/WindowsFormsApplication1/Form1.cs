using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using Npgsql;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private NpgsqlConnection conn;
        string connstring = "Server=localhost; Port=5432; Database=Eda; User ID=postgres; Password=12345;";
        private NpgsqlCommand cmd;
        private string sql = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from login(:username,:password)";
                cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("username", txtusername.Text);
                cmd.Parameters.AddWithValue("password", txtpassword.Text);
                int result = (int)cmd.ExecuteScalar();
                

                conn.Close();
                if (result == 1)
                {
                    this.Hide();
                    new main(txtusername.Text).Show();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı","Hatalı Giriş",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    return;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message, "Bağlantıda sorun var", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            
            
        }
    }
}
