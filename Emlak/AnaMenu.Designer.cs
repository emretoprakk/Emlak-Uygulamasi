
namespace Emlak
{
    partial class AnaMenu
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
            this.button_Ev_Sorgulama = new System.Windows.Forms.Button();
            this.button_Ev_Ekleme = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Ev_Sorgulama
            // 
            this.button_Ev_Sorgulama.Location = new System.Drawing.Point(359, 12);
            this.button_Ev_Sorgulama.Name = "button_Ev_Sorgulama";
            this.button_Ev_Sorgulama.Size = new System.Drawing.Size(333, 231);
            this.button_Ev_Sorgulama.TabIndex = 1;
            this.button_Ev_Sorgulama.Text = "Ev Sorgulama / Ev Listeleme";
            this.button_Ev_Sorgulama.UseVisualStyleBackColor = true;
            this.button_Ev_Sorgulama.Click += new System.EventHandler(this.button_Ev_Sorgulama_Click);
            // 
            // button_Ev_Ekleme
            // 
            this.button_Ev_Ekleme.Location = new System.Drawing.Point(12, 12);
            this.button_Ev_Ekleme.Name = "button_Ev_Ekleme";
            this.button_Ev_Ekleme.Size = new System.Drawing.Size(320, 231);
            this.button_Ev_Ekleme.TabIndex = 2;
            this.button_Ev_Ekleme.Text = "Ev Ekleme";
            this.button_Ev_Ekleme.UseVisualStyleBackColor = true;
            this.button_Ev_Ekleme.Click += new System.EventHandler(this.button_Ev_Ekleme_Click);
            // 
            // AnaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 276);
            this.Controls.Add(this.button_Ev_Sorgulama);
            this.Controls.Add(this.button_Ev_Ekleme);
            this.Name = "AnaMenu";
            this.Text = "AnaMenu";
            this.Load += new System.EventHandler(this.AnaMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Ev_Sorgulama;
        private System.Windows.Forms.Button button_Ev_Ekleme;
    }
}