using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace EOkul
{
    internal class Sqlbaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglantı = new SqlConnection(@"Data Source=MAMIMONSTER\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True;Encrypt=False");
            baglantı.Open();
            return baglantı;
        }
    }
}
