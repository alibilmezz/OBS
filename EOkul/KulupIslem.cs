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
    public partial class KulupIslem : Form
    {
        public KulupIslem()
        {
            InitializeComponent();
        }
        Sqlbaglanti bgl=new Sqlbaglanti();
        private void button5_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
        void listele()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Kulup", bgl.baglanti());
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        void temizle()
        {
            TxtAd.Text = string.Empty;
            TxtId.Text = string.Empty;
        }
        private void KulupIslem_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            listele();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtAd.Text))
            {
                MessageBox.Show("Kulüp adı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (TxtAd.Text.Length < 3 || TxtAd.Text.Length > 50) 
            {
                MessageBox.Show("Kulüp adı en az 3, en fazla 50 karakter uzunluğunda olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Tbl_Kulup (KulupAd) values (@p1)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
                cmd.ExecuteNonQuery();
                listele();
                bgl.baglanti().Close();
                MessageBox.Show("Kulüp Başarıyla Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtAd.Text) ||
               string.IsNullOrEmpty(TxtId.Text))
            {
                MessageBox.Show("Kulüp adı veya Kulüp ID boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (TxtAd.Text.Length < 3 || TxtAd.Text.Length > 50)
            {
                MessageBox.Show("Kulüp adı en az 3, en fazla 50 karakter uzunluğunda olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlCommand cmd = new SqlCommand("delete from Tbl_Kulup where KulupID=@p1", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", TxtId.Text);
                cmd.ExecuteNonQuery();
                listele();
                bgl.baglanti().Close();
                MessageBox.Show("Kulüp Başarıyla Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtAd.Text)||
                string.IsNullOrEmpty(TxtId.Text))
            {
                MessageBox.Show("Kulüp adı veya Kulüp ID boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (TxtAd.Text.Length < 3 || TxtAd.Text.Length > 50)
            {
                MessageBox.Show("Kulüp adı en az 3, en fazla 50 karakter uzunluğunda olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlCommand cmd = new SqlCommand("update Tbl_Kulup set KulupAd=@p1 where KulupID=@p2", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
                cmd.Parameters.AddWithValue("@p2", TxtId.Text);
                cmd.ExecuteNonQuery();
                listele();
                bgl.baglanti().Close();
                MessageBox.Show("Kulüp Başarıyla Güncellendi    .", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
