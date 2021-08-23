using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApplication1
{
    public partial class main : Form
    {
        public main(string username)
        {
            this.username = username;
            InitializeComponent();
        }
        private NpgsqlConnection conn;
        string connstring = "Server=localhost; Port=5432; Database=Eda; User ID=postgres; Password=12345;";
        private NpgsqlCommand cmd;
        private string sql = null;

        private string username;
        private void main_Load(object sender, EventArgs e)
        {
            lbluser.Text = lbluser.Text + username;
            conn = new NpgsqlConnection(connstring);
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sorgu = "select * from tbl_oyuncak";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu,conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sorgu = "select * from tbl_marka";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            NpgsqlCommand ekle = new NpgsqlCommand("insert into tbl_oyuncak (oyuncak_id, alinan_fiyat, takas_degeri, yipranma_orani, marka_id, kullanici_id, oyuncak_adi, yasgrubu, aciklama) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)", conn);
            ekle.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            ekle.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text));
            ekle.Parameters.AddWithValue("@p3", int.Parse(textBox4.Text));
            ekle.Parameters.AddWithValue("@p4", int.Parse(textBox5.Text));
            ekle.Parameters.AddWithValue("@p5", int.Parse(textBox8.Text));
            ekle.Parameters.AddWithValue("@p6", int.Parse(textBox9.Text));
            ekle.Parameters.AddWithValue("@p7", textBox2.Text);
            ekle.Parameters.AddWithValue("@p8", textBox6.Text);
            ekle.Parameters.AddWithValue("@p9", textBox7.Text);
            ekle.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Yeni Oyuncak Tanımlaması Başarılı Bir Şekilde Gerçekleşmiştir...:)");

        }

        private void button7_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sorgu = "select hesap_id, kullanici_adi from tbl_hesap";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sorgu = "select * from tbl_oyuncak where oyuncak_adi like'%" + textBox2.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
            conn.Open();
            NpgsqlCommand sil = new NpgsqlCommand("Delete from tbl_oyuncak where oyuncak_adi=@p7", conn);
            sil.Parameters.AddWithValue("@p7", textBox2.Text);
            sil.ExecuteNonQuery();
            conn.Close();
            }
            MessageBox.Show("Seçilen Oyuncak Silinmiştir...:)","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Stop);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            NpgsqlCommand guncelle = new NpgsqlCommand("Update tbl_oyuncak set alinan_fiyat=@p2, takas_degeri=@p3 where oyuncak_adi=@p7", conn);
            guncelle.Parameters.AddWithValue("@p7", textBox2.Text);
            guncelle.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text));
            guncelle.Parameters.AddWithValue("@p3", int.Parse(textBox4.Text));
            guncelle.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Oyuncak Güncellemesi Başarılı Bir Şekilde Gerçekleşmiştir...:)", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
