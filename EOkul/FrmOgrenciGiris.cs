using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOkul
{
    public partial class FrmOgrenciGiris : Form
    {
        public FrmOgrenciGiris()
        {
            InitializeComponent();
        }
        Sqlbaglanti bgl=new Sqlbaglanti();
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Close();

        }

        private void FrmOgrenciGiris_Load(object sender, EventArgs e)
        {
          
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from Tbl_Ogrenci where OgrTc=@p1 and OgrSifre=@p2", bgl.baglanti());
            command.Parameters.AddWithValue("@p1", MskTc.Text);
            command.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FrmOgrencidetay frm = new FrmOgrencidetay();
                frm.Tc = MskTc.Text;
                frm.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("Girdiğiniz Şifre Veya Tc Hatalı.lütfen Tekrar Deneyiniz..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TxtSifre.UseSystemPasswordChar = !TxtSifre.UseSystemPasswordChar;
        }
    }
}
