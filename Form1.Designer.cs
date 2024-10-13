namespace yemektarifleri
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "meze ", "çorba", "salata" });
            comboBox1.Location = new Point(228, 77);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(190, 28);
            comboBox1.TabIndex = 0;
            comboBox1.Text = "Kategoriler";
            comboBox1.Enter += comcobox_enter;
            comboBox1.Leave += comcobox_leave;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(64, 15);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(525, 27);
            textBox1.TabIndex = 1;
            textBox1.Text = "Yemek Tarifi Arayın";
            textBox1.Enter += textbox1_enter;
            textBox1.Leave += textbox1_leave;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(444, 77);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(190, 27);
            textBox2.TabIndex = 2;
            textBox2.Text = "Hazırlanma Süresi";
            textBox2.Enter += textbox2_enter;
            textBox2.Leave += textbox2_leave;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(649, 75);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(190, 27);
            textBox3.TabIndex = 3;
            textBox3.Text = "Maliyet";
            textBox3.Enter += textbox3_enter;
            textBox3.Leave += textbox3_leave;
            // 
            // button1
            // 
            button1.Location = new Point(629, 13);
            button1.Name = "button1";
            button1.Size = new Size(210, 29);
            button1.TabIndex = 4;
            button1.Text = "Tarif Ekle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(64, 75);
            button2.Name = "button2";
            button2.Size = new Size(137, 29);
            button2.TabIndex = 5;
            button2.Text = "Sonuçları filtrele";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox3);
            panel1.Location = new Point(-1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(866, 555);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.Location = new Point(13, 159);
            panel2.Name = "panel2";
            panel2.Size = new Size(841, 383);
            panel2.TabIndex = 6;
            panel2.Paint += panel2_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 555);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
        private Panel panel1;
        private Panel panel2;
    }
}
