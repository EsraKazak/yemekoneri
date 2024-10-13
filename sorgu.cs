using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yemektarifleri
{
    internal class sorgu
    {
        private static string connectionString = "Data Source=DESKTOP-IANIHDI\\SQLEXPRESS;Initial Catalog=tarif;Integrated Security=True";

        public static string ResimleriGetirSorgusu()
        {
            return "SELECT resim, tarifAdi, hazirlamaSuresi ,Talimatlar FROM tarifler";
        }

        // Bağlantı dizesini döndüren bir özellik ekliyoruz
        public static string ConnectionString
        {
            get { return connectionString; }
        }
        public bool TarifGuncelle(string tarifAdi, string hazirlanmaSuresi, string yapilisi)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // 1. Tarif ID'sini tarif adına göre bulma sorgusu
                    string selectSorgu = $"SELECT TarifID FROM tarifler WHERE TarifAdi = '{tarifAdi}'";
                    SqlCommand selectKomut = new SqlCommand(selectSorgu, con);
                    object result = selectKomut.ExecuteScalar();

                    if (result != null)
                    {
                        int tarifID = Convert.ToInt32(result);

                        // 2. Tarif güncelleme sorgusu
                        string updateSorgu = $"UPDATE tarifler SET HazirlamaSuresi = '{hazirlanmaSuresi}', Talimatlar = '{yapilisi}' WHERE TarifID = {tarifID}";
                        SqlCommand updateKomut = new SqlCommand(updateSorgu, con);

                        int rowsAffected = updateKomut.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    else
                    {
                        MessageBox.Show("Tarif bulunamadı.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                    return false;
                }
            }
        }

    }
}
