using System;
using System.IO;
using System.Windows.Forms;

namespace Emlak
{
    public partial class Kayit : Form
    {
        public Kayit()
        {
            InitializeComponent();
        }

        private void button_kayit_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox_kullanici_adi.Text;
            string sifre = textBox_sifre.Text;
            bool yetkiliKullanici = checkBox_yetkilendirme.Checked;

            if (!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(sifre))
            {
                if (KullaniciVarMi(kullaniciAdi))
                {
                    MessageBox.Show("Bu kullanıcı zaten var!");
                }
                else
                {
                    KullaniciEkle(kullaniciAdi, sifre, yetkiliKullanici);
                    MessageBox.Show("Kullanıcı başarıyla kaydedildi!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz!");
            }
        }

        private bool KullaniciVarMi(string kullaniciAdi) //kullanici var mi kontrolu
        {
            string dosyaYolu = "users.txt";

            if (File.Exists(dosyaYolu)) //dosya varsa
            {
                string[] kullaniciBilgileri = File.ReadAllLines(dosyaYolu);  //dosyanın tum satırlarını okur

                foreach (string satir in kullaniciBilgileri)
                {
                    string[] bilgiler = satir.Split(',');
                    if (bilgiler.Length >= 2 && bilgiler[0].Trim() == kullaniciAdi)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void KullaniciEkle(string kullaniciAdi, string sifre, bool yetkili) 
        {
            string dosyaYolu = "users.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(dosyaYolu, true)) //kullanıcı bilgileri dosyaya yazar
                {
                    writer.WriteLine($"{kullaniciAdi},{sifre},{(yetkili ? "Yetkili" : "Yetkisiz")}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        
    }
}
