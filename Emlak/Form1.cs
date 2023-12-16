using System;
using System.IO;
using System.Windows.Forms;

namespace Emlak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;

            // "users.txt" dosyasını kontrol ederek doğrulama yapalım
            if (KullaniciDogrula(kullaniciAdi, sifre))
            {
                AnaMenu anaMenu = new AnaMenu(); // AnaForm'u oluştur
                this.Hide(); // Login formunu gizle
                anaMenu.ShowDialog(); // Ana formu göster
                this.Close(); // Login formunu kapat
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre!"); // Hatalı giriş
            }
        }

        private bool KullaniciDogrula(string kullaniciAdi, string sifre)
        {
            string dosyaYolu = "users.txt";

            if (File.Exists(dosyaYolu))
            {
                string[] kullaniciBilgileri = File.ReadAllLines(dosyaYolu);

                foreach (string satir in kullaniciBilgileri)
                {
                    string[] bilgiler = satir.Split(',');
                    if (bilgiler.Length == 2 && bilgiler[0].Trim() == kullaniciAdi && bilgiler[1].Trim() == sifre)
                    {
                        return true; // Kullanıcı adı ve şifre doğru
                    }
                }
            }
            return false; // Kullanıcı adı veya şifre yanlış
        }
    }
}
