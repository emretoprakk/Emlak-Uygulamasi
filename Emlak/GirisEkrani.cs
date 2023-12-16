using System;
using System.IO;
using System.Windows.Forms;

namespace Emlak
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;

            // "users.txt" dosyasını kontrol ederek doğrulama yap
            if (KullaniciDogrula(kullaniciAdi, sifre))
            {
                AnaMenu anaMenu = new AnaMenu(); 
                this.Hide(); 
                anaMenu.ShowDialog(); // Ana formu göster
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre!"); 
            }
        }

        private bool KullaniciDogrula(string kullaniciAdi, string sifre) //users.txt dosyasındaki kayıtlarla karsılastırırız
        {
            string dosyaYolu = "users.txt";

            if (File.Exists(dosyaYolu)) //dosya varsa
            {
                string[] kullaniciBilgileri = File.ReadAllLines(dosyaYolu); //dosya oku

                foreach (string satir in kullaniciBilgileri)
                {
                    string[] bilgiler = satir.Split(',');
                    if (bilgiler.Length >= 2 && bilgiler[0] == kullaniciAdi && bilgiler[1] == sifre)
                    {
                        return true; // Kullanıcı adı ve şifre doğru
                    }
                }
            }
            return false; // Kullanıcı adı veya şifre yanlış
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Kayit fr = new Kayit();
            fr.Show();
        }

        
    }
}
