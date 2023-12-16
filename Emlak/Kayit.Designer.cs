
namespace Emlak
{
    partial class Kayit
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
            this.checkBox_yetkilendirme = new System.Windows.Forms.CheckBox();
            this.button_kayit = new System.Windows.Forms.Button();
            this.textBox_sifre = new System.Windows.Forms.TextBox();
            this.textBox_kullanici_adi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox_yetkilendirme
            // 
            this.checkBox_yetkilendirme.AutoSize = true;
            this.checkBox_yetkilendirme.Location = new System.Drawing.Point(249, 100);
            this.checkBox_yetkilendirme.Name = "checkBox_yetkilendirme";
            this.checkBox_yetkilendirme.Size = new System.Drawing.Size(75, 17);
            this.checkBox_yetkilendirme.TabIndex = 15;
            this.checkBox_yetkilendirme.Text = "Yetkilendir";
            this.checkBox_yetkilendirme.UseVisualStyleBackColor = true;
            // 
            // button_kayit
            // 
            this.button_kayit.Location = new System.Drawing.Point(22, 135);
            this.button_kayit.Name = "button_kayit";
            this.button_kayit.Size = new System.Drawing.Size(375, 67);
            this.button_kayit.TabIndex = 14;
            this.button_kayit.Text = "Kayıt";
            this.button_kayit.UseVisualStyleBackColor = true;
            this.button_kayit.Click += new System.EventHandler(this.button_kayit_Click);
            // 
            // textBox_sifre
            // 
            this.textBox_sifre.Location = new System.Drawing.Point(151, 58);
            this.textBox_sifre.Name = "textBox_sifre";
            this.textBox_sifre.PasswordChar = '*';
            this.textBox_sifre.Size = new System.Drawing.Size(208, 20);
            this.textBox_sifre.TabIndex = 12;
            this.textBox_sifre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_kullanici_adi
            // 
            this.textBox_kullanici_adi.Location = new System.Drawing.Point(151, 26);
            this.textBox_kullanici_adi.Name = "textBox_kullanici_adi";
            this.textBox_kullanici_adi.Size = new System.Drawing.Size(208, 20);
            this.textBox_kullanici_adi.TabIndex = 13;
            this.textBox_kullanici_adi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(97, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "Sifre";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(97, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "Kullanıcı Yetkilendirme";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(51, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Kullanici Ad";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Kayit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 231);
            this.Controls.Add(this.checkBox_yetkilendirme);
            this.Controls.Add(this.button_kayit);
            this.Controls.Add(this.textBox_sifre);
            this.Controls.Add(this.textBox_kullanici_adi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Kayit";
            this.Text = "Kayit";
//            this.Load += new System.EventHandler(this.Kayit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_yetkilendirme;
        private System.Windows.Forms.Button button_kayit;
        private System.Windows.Forms.TextBox textBox_sifre;
        private System.Windows.Forms.TextBox textBox_kullanici_adi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}