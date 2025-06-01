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
    public partial class FrmDersler : Form
    {
        public FrmDersler()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        DataSet1TableAdapters.Tbl_DersTableAdapter ds = new DataSet1TableAdapters.Tbl_DersTableAdapter();
        private void FrmDersler_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.DersListesi();
        }
        

        private void button3_Click(object sender, EventArgs e)
        {

            ds.DersEkle(TxtAd.Text);
            MessageBox.Show("Ders Başarıyla Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.DersListesi();
            TxtAd.Text=string.Empty;
            TxtId.Text=string.Empty;

        }

        private void button2_Click(object sender, EventArgs e)
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
                ds.DersSil(byte.Parse(TxtId.Text));
                MessageBox.Show("Ders Başarıyla Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = ds.DersListesi();
                TxtAd.Text = string.Empty;
                TxtId.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void button4_Click(object sender, EventArgs e)
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
                ds.DersGüncelle(TxtAd.Text, byte.Parse(TxtId.Text));
                MessageBox.Show("Ders Başarıyla Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = ds.DersListesi();
                TxtAd.Text = string.Empty;
                TxtId.Text = string.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
