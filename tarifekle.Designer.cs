namespace yemektarifleri
{
    partial class tarifekle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label3 = new Label();
            button2 = new Button();
            panel2 = new Panel();
            richTextBox1 = new RichTextBox();
            label2 = new Label();
            listBox1 = new ListBox();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            comboBox2 = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            label4 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(798, 556);
            panel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.Location = new Point(56, 52);
            label3.Name = "label3";
            label3.Size = new Size(66, 85);
            label3.TabIndex = 8;
            label3.Text = "Resim Seçmek için Tıklayın";
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(301, 144);
            button2.Name = "button2";
            button2.Size = new Size(285, 29);
            button2.TabIndex = 6;
            button2.Text = "Tarifi Ekle";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(richTextBox1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(listBox1);
            panel2.Controls.Add(textBox5);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(comboBox2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(12, 188);
            panel2.Name = "panel2";
            panel2.Size = new Size(760, 365);
            panel2.TabIndex = 5;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(324, 37);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(429, 316);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // label2
            // 
            label2.Location = new Point(470, 9);
            label2.Name = "label2";
            label2.Size = new Size(156, 38);
            label2.TabIndex = 6;
            label2.Text = "Tarif Adımları";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(-2, 105);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(266, 244);
            listBox1.TabIndex = 5;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(139, 72);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(130, 27);
            textBox5.TabIndex = 4;
            textBox5.Text = "Birim";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(3, 72);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(130, 27);
            textBox4.TabIndex = 3;
            textBox4.Text = "Miktar";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(3, 38);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(130, 28);
            comboBox2.TabIndex = 2;
            comboBox2.Text = "Malzeme Adı";
            // 
            // button1
            // 
            button1.Location = new Point(139, 37);
            button1.Name = "button1";
            button1.Size = new Size(130, 29);
            button1.TabIndex = 1;
            button1.Text = "Malzeme Ekle";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(84, 9);
            label1.Name = "label1";
            label1.Size = new Size(124, 25);
            label1.TabIndex = 0;
            label1.Text = "Malzeme Ekleme";
            label1.Click += label1_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(534, 73);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(135, 27);
            textBox3.TabIndex = 4;
            textBox3.Text = "Maliyet";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(392, 74);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(136, 27);
            textBox2.TabIndex = 3;
            textBox2.Text = "Hazırlanma süresi";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(211, 73);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(175, 28);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "Kategori";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(211, 18);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(458, 36);
            textBox1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(12, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(175, 173);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(392, 117);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 9;
            label4.Text = "label4";
            // 
            // tarifekle
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 561);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "tarifekle";
            Text = "tarifekle";
            Load += tarifekle_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private TextBox textBox3;
        private TextBox textBox2;
        private ComboBox comboBox1;
        private Panel panel2;
        private Button button1;
        private Label label1;
        private ComboBox comboBox2;
        private ListBox listBox1;
        private TextBox textBox5;
        private TextBox textBox4;
        private RichTextBox richTextBox1;
        private Label label2;
        private Button button2;
        private Label label3;
        private OpenFileDialog openFileDialog1;
        private Label label4;
    }
}