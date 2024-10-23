using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Formats.Tar;


namespace yemektarifleri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // bunu düzelt panel 2 için olan fonksiyona falan yerletr
            panel2.AutoScroll = true;
            ResimleriGetir();
            sorgu.MalzemeDoldur(comboBox2);
            sorgu.KategoriDoldur(comboBox1);
        }



        private void textbox1_enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Yemek Tarifi Arayýn")
            {
                textBox1.Text = " ";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox1_leave(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                textBox1.Text = "Yemek Tarifi Arayýn";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void comcobox_enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Kategoriler")
            {
                comboBox1.Text = " ";
                comboBox1.ForeColor = Color.Black;
            }
        }

        private void comcobox_leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == " ")
            {
                comboBox1.Text = "Kategoriler";
                comboBox1.ForeColor = Color.Silver;
            }
        }


        private void comcobox2_enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Malzemeler")
            {
                comboBox1.Text = " ";
                comboBox1.ForeColor = Color.Black;
            }
        }

        private void comcobox2_leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == " ")
            {
                comboBox1.Text = "Malzemeler";
                comboBox1.ForeColor = Color.Silver;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tarifekle trf = new tarifekle();
            trf.Show();
            this.Hide();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // Seçilen kategori ve malzeme deðerlerini alalým
            string kategori = null;
            string aramaMetni = textBox1.Text.Trim();

            if (comboBox1.SelectedItem != null)
            {
                kategori = comboBox1.SelectedItem.ToString() != "Kategoriler" ? comboBox1.SelectedItem.ToString() : null;
            }
            string malzeme = comboBox2.SelectedItem != null && comboBox2.SelectedItem.ToString() != "Malzemeler"
                                       ? comboBox2.SelectedItem.ToString()
                                       : null;

            // Sýralama seçeneðini alalým
            int selectedIndex = comboBox3.SelectedIndex;

            // Sorgu sýnýfýný kullanarak sorguyu oluþtur

            string query = sorgu.tarifquery(kategori, malzeme, aramaMetni, selectedIndex);

            // SQL baðlantýsý ve komutu
            using (SqlConnection connection = new SqlConnection(sorgu.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Parametreleri ekleyelim
                if (!string.IsNullOrEmpty(aramaMetni) && aramaMetni != "Yemek Tarifi Arayýn")
                {
                    command.Parameters.AddWithValue("@tarifAdi", "%" + aramaMetni + "%");
                }

                if (!string.IsNullOrEmpty(kategori))
                    command.Parameters.AddWithValue("@kategori", kategori);
                if (!string.IsNullOrEmpty(malzeme))
                    command.Parameters.AddWithValue("@malzeme", malzeme);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Paneldeki önceki sonuçlarý temizleyelim
                panel2.Controls.Clear();

                // Tarif ID ve maliyet listesini oluþtur
                List<KeyValuePair<int, decimal>> tarifMaliyetListesi = new List<KeyValuePair<int, decimal>>();

                // Tarifleri okuyalým ve maliyetleri hesaplayalým
                while (reader.Read())
                {
                    int tarifID = Convert.ToInt32(reader["TarifID"]);
                    decimal maliyet = sorgu.HesaplaMaliyet(tarifID);

                    tarifMaliyetListesi.Add(new KeyValuePair<int, decimal>(tarifID, maliyet));
                }

                reader.Close();

                // Maliyetlere göre sýralama yapalým
                if (selectedIndex == 3) // Maliyet Artan
                {
                    tarifMaliyetListesi = tarifMaliyetListesi.OrderBy(t => t.Value).ToList();
                }
                else if (selectedIndex == 4) // Maliyet Azalan
                {
                    tarifMaliyetListesi = tarifMaliyetListesi.OrderByDescending(t => t.Value).ToList();
                }

                // Panelde göstermek için tarifleri yeniden sorgulayalým
                int xPos = 30;
                int yPos = 30;
                int padding = 50;

                foreach (var item in tarifMaliyetListesi)
                {
                    int tarifID = item.Key;
                    decimal maliyet = item.Value;

                    // Tarif bilgilerini yeniden çekmek için sorgu
                    string tarifQuery = $"SELECT * FROM Tarifler WHERE TarifID = @tarifID";
                    SqlCommand tarifCommand = new SqlCommand(tarifQuery, connection);
                    tarifCommand.Parameters.AddWithValue("@tarifID", tarifID);

                    SqlDataReader tarifReader = tarifCommand.ExecuteReader();

                    if (tarifReader.Read())
                    {
                        string resimYolu = tarifReader["resim"].ToString();
                        string tarifAdi = tarifReader["tarifAdi"].ToString();
                        int hazirlamaSuresiDb = Convert.ToInt32(tarifReader["hazirlamaSuresi"]);
                        string talimat = tarifReader["Talimatlar"].ToString();

                        Image resim = Image.FromFile(resimYolu);
                        bool yeterli = sorgu.YeterliMalzemeVarMi(tarifID);

                        // PictureBox ve Label ekleyelim
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Image = resim;
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Size = new Size(150, 150);
                        pictureBox.Location = new Point(xPos, yPos);

                        Label label = new Label();
                        label.Text = $"{tarifAdi}\n{hazirlamaSuresiDb} Dk\nMaliyet: {maliyet:C}";
                        label.AutoSize = false;
                        label.Size = new Size(150, 60);
                        label.Font = new Font("Arial", 9, FontStyle.Bold);
                        label.TextAlign = ContentAlignment.TopCenter;
                        label.Location = new Point(xPos, yPos + pictureBox.Height + 10);
                        label.ForeColor = yeterli ? Color.Green : Color.Red;

                        pictureBox.Click += (s, ev) =>
                        {
                            // Detay formunu gösterelim
                            tarifDetay detayForm = new tarifDetay();
                            detayForm.Goster(resim, tarifAdi, hazirlamaSuresiDb.ToString(), talimat, tarifID);
                            detayForm.ShowDialog();
                            this.Hide();
                        };

                        panel2.Controls.Add(pictureBox);
                        panel2.Controls.Add(label);

                        xPos += pictureBox.Width + padding;
                        if (xPos + pictureBox.Width > panel2.Width)
                        {
                            xPos = 30;
                            yPos += pictureBox.Height + label.Height + padding;
                        }
                    }

                    tarifReader.Close();
                }

                connection.Close();
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void ResimleriGetir()
        {
            string query = sorgu.ResimleriGetirSorgusu(); // Sorguyu Sorgu sýnýfýndan çekiyoruz
            using (SqlConnection connection = new SqlConnection(sorgu.ConnectionString)) // connectionString'ý Sorgu sýnýfýndan alýyoruz
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Paneldeki önceki kontrolleri temizleyelim
                panel2.Controls.Clear();

                // Resimleri listeleyelim
                int xPos = 30; // Baþlangýç pozisyonu (X ekseni)
                int yPos = 30; // Baþlangýç pozisyonu (Y ekseni)
                int padding = 50; // Resimler ve etiketler arasý boþluk

                while (reader.Read())
                {
                    string resimYolu = reader["resim"].ToString();
                    string tarifAdi = reader["tarifAdi"].ToString(); // Tarif adýný al
                    string hazirlamaSuresi = reader["hazirlamaSuresi"].ToString(); // Hazýrlanma süresini al
                    string talimat = reader["Talimatlar"].ToString();
                    int tarifID = Convert.ToInt32(reader["TarifID"]); // tarifID'yi alýyoruz

                    // Maliyet hesapla
                    decimal maliyet = sorgu.HesaplaMaliyet(tarifID);
                    bool yeterli = sorgu.YeterliMalzemeVarMi(tarifID);

                    // Resmi dosya yolundan yükle
                    Image resim = Image.FromFile(resimYolu);

                    // PictureBox oluþtur ve panele ekle
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = resim;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Size = new Size(150, 150); // Resim boyutu
                    pictureBox.Location = new Point(xPos, yPos); // Resmin paneldeki pozisyonu

                    // Label oluþtur ve tarif adýný, hazýrlanma süresini ve maliyeti ekle
                    Label label = new Label();
                    label.Text = $"{tarifAdi}\n{hazirlamaSuresi} Dk\nMaliyet: {maliyet:C}"; // Alt alta yazma için \n kullanýldý
                    label.AutoSize = false;
                    label.Size = new Size(150, 80); // Label boyutu geniþletildi
                    label.Font = new Font("Arial", 9, FontStyle.Bold); // Yazý tipi boyutu küçültüldü
                    label.TextAlign = ContentAlignment.TopCenter;
                    label.Location = new Point(xPos, yPos + pictureBox.Height + 10); // PictureBox'ýn altýna yerleþtirildi


                    label.ForeColor = yeterli ? Color.Green : Color.Red;
                    // PictureBox'a týklama olayýný ekle
                    pictureBox.Click += (sender, e) =>
                    {
                        // Yeni tarifDetay formunu oluþtur
                        tarifDetay detayForm = new tarifDetay();

                        // Resmi ve bilgileri detay formuna aktar
                        detayForm.Goster(resim, tarifAdi, hazirlamaSuresi, talimat, tarifID); // Goster metodunu güncelleyelim

                        // Detay formunu aç
                        detayForm.ShowDialog(); // Detay formunu modal olarak göster
                        this.Hide();

                    };

                    // Panel'e PictureBox ve Label ekle
                    panel2.Controls.Add(pictureBox);
                    panel2.Controls.Add(label);

                    // Bir sonraki resim ve etiket için yatayda kaydýr
                    xPos += pictureBox.Width + padding;

                    // Panelde yatay sýnýrý geçerse, bir satýr aþaðýya geç
                    if (xPos + pictureBox.Width > panel2.Width)
                    {
                        xPos = 30; // Yatay pozisyonu sýfýrla
                        yPos += pictureBox.Height + label.Height + padding; // Bir satýr aþaðýya in
                    }
                }

                reader.Close();
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Malzemeyegöreara mlz = new Malzemeyegöreara();
            mlz.ShowDialog();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            stokform stk=new stokform();
            stk.ShowDialog();
            
        }
    }
}
