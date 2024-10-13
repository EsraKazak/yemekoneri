namespace yemektarifleri
{
    partial class tarifDetay
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
            pictureBox1 = new PictureBox();
            labelAd = new Label();
            labelSüre = new Label();
            panel1 = new Panel();
            listBox1 = new ListBox();
            richTextBox1 = new RichTextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(62, 62);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(206, 304);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // labelAd
            // 
            labelAd.AutoSize = true;
            labelAd.Location = new Point(296, 62);
            labelAd.Name = "labelAd";
            labelAd.Size = new Size(74, 20);
            labelAd.TabIndex = 1;
            labelAd.Text = "Tarif Adı -";
            // 
            // labelSüre
            // 
            labelSüre.AutoSize = true;
            labelSüre.Location = new Point(651, 62);
            labelSüre.Name = "labelSüre";
            labelSüre.Size = new Size(141, 20);
            labelSüre.TabIndex = 2;
            labelSüre.Text = "Hazırlanma Süredi -";
            // 
            // panel1
            // 
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(listBox1);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(labelAd);
            panel1.Controls.Add(labelSüre);
            panel1.Location = new Point(4, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(901, 719);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;
            // 
            // listBox1
            // 
            listBox1.Enabled = false;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(376, 142);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(321, 224);
            listBox1.TabIndex = 15;
            // 
            // richTextBox1
            // 
            richTextBox1.Enabled = false;
            richTextBox1.Location = new Point(62, 428);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(635, 284);
            richTextBox1.TabIndex = 14;
            richTextBox1.Text = "";
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(798, 62);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(53, 27);
            textBox2.TabIndex = 13;
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(376, 62);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(219, 27);
            textBox1.TabIndex = 12;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(717, 536);
            button3.Name = "button3";
            button3.Size = new Size(124, 29);
            button3.TabIndex = 11;
            button3.Text = "Tarifi Kaydet";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(717, 485);
            button2.Name = "button2";
            button2.Size = new Size(124, 29);
            button2.TabIndex = 8;
            button2.Text = "Tarifi Sil";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(717, 428);
            button1.Name = "button1";
            button1.Size = new Size(124, 29);
            button1.TabIndex = 7;
            button1.Text = "Tarifi Güncelle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(62, 391);
            label1.Name = "label1";
            label1.Size = new Size(125, 28);
            label1.TabIndex = 5;
            label1.Text = "Nasıl Yapılır?";
            // 
            // tarifDetay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(917, 732);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "tarifDetay";
            Text = "tarifDetay";
            Load += tarifDetay_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label labelAd;
        private Label labelSüre;
        private Panel panel1;
        private Label label1;
        private Button button2;
        private Button button1;
        private Button button3;
        private ListBox listBox1;
        private RichTextBox richTextBox1;
        private TextBox textBox2;
        private TextBox textBox1;
    }
}