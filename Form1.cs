using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;


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

        private void textbox2_enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Hazýrlanma Süresi")
            {
                textBox1.Text = " ";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox2_leave(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                textBox1.Text = "Hazýrlanma Süresi";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textbox3_enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Maliyet")
            {
                textBox1.Text = " ";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox3_leave(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                textBox1.Text = "Maliyet";
                textBox1.ForeColor = Color.Silver;
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

                    // Resmi dosya yolundan yükle
                    Image resim = Image.FromFile(resimYolu);

                    // PictureBox oluþtur ve panele ekle
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = resim;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Size = new Size(150, 150); // Resim boyutu
                    pictureBox.Location = new Point(xPos, yPos); // Resmin paneldeki pozisyonu

                    // Label oluþtur ve tarif adýný ve hazýrlanma süresini ekle
                    Label label = new Label();
                    label.Text = $"{tarifAdi}\n{hazirlamaSuresi} Dk"; // Alt alta yazma için \n kullanýldý
                    label.AutoSize = false;
                    label.Size = new Size(150, 60); // Label boyutu geniþletildi
                    label.Font = new Font("Arial", 9, FontStyle.Bold); // Yazý tipi boyutu küçültüldü
                    label.TextAlign = ContentAlignment.TopCenter;
                    label.Location = new Point(xPos, yPos + pictureBox.Height + 10); // PictureBox'ýn altýna yerleþtirildi

                    // PictureBox'a týklama olayýný ekle
                    pictureBox.Click += (sender, e) =>
                    {
                        // Yeni tarifDetay formunu oluþtur
                        tarifDetay detayForm = new tarifDetay();

                        // Resmi ve bilgileri detay formuna aktar
                        detayForm.Goster(resim, tarifAdi, hazirlamaSuresi, talimat); // Goster metodunu güncelleyelim

                        // Detay formunu aç
                        detayForm.ShowDialog(); // Detay formunu modal olarak göster
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
    }
}
