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
using System.Data.SqlClient;
using System.Data.OleDb;


namespace EOkul
{
    public partial class FrmOgrencıIslerı : Form
    {
        public FrmOgrencıIslerı()
        {
            InitializeComponent();
        }
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        Sqlbaglanti bgl = new Sqlbaglanti();
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmOgrencıIslerı_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource= ds.OgrenciListe();
            SqlCommand cmd = new SqlCommand("select * from Tbl_Kulup",bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "KulupAd";
            comboBox1.ValueMember = "KulupID";
            comboBox1.DataSource = dt;
           

        }
        void Temizle()
        {
            TxtAd.Text=string.Empty;
            maskedTextBox1.Text=string.Empty;
            TxtSoyad.Text=string.Empty;
            comboBox1.Text=string.Empty;
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
                string.IsNullOrWhiteSpace(TxtAd.Text) ||
                string.IsNullOrWhiteSpace(TxtSoyad.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text) ||
                (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
            if (maskedTextBox1.Text.Length != 11 || !long.TryParse(maskedTextBox1.Text, out _))
            {
                MessageBox.Show("TC Kimlik numarası 11 haneli ve sadece rakamlardan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            {
                
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tbl_Ogrenci WHERE OgrTc = @Tc",bgl.baglanti());
                cmd.Parameters.AddWithValue("@Tc", maskedTextBox1.Text);
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Bu TC Kimlik numarası zaten kayıtlı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

         
            if (!TxtAd.Text.All(char.IsLetter) || !TxtSoyad.Text.All(char.IsLetter))
            {
                MessageBox.Show("Ad ve Soyad sadece harflerden oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string c = "";
                if (radioButton1.Checked == true)
                {
                    c = "Kadın";
                }
                else
                {
                    c = "Erkek";
                }
                ds.OgrenciEkle(TxtAd.Text,TxtSoyad.Text,byte.Parse(comboBox1.SelectedValue.ToString()),c,maskedTextBox1.Text);
                MessageBox.Show("Öğrenci Başarıyla Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = ds.OgrenciListe();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
       string.IsNullOrWhiteSpace(TxtAd.Text) ||
       string.IsNullOrWhiteSpace(TxtSoyad.Text) ||
       string.IsNullOrWhiteSpace(comboBox1.Text) ||
       (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maskedTextBox1.Text.Length != 11 || !long.TryParse(maskedTextBox1.Text, out _))
            {
                MessageBox.Show("TC Kimlik numarası 11 haneli ve sadece rakamlardan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TxtAd.Text.All(char.IsLetter) || !TxtSoyad.Text.All(char.IsLetter))
            {
                MessageBox.Show("Ad ve Soyad sadece harflerden oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                ds.Ogrsil(Convert.ToInt32(label7.Text));
                MessageBox.Show("Öğrenci Başarıyla Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = ds.OgrenciListe();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            label7.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex ].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() == "Erkek")
            {
                radioButton2.Checked=true;

            }
            else
            {
               
                radioButton1.Checked=true;
            }


            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
                string.IsNullOrWhiteSpace(TxtAd.Text) ||
                string.IsNullOrWhiteSpace(TxtSoyad.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text) ||
                (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (maskedTextBox1.Text.Length != 11 || !long.TryParse(maskedTextBox1.Text, out _))
            {
                MessageBox.Show("TC Kimlik numarası 11 haneli ve sadece rakamlardan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


           


            if (!TxtAd.Text.All(char.IsLetter) || !TxtSoyad.Text.All(char.IsLetter))
            {
                MessageBox.Show("Ad ve Soyad sadece harflerden oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                
                string c = "";
                if (radioButton1.Checked == true)
                {
                    c = "Kadın";
                }
                else
                {
                    c = "Erkek";
                }
                ds.Ogrgüncelle(TxtAd.Text, TxtSoyad.Text, byte.Parse(comboBox1.SelectedValue.ToString()), c,int.Parse(label7.Text));
                MessageBox.Show("Öğrenci Başarıyla Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = ds.OgrenciListe();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

      

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            string query = "SELECT OgrID,Ograd,OgrSoyad,OgrKulup,OgrCinsiyet,OgrTc FROM Tbl_Ogrenci WHERE OgrAd LIKE @searchText + '%'";

          

            SqlCommand cmd = new SqlCommand(query,bgl.baglanti());
            cmd.Parameters.AddWithValue("@searchText", searchText + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

           

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TxtSoyad_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
