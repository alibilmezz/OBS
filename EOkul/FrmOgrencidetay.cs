using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Security.AccessControl;

namespace EOkul
{
    public partial class FrmOgrencidetay : Form
    {
        public FrmOgrencidetay()
        {
            InitializeComponent();
        }
        public string Tc;
        DataSet1TableAdapters.Tbl_NotlarTableAdapter dc = new DataSet1TableAdapters.Tbl_NotlarTableAdapter();
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        Sqlbaglanti bgl=new Sqlbaglanti();
        private void FrmOgrencidetay_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            //öğrenci bilgilerini çekme
            LblTc.Text = Tc;
            SqlCommand cmd = new SqlCommand("Select OgrAd,OgrSoyad,OgrId from Tbl_Ogrenci Where OgrTc=@p1",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",Tc);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LblAd.Text = reader[0]+"";
                LblSoyad.Text = reader[1] + "";
                LblID.Text = reader[2]+"";
            }
            
            dataGridView1.DataSource= dc.OgrenciNot(int.Parse(LblID.Text));
            dataGridView1.Columns["DersID"].Visible = false;
            dataGridView1.Columns["OgrID"].Visible = false;
            //Kulüp bilgilerini çekme
            SqlCommand cmd1 = new SqlCommand("select OgrKulup From Tbl_Ogrenci Where OgrTc=@p1", bgl.baglanti());
            cmd1.Parameters.AddWithValue("@p1", Tc);
            SqlDataReader reader2 = cmd1.ExecuteReader();
            while (reader2.Read())
            {
                LblKulupıd.Text = reader2[0] + "";
            }
            SqlCommand cmd2 = new SqlCommand("select KulupAd From Tbl_Kulup Where KulupID=@p1", bgl.baglanti());
            cmd2.Parameters.AddWithValue("@p1",LblKulupıd.Text);
            SqlDataReader reader3 = cmd2.ExecuteReader();
            while (reader3.Read())
            {
                LblKulupad.Text = reader3[0] + "";
            }
            bgl.baglanti().Close();



        }

        private void LblTc_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
