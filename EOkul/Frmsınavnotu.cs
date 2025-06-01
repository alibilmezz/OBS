using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EOkul
{
    public partial class Frmsınavnotu : Form
    {
        public Frmsınavnotu()
        {
            InitializeComponent();
        }
        
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        DataSet1TableAdapters.Tbl_NotlarTableAdapter dc = new DataSet1TableAdapters.Tbl_NotlarTableAdapter();


        Sqlbaglanti bgl = new Sqlbaglanti();
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            string query = "SELECT OgrID,Ograd,OgrSoyad FROM Tbl_Ogrenci WHERE OgrAd LIKE @searchText + '%'";



            SqlCommand cmd = new SqlCommand(query, bgl.baglanti());
            cmd.Parameters.AddWithValue("@searchText", searchText + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void BransCekme()
        {
            SqlCommand cmd = new SqlCommand("select OgretmenBrans from Tbl_Ogretmen where OgretmenID=@p1",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",ID);
            SqlDataReader dr = cmd.ExecuteReader(); 
            while (dr.Read())
            {
                Bransıd.Text = "" + dr[0];
            }
        }

        private void Frmsınavnotu_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            button2.Enabled = false;
            Bransıd.Visible = false;
            dataGridView1.DataSource = ds.OgrBilgiNot();
            BransCekme();
           SqlCommand cmd =new SqlCommand("select DersID,DersAd From Tbl_Ders where DersID=@p1",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", Bransıd.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.ValueMember=""+dr[0];
                comboBox1.Text = "" + dr[1];
            }
            dataGridView2.DataSource = dc.NotListeFul(byte.Parse(comboBox1.ValueMember));





        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            dataGridView2.DataSource = dc.Notliste(int.Parse(TxtId.Text), byte.Parse(comboBox1.ValueMember));
        }

        private void TxtDers_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        void Temizle()
        {
            TxtId.Text= string.Empty;
            TxtVize.Text= string.Empty;
            Txtquiz.Text= string.Empty;
            TxtProje.Text= string.Empty;
            TxtFinal.Text= string.Empty;
            TxtOrt.Text= string.Empty;
            TxtDurum.Text= string.Empty;

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(TxtVize.Text) ||
               string.IsNullOrWhiteSpace(TxtFinal.Text) ||
               string.IsNullOrWhiteSpace(Txtquiz.Text) ||
               string.IsNullOrWhiteSpace(TxtProje.Text)||
               string.IsNullOrWhiteSpace(TxtId.Text))
            if (!TxtVize.Text.All(char.IsNumber) ||
                !TxtFinal.Text.All(char.IsNumber)||
                    !Txtquiz.Text.All(char.IsNumber)||
                !TxtOrt.Text.All(char.IsNumber))
                

            {
                MessageBox.Show("Notlar sadece sayılardan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
               
                dc.NotGüncelle3(byte.Parse(sinav1.ToString()), byte.Parse(sinav2.ToString()), byte.Parse(quiz.ToString()), byte.Parse(proje.ToString()), decimal.Parse(ortlama.ToString()), durum, notıd);
                MessageBox.Show("Öğrenci Başarıyla Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = ds.OgrenciListe();
                Temizle();
                dataGridView2.DataSource = dc.NotListeFul(byte.Parse(comboBox1.ValueMember));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public string ID { get; set; }
        int notıd;
        int sinav1, sinav2, quiz, proje;
        int ortlama;
        bool durum;
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtVize.Text) ||
               string.IsNullOrWhiteSpace(TxtFinal.Text) ||
               string.IsNullOrWhiteSpace(Txtquiz.Text) ||
            string.IsNullOrWhiteSpace(TxtProje.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sinav1 =Convert.ToInt32(TxtVize.Text);
            sinav2 = Convert.ToInt32(TxtFinal.Text);
            quiz = Convert.ToInt32(Txtquiz.Text);
            proje = Convert.ToInt32(TxtProje.Text);
            ortlama = ((sinav1*30/100) + (sinav2 *40/100) + (quiz *10/100)+(proje *20/100));
            TxtOrt.Text=ortlama.ToString();
            if (ortlama >= 50)
            {
                TxtDurum.Text = "Geçti";
                durum =true;
            }
            else
            {
                TxtDurum.Text = "Kaldı";
                durum=false;
            }
            button2.Enabled = true;
            
            
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            notıd=int.Parse( dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
            TxtVize.Text= dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtFinal.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            Txtquiz.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtProje.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
            TxtOrt.Text = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
            TxtDurum.Text = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();

        }
    }
}
