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

namespace EOkul
{
    public partial class FrmOgretmendetay : Form
    {
        public FrmOgretmendetay()
        {
            InitializeComponent();
        }
        public string Tc;
        
        Sqlbaglanti bgl=new Sqlbaglanti();
        private void FrmOgretmendetay_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select OgretmenAdsoyad,OgretmenID from Tbl_Ogretmen where OgretmenTc=@p1",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",Tc);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label1.Text = ""+dr[0];
                label2.Text=""+dr[1];
            }
            bgl.baglanti().Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmOgretmenGiris frm=new FrmOgretmenGiris();
            frm.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
         KulupIslem frm=new KulupIslem();
            frm.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDersler frm=new FrmDersler();
            frm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
           FrmOgrencıIslerı fr=new FrmOgrencıIslerı();
            fr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Frmsınavnotu fr =new Frmsınavnotu();
            fr.ID = label2.Text;
            fr.Show();
        }
    }
}
