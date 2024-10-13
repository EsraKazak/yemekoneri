using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yemektarifleri
{
    public partial class tarifDetay : Form
    {
        public tarifDetay()
        {
            InitializeComponent();
        }

        private void tarifDetay_Load(object sender, EventArgs e)
        {

        }

        public void Goster(Image resim, string tarifAd, string hazirlanmaSuresi, string talimat)
        {
            pictureBox1.Image = resim; // pictureBox1, tarif detay formundaki PictureBox
            textBox1.Text = tarifAd; // textBox1, tarif adını gösteren TextBox
            textBox2.Text = hazirlanmaSuresi; // textBox2, hazırlanma süresini gösteren TextBox
            richTextBox1.Text = talimat;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            richTextBox1.Enabled = true;
            listBox1.Enabled = true;
            button3.Enabled = true;

            ///buradan devam edecen gardaş
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tarifAdi = textBox1.Text;
            string hazirlanmaSuresi = textBox2.Text;
            string yapilisi = richTextBox1.Text;

            if (!string.IsNullOrWhiteSpace(tarifAdi) && !string.IsNullOrWhiteSpace(hazirlanmaSuresi) && !string.IsNullOrWhiteSpace(yapilisi))
            {
                // Sorgu sınıfı üzerinden güncelleme işlemi çağrılıyor
                sorgu sorguNesnesi = new sorgu();
                bool basariliMi = sorguNesnesi.TarifGuncelle(tarifAdi, hazirlanmaSuresi, yapilisi);

                if (basariliMi)
                {
                    MessageBox.Show("Tarif başarıyla güncellendi!");
                }
                else
                {
                    MessageBox.Show("Güncelleme başarısız oldu!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
            }
        }
    }
}
