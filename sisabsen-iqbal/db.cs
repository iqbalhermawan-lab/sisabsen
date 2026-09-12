using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace sisabsen_iqbal
{
    class db
    {
        public static MySqlConnection koneksi = new MySqlConnection("server = 127.0.0.1; username = root; password = ; database = absensi_siswa");
        public static DataSet ds = new DataSet();
        public static MySqlDataAdapter da;
        public static MySqlCommand perintah;

        public static void crud(string query)
        {
            Console.WriteLine(query);
            ds.Tables.Clear();
            perintah = new MySqlCommand(query, koneksi);
            da = new MySqlDataAdapter(perintah);
            da.Fill(ds);
        }
    }
}
