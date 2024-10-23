using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yemektarifleri
{
    public partial class stokform : Form
    {
        public stokform()
        {
            InitializeComponent();
            MalzemeTablosunuGoruntule();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static string connectionString = "Data Source=DESKTOP-IANIHDI\\SQLEXPRESS;Initial Catalog=tarif;Integrated Security=True";



        // Bağlantı dizesini döndüren bir özellik ekliyoruz
        public static string ConnectionString
        {
            get { return connectionString; }
        }

        private void MalzemeTablosunuGoruntule()
        {
            // Veritabanından malzeme bilgilerini çekme
            string query = "SELECT  MalzemeAdi, ToplamMiktar, MalzemeBirim, BirimFiyat FROM malzeme";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable malzemeTable = new DataTable();
                adapter.Fill(malzemeTable);

                // DataGridView'ı doldurma
                dataGridView1.DataSource = malzemeTable;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Seçilen hücreyi kontrol et
            if (dataGridView1.SelectedCells.Count > 0)
            {
                // Seçilen hücreyi al
                DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];

                // Malzeme adını al
                string malzemeAdi = dataGridView1.Rows[selectedCell.RowIndex].Cells["MalzemeAdi"].Value.ToString();

                // Yeni değerleri al
                string yeniMiktar = textBox1.Text; // Toplam miktar için
                string yeniFiyat = textBox2.Text; // Birim fiyat için
                string ad = textBox3.Text;

                // Güncelleme sorgusu oluştur
                string query = "UPDATE malzeme SET ";

                // Güncellenecek alanları kontrol et
                if (!string.IsNullOrEmpty(yeniMiktar))
                {
                    query += "ToplamMiktar = @yeniMiktar ";
                }

                if (!string.IsNullOrEmpty(yeniFiyat))
                {
                    if (!string.IsNullOrEmpty(yeniMiktar))
                    {
                        query += ", "; // Eğer hem miktar hem de fiyat güncelleniyorsa araya virgül ekle
                    }
                    query += "BirimFiyat = @yeniFiyat ";
                }

                query += "WHERE MalzemeAdi = @malzemeAdi";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametreleri ekle
                        if (!string.IsNullOrEmpty(yeniMiktar))
                        {
                            command.Parameters.AddWithValue("@yeniMiktar", yeniMiktar);
                        }

                        if (!string.IsNullOrEmpty(yeniFiyat))
                        {
                            command.Parameters.AddWithValue("@yeniFiyat", yeniFiyat);
                        }

                        command.Parameters.AddWithValue("@malzemeAdi", ad);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Güncelleme başarılı!");
                                MalzemeTablosunuGoruntule();
                            }
                            else
                            {
                                MessageBox.Show("Güncelleme başarısız, malzeme bulunamadı.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir hücre seçin.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void stokform_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }


        private void stokform_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Ana formu aç
            Form1 anaForm = new Form1();
          anaForm.Show();
        } 
    }
}
