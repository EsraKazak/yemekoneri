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
        public static string connectionString = "Data Source=DESKTOP-IANIHDI\\SQLEXPRESS;Initial Catalog=tarif;Integrated Security=True";

        public static string ResimleriGetirSorgusu()
        {
            return "SELECT TarifID,resim, tarifAdi, hazirlamaSuresi ,Talimatlar FROM tarifler";
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

        public bool TarifSil(int tarifID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Tarif ile ilişkili malzemeleri sil
                string deleteMalzemeQuery = "DELETE FROM tarifMalzeme WHERE tarifID = @tarifID;";
                using (SqlCommand cmd = new SqlCommand(deleteMalzemeQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@tarifID", tarifID);
                    cmd.ExecuteNonQuery(); // Malzemeleri sil
                }

                // 2. Tarifi sil
                string deleteTarifQuery = "DELETE FROM tarifler WHERE TarifID = @tarifID;";
                using (SqlCommand cmd = new SqlCommand(deleteTarifQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@tarifID", tarifID);
                    int rowsAffected = cmd.ExecuteNonQuery(); // Tarifi sil

                    // Silme işlemi başarılı ise
                    return rowsAffected > 0;
                }
            }
        }

        public static void KategoriDoldur(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT kategoriAdi FROM kategori", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox.Items.Add(reader["kategoriAdi"].ToString());
                    }
                }
            }
        }

        public static void MalzemeDoldur(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT MalzemeAdi FROM malzeme ", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox.Items.Add(reader["MalzemeAdi"].ToString());
                    }
                }
            }
        }

        public static bool TarifEkle(string tarifAdi, string yapilisi, string hazirlamaSuresi, string resimYolu, string kategoriAdi, List<Tuple<int, string>> malzemeListesi)
        {
            int kategoriID = GetKategoriID(kategoriAdi);
            if (kategoriID == -1)
            {
                MessageBox.Show("Kategori bulunamadı.");
                return false;
            }

            string query = "INSERT INTO tarifler (TarifAdi, Talimatlar, HazirlamaSuresi, Resim, kategorii) " +
                           "VALUES (@tarifAdi, @Talimatlar, @hazirlamaSuresi, @resim, @kategorii);" +
                           "SELECT SCOPE_IDENTITY();"; // Eklenen tarifin ID'sini almak için

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tarifAdi", tarifAdi);
                    cmd.Parameters.AddWithValue("@Talimatlar", yapilisi);
                    cmd.Parameters.AddWithValue("@hazirlamaSuresi", hazirlamaSuresi);
                    cmd.Parameters.AddWithValue("@resim", resimYolu);
                    cmd.Parameters.AddWithValue("@kategorii", kategoriID);

                    conn.Open();
                    // Tarif eklendikten sonra tarifID'yi al
                    object result = cmd.ExecuteScalar();
                    int tarifID = Convert.ToInt32(result);

                    if (tarifID > 0)
                    {
                        // Tarif başarılı bir şekilde eklendiyse malzemeleri tarif-malzeme tablosuna ekle
                        foreach (var malzeme in malzemeListesi)
                        {
                            int malzemeID = malzeme.Item1;
                            string miktar = malzeme.Item2;

                            bool success = TarifMalzemeEkle(tarifID, malzemeID, miktar);
                            if (!success)
                            {
                                MessageBox.Show("Bir malzeme eklenemedi.");
                                return false;
                            }
                        }
                        return true;
                    }
                    return false;
                }
            }
        }


        public static bool TarifMalzemeEkle(int tarifID, int malzemeID, string miktar)
        {
            string query = "INSERT INTO tarifMalzeme (tarifID, malzemeID, MalzemeMiktar) VALUES (@tarifID, @malzemeID, @MalzemeMiktar)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tarifID", tarifID);
                    cmd.Parameters.AddWithValue("@malzemeID", malzemeID);
                    cmd.Parameters.AddWithValue("@MalzemeMiktar", miktar);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Başarıyla eklenirse true döner
                }
            }
        }

        public static int GetKategoriID(string kategoriAdi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT kategoriID FROM kategori WHERE kategoriAdi = @kategoriAdi";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kategoriAdi", kategoriAdi);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                return -1; // Kategori bulunamazsa -1 döner
            }
        }

        public static int GetMalzemeID(string malzemeAdi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT malzemeID FROM malzeme WHERE malzemeAdi = @malzemeAdi";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@malzemeAdi", malzemeAdi);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result); // Eğer malzeme bulunursa malzemeID döner
                }
                return -1; // Eğer malzeme bulunamazsa -1 döner
            }
        }





        public void MalzemeEkle(string malzemeAdi, string toplamMiktar, string malzemeBirim, string birimFiyat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO malzeme (MalzemeAdi, ToplamMiktar, MalzemeBirim, BirimFiyat) VALUES (@MalzemeAdi, @ToplamMiktar, @MalzemeBirim, @BirimFiyat)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
                    command.Parameters.AddWithValue("@ToplamMiktar", toplamMiktar);
                    command.Parameters.AddWithValue("@MalzemeBirim", malzemeBirim);
                    command.Parameters.AddWithValue("@BirimFiyat", birimFiyat);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static bool TarifVarMi(string tarifAdi)
        {
            string query = "SELECT COUNT(*) FROM tarifler WHERE TarifAdi = @tarifAdi";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tarifAdi", tarifAdi);

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        public static List<string> GetTarifMalzemeler(int tarifID)
        {
            List<string> malzemeler = new List<string>();
            string query = "SELECT m.MalzemeAdi, tm.MalzemeMiktar FROM tarifMalzeme tm " +
                           "JOIN malzeme m ON tm.malzemeID = m.MalzemeID " +
                           "WHERE tm.TarifID = @TarifID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tarifID", tarifID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string malzemeAdi = reader["MalzemeAdi"].ToString();
                        string miktar = reader["MalzemeMiktar"].ToString();
                        malzemeler.Add($"{malzemeAdi} - {miktar}");
                    }
                }
            }
            return malzemeler;
        }


        public static  decimal HesaplaMaliyet(int tarifID)
        {
            decimal toplamMaliyet = 0;

            string sql = @"
        SELECT 
            m.BirimFiyat, 
            m.MalzemeBirim, 
            tm.MalzemeMiktar 
        FROM 
            tarifMalzeme tm
        INNER JOIN 
            malzeme m ON tm.MalzemeID = m.MalzemeID
        WHERE 
            tm.TarifID = @TarifID";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TarifID", tarifID);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // BirimFiyat'ı varchar olarak al
                            string birimFiyatStr = reader["BirimFiyat"].ToString();
                            // MalzemeBirim
                            string malzemeBirim = reader["MalzemeBirim"].ToString();
                            // MalzemeMiktar'ı double olarak al
                            double malzemeMiktar = Convert.ToDouble(reader["MalzemeMiktar"]);

                            decimal birimFiyat;

                            // Birim fiyatı decimal'a dönüştürme
                            if (decimal.TryParse(birimFiyatStr, out birimFiyat))
                            {
                                decimal birimDönüşüm = 1; // Varsayılan dönüşüm oranı

                                // Birim dönüşüm oranlarını ayarla
                                if (malzemeBirim.Equals("gram", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Örneğin: 1 kg = 1000 gram
                                    birimDönüşüm = 1 / 1000m; // 1000 gram için dönüşüm oranı
                                }
                                else if (malzemeBirim.Equals("litre", StringComparison.OrdinalIgnoreCase))
                                {
                                    // 1 litre su = 1000 gram
                                    birimDönüşüm = 1; // 1 litre için dönüşüm oranı
                                }
                                // Diğer birim dönüşümleri burada eklenebilir

                                // Toplam maliyet hesapla
                                toplamMaliyet += birimFiyat * (decimal)malzemeMiktar * birimDönüşüm; // Dönüşüm oranı ile çarp
                            }
                            
                        }
                    }
                }
            }

            return toplamMaliyet;
        }


        public static bool YeterliMalzemeVarMi(int tarifID)
        {
            string sql = @"
        SELECT 
            m.ToplamMiktar, 
            tm.MalzemeMiktar 
        FROM 
            tarifMalzeme tm
        INNER JOIN 
            malzeme m ON tm.MalzemeID = m.MalzemeID
        WHERE 
            tm.TarifID = @TarifID";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TarifID", tarifID);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Toplam miktarı string olarak al ve decimal'a dönüştür
                            decimal toplamMiktar = Convert.ToDecimal(reader["ToplamMiktar"]);
                            // Malzeme miktarını double olarak al
                            double malzemeMiktar = Convert.ToDouble(reader["MalzemeMiktar"]);

                            // Yeterli malzeme kontrolü
                            if (toplamMiktar < (decimal)malzemeMiktar)
                            {
                                return false; // Yetersiz malzeme varsa false döndür
                            }
                        }
                    }
                }
            }

            return true; // Tüm malzemeler yeterliyse true döndür
        }

        public static string tarifquery(string kategori, string malzeme, string aramaMetni, int? sortOrder)
        {
            // Filtreleme sorgusunu oluştur
            string query = "SELECT TarifID, resim, tarifAdi, hazirlamaSuresi, Talimatlar FROM tarifler WHERE 1=1";

            // Kategori koşulunu ekleyelim
            if (!string.IsNullOrEmpty(kategori))
            {
                query += " AND kategorii IN (SELECT kategoriID FROM kategori WHERE kategoriAdi = @kategori)";
            }

            // Malzeme koşulunu ekleyelim
            if (!string.IsNullOrEmpty(malzeme))
            {
                query += " AND TarifID IN (SELECT tarifID FROM tarifMalzeme WHERE malzemeID = (SELECT malzemeID FROM malzeme WHERE MalzemeAdi = @malzeme))";
            }

            // Arama metni koşulunu ekleyelim
            if (!string.IsNullOrEmpty(aramaMetni) && aramaMetni != "Yemek Tarifi Arayın")
            {
                query += " AND tarifAdi LIKE @tarifAdi";
            }

            // Sıralama ekleyelim
            if (sortOrder.HasValue)
            {
                switch (sortOrder.Value)
                {
                    case 0: // Alfabetik (A-Z)
                        query += " ORDER BY tarifAdi ASC";
                        break;
                    case 1: // Hazırlama Süresi (Artan)
                        query += " ORDER BY hazirlamaSuresi ASC";
                        break;
                    case 2: // Hazırlama Süresi (Azalan)
                        query += " ORDER BY hazirlamaSuresi DESC";
                        break;
                        // Diğer sıralama seçeneklerini buraya ekleyebilirsiniz
                }
            }

            return query;
        }

       public static void Malzemeleripanelekoy(Panel panel, Panel tarifPanel)
{
    panel.Controls.Clear(); // Paneldeki mevcut kontrolü temizler
    panel.AutoScroll = true; // Panelde kaydırma çubuğu aktif olur

    int x = 10; // İlk checkbox'ın soldan uzaklığı
    int y = 10; // İlk checkbox'ın yukarıdan uzaklığı
    int checkboxWidth = 160; // Her bir checkbox'ın genişliği
    int checkboxHeight = 30; // Her bir checkbox'ın yüksekliği
    int columnCount = 5; // Her satırda kaç checkbox olacağını ayarlar

    int currentColumn = 0; // O anki sütun sayacı

    // Veritabanından malzemeleri çek
    string query = "SELECT MalzemeID, MalzemeAdi FROM malzeme";
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Yeni bir CheckBox oluştur
                    CheckBox cb = new CheckBox();
                    cb.Text = reader["MalzemeAdi"].ToString();
                    cb.Tag = reader["MalzemeID"]; // MalzemeID'yi Tag olarak kaydet
                    cb.AutoSize = true;
                    cb.Location = new Point(x, y);

                    // CheckedChanged olayı ile malzemelere göre tarif getirme işlemi
                    cb.CheckedChanged += (s, e) => 
                    {
                        MalzemelereGoreTarifGetir(panel, tarifPanel); // Tarifi listeleyen fonksiyon
                    };

                    // Panel'e ekle
                    panel.Controls.Add(cb);

                    // Sıradaki checkbox'ın konumunu ayarlama
                    currentColumn++;
                    if (currentColumn >= columnCount)
                    {
                        currentColumn = 0; // Yeni satıra geç
                        x = 10; // Sola dön
                        y += checkboxHeight; // Aşağıya geç
                    }
                    else
                    {
                        x += checkboxWidth; // Bir sonraki sütuna geç
                    }
                }
            }
        }
    }
}
        public static void MalzemelereGoreTarifGetir(Panel malzemePanel, Panel tarifPanel)
        {
            // Seçilen malzemelerin ID'lerini al
            List<int> secilenMalzemeIDler = new List<int>();
            foreach (Control control in malzemePanel.Controls)
            {
                if (control is CheckBox cb && cb.Checked)
                {
                    secilenMalzemeIDler.Add((int)cb.Tag); // MalzemeID'yi listeye ekle
                }
            }

            if (secilenMalzemeIDler.Count == 0)
            {
                // Hiç malzeme seçilmediyse, tarif panelini temizle
                tarifPanel.Controls.Clear();
                return;
            }

           
            // SQL sorgusunu dinamik olarak oluştur
            string malzemeFiltre = string.Join(",", secilenMalzemeIDler);
            string query = $@"
SELECT 
    t.TarifID, 
    t.TarifAdi, 
    (COUNT(DISTINCT tm.MalzemeID) * 100.0) / NULLIF((SELECT COUNT(*) 
                                                      FROM tarifMalzeme 
                                                      WHERE TarifID = t.TarifID), 0) AS EslestirmeYuzdesi
FROM 
    tarifler AS t
LEFT JOIN 
    tarifMalzeme AS tm ON t.TarifID = tm.TarifID AND tm.MalzemeID IN ({malzemeFiltre})
WHERE 
    t.TarifID IN (SELECT TarifID FROM tarifMalzeme WHERE MalzemeID IN ({malzemeFiltre}))
GROUP BY 
    t.TarifID, t.TarifAdi
ORDER BY 
    EslestirmeYuzdesi DESC;";
            // Veritabanına bağlan ve sorguyu çalıştır
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Tarif panelini temizle
                        tarifPanel.Controls.Clear();

                        int x = 10; // İlk resmin soldan uzaklığı
                        int y = 10; // İlk resmin yukarıdan uzaklığı
                        int pictureBoxWidth = 100; // Resmin genişliği
                        int pictureBoxHeight = 100; // Resmin yüksekliği
                        int labelHeight = 30; // Yazının yüksekliği
                        int margin = 20; // Resim ve etiketler arası boşluk
                        int maxColumns = (tarifPanel.Width / (pictureBoxWidth + margin)); // Bir satırda kaç resim olabileceğini hesapla

                        while (reader.Read())
                        {
                            int tarifID = (int)reader["TarifID"]; // Tarif ID'sini al
                            string resimYolu = GetResimYolu(tarifID); // Resim yolunu al
                            Image resim = Image.FromFile(resimYolu);
                            PictureBox pictureBox = new PictureBox
                            {

                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Image = resim,
                                Location = new Point(x, y),
                                Size = new Size(pictureBoxWidth, pictureBoxHeight)

                              
                            };

                            Label lblTarif = new Label
                            {
                                Text = $"{reader["TarifAdi"]}\n {Convert.ToDecimal(reader["EslestirmeYuzdesi"]).ToString("F2")}%",
                                AutoSize = true,
                                Location = new Point(x, y + pictureBoxHeight + 5) // Resmin altına yerleştir
                            };

                            // Tarif paneline ekle
                            tarifPanel.Controls.Add(pictureBox);
                            tarifPanel.Controls.Add(lblTarif);

                            x += pictureBoxWidth + margin+5; // Bir sonraki resmin konumu için sağa kaydır

                            // Eğer x konumu panelin genişliğini aşıyorsa, bir alt satıra geç
                            if (x + pictureBoxWidth > tarifPanel.Width)
                            {
                                x = 10; // Sola döner
                                y += pictureBoxHeight + labelHeight + margin + 10; // Bir alt satıra geç
                            }
                        }
                    }
                }
            }
        }

        private static string GetResimYolu(int tarifID)
        {
            string resimYolu = string.Empty;

            string query = "SELECT resim FROM tarifler WHERE TarifID = @TarifID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TarifID", tarifID);
                    connection.Open();
                    object result = command.ExecuteScalar(); // Buradan gelen sonuç 'object' tipindedir.

                    // Eğer sonuç null değilse, string'e dönüştür.
                    if (result != null)
                    {
                        resimYolu = result.ToString();
                    }
                    else
                    {
                        // Hata yönetimi: Eğer resim yoksa uygun bir değer döndür
                        resimYolu = "default_path_to_image.jpg"; // Varsayılan bir resim yolu verin.
                    }
                }
            }

            return resimYolu;
        }


        public static bool MalzemeSil(int tarifID, string malzemeAdi)
        {
            // SQL bağlantısı ve komutu
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // Önce MalzemeID'yi bul
                string getMalzemeIDQuery = "SELECT MalzemeID FROM malzeme WHERE MalzemeAdi = @malzemeAdi";
                SqlCommand getMalzemeIDCommand = new SqlCommand(getMalzemeIDQuery, connection);
                getMalzemeIDCommand.Parameters.AddWithValue("@malzemeAdi", malzemeAdi);

                connection.Open();
                object result = getMalzemeIDCommand.ExecuteScalar(); // İlk satır ve ilk sütunu al

                // MalzemeID'yi kontrol et
                if (result != null)
                {
                    int malzemeID = Convert.ToInt32(result);

                    // Şimdi tarifMalzeme tablosundan silme işlemini gerçekleştir
                    string deleteQuery = "DELETE FROM tarifMalzeme WHERE TarifID = @tarifID AND MalzemeID = @malzemeID";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@tarifID", tarifID);
                    deleteCommand.Parameters.AddWithValue("@malzemeID", malzemeID);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    return rowsAffected > 0; // Eğer bir veya daha fazla satır silindiyse true döner
                }
                else
                {
                    return false; // Malzeme bulunamazsa false döner
                }
            }
        }

        public static bool MalzemeEkle2(int tarifID, string malzemeAdi, string malzemeMiktar)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // MalzemeID'yi almak için sorgu
                string malzemeIDQuery = "SELECT MalzemeID FROM malzeme WHERE MalzemeAdi = @MalzemeAdi";

                int malzemeID;
                using (SqlCommand cmd = new SqlCommand(malzemeIDQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
                    connection.Open();
                    var result = cmd.ExecuteScalar(); // MalzemeID'yi al
                    malzemeID = result != null ? Convert.ToInt32(result) : -1; // Eğer bulamazsa -1 döndür
                }

                // Eğer MalzemeID bulunamazsa, işlem yapma
                if (malzemeID == -1)
                {
                    return false; // Malzeme adı bulunamadı
                }

                // tarifMalzeme tablosuna ekleme sorgusu
                string query = "INSERT INTO tarifMalzeme (TarifID, MalzemeID, MalzemeMiktar) " +
                               "VALUES (@TarifID, @MalzemeID, @MalzemeMiktar)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TarifID", tarifID);
                    command.Parameters.AddWithValue("@MalzemeID", malzemeID);
                    command.Parameters.AddWithValue("@MalzemeMiktar", malzemeMiktar);
                   

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Eğer bir veya daha fazla satır eklendiyse true döner
                }
            }
        }


    }
}
