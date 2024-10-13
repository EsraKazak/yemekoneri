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
            // bunu d�zelt panel 2 i�in olan fonksiyona falan yerletr
            panel2.AutoScroll = true;
            ResimleriGetir();
        }



        private void textbox1_enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Yemek Tarifi Aray�n")
            {
                textBox1.Text = " ";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox1_leave(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                textBox1.Text = "Yemek Tarifi Aray�n";
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
            if (textBox1.Text == "Haz�rlanma S�resi")
            {
                textBox1.Text = " ";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox2_leave(object sender, EventArgs e)
        {
            if (textBox1.Text == " ")
            {
                textBox1.Text = "Haz�rlanma S�resi";
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
            string query = sorgu.ResimleriGetirSorgusu(); // Sorguyu Sorgu s�n�f�ndan �ekiyoruz
            using (SqlConnection connection = new SqlConnection(sorgu.ConnectionString)) // connectionString'� Sorgu s�n�f�ndan al�yoruz
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Paneldeki �nceki kontrolleri temizleyelim
                panel2.Controls.Clear();

                // Resimleri listeleyelim
                int xPos = 30; // Ba�lang�� pozisyonu (X ekseni)
                int yPos = 30; // Ba�lang�� pozisyonu (Y ekseni)
                int padding = 50; // Resimler ve etiketler aras� bo�luk

                while (reader.Read())
                {
                    string resimYolu = reader["resim"].ToString();
                    string tarifAdi = reader["tarifAdi"].ToString(); // Tarif ad�n� al
                    string hazirlamaSuresi = reader["hazirlamaSuresi"].ToString(); // Haz�rlanma s�resini al
                    string talimat = reader["Talimatlar"].ToString();

                    // Resmi dosya yolundan y�kle
                    Image resim = Image.FromFile(resimYolu);

                    // PictureBox olu�tur ve panele ekle
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = resim;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Size = new Size(150, 150); // Resim boyutu
                    pictureBox.Location = new Point(xPos, yPos); // Resmin paneldeki pozisyonu

                    // Label olu�tur ve tarif ad�n� ve haz�rlanma s�resini ekle
                    Label label = new Label();
                    label.Text = $"{tarifAdi}\n{hazirlamaSuresi} Dk"; // Alt alta yazma i�in \n kullan�ld�
                    label.AutoSize = false;
                    label.Size = new Size(150, 60); // Label boyutu geni�letildi
                    label.Font = new Font("Arial", 9, FontStyle.Bold); // Yaz� tipi boyutu k���lt�ld�
                    label.TextAlign = ContentAlignment.TopCenter;
                    label.Location = new Point(xPos, yPos + pictureBox.Height + 10); // PictureBox'�n alt�na yerle�tirildi

                    // PictureBox'a t�klama olay�n� ekle
                    pictureBox.Click += (sender, e) =>
                    {
                        // Yeni tarifDetay formunu olu�tur
                        tarifDetay detayForm = new tarifDetay();

                        // Resmi ve bilgileri detay formuna aktar
                        detayForm.Goster(resim, tarifAdi, hazirlamaSuresi, talimat); // Goster metodunu g�ncelleyelim

                        // Detay formunu a�
                        detayForm.ShowDialog(); // Detay formunu modal olarak g�ster
                    };

                    // Panel'e PictureBox ve Label ekle
                    panel2.Controls.Add(pictureBox);
                    panel2.Controls.Add(label);

                    // Bir sonraki resim ve etiket i�in yatayda kayd�r
                    xPos += pictureBox.Width + padding;

                    // Panelde yatay s�n�r� ge�erse, bir sat�r a�a��ya ge�
                    if (xPos + pictureBox.Width > panel2.Width)
                    {
                        xPos = 30; // Yatay pozisyonu s�f�rla
                        yPos += pictureBox.Height + label.Height + padding; // Bir sat�r a�a��ya in
                    }
                }

                reader.Close();
            }


        }
    }
}
