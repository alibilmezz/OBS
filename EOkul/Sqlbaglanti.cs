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
            SqlConnection baglantı = new SqlConnection(@"Data Source=");
            baglantı.Open();
            return baglantı;
        }
    }
}
