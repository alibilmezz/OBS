using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace EOkul
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmOgrenciGiris frm = new FrmOgrenciGiris();
            frm.Show();
            this.Hide();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmOgretmenGiris frm1= new FrmOgretmenGiris();
            frm1.Show();
            this.Hide();

        }
    }
}
