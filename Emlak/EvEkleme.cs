using System;
using System.IO;
using System.Windows.Forms;

namespace Emlak
{
    public partial class EvEkleme : Form
    {
        public enum EvTur
        {
            Bilinmiyor,
            Daire,
            Bahçeli,
            Dubleks,
            Müstakil,
            Havuzlu,
            Apart
        }

        public EvTur evtur;
        public EvEkleme()
        {
            evtur = EvTur.Bilinmiyor;
            InitializeComponent();
        }

        private void EvEkleme_Load(object sender, EventArgs e)
        {
            groupBox_kiralik_ev_.Hide();
            groupBox_satilik_ev.Show();
            IlListYukleme();
            EvTurBelirleme();
        }
        private void EvTurBelirleme() //enumdaki ev turlerini comboboxa ekler
        {
            string[] evturleri = Enum.GetNames(typeof(EvTur));
            foreach (var item in evturleri)
            {
                comboBox_ev_turu.Items.Add(item);
            }
        }
        private void IlListYukleme() //illerin bulundugu dosyadan il listesini yükler
        {
            string ilDosyaYolu = "iller.txt";

            try
            {
                if (File.Exists(ilDosyaYolu)) //dosyanın varlıgını kontrol eder
                {
                    string[] iller = File.ReadAllLines(ilDosyaYolu); //dosyanın tum satırlarını dizi olarak okur ve dosyadaki her satırı diziye atar
                    comboBox_il.Items.AddRange(iller);
                }
                else
                {
                    MessageBox.Show("İl dosyası bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void comboBox_il_SelectedIndexChanged(object sender, EventArgs e) //secilen ile göre ilçe listesini yükler
        {
            string selectedCity = comboBox_il.SelectedItem.ToString();
            string ilceDosyaAdi = $"ilceler_{selectedCity}.txt";

            try
            {
                if (File.Exists(ilceDosyaAdi))
                {
                    string[] ilceler = File.ReadAllLines(ilceDosyaAdi);
                    comboBox_ilce.Items.Clear();
                    comboBox_ilce.Items.AddRange(ilceler);
                }
                else
                {
                    MessageBox.Show("İlçe listesi bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button_kaydet_Click(object sender, EventArgs e)
        {
            if (comboBox_il.Text == "")
            {
                MessageBox.Show("Lütfen İl Seçimi Yapınız.");
                return;
            }
            else if (comboBox_ilce.Text == "")
            {
                MessageBox.Show("Lütfen İlçe Seçimi Yapınız.");
                return;
            }
            else if (textBox_adres.Text == "")
            {
                MessageBox.Show("Lütfen Adres Bilgilerini Giriniz.");
                return;
            }
            else if (radioButton_aktif.Checked == false && radioButton_pasif.Checked == false)
            {
                MessageBox.Show("Lütfen Ev Durumunu Aktif veya Pasi Yapınız");
                return;
            }
            else if (textBox_kat_numarasi.Text == "")
            {
                MessageBox.Show("Kat Numarası Boş Olamaz.");
                return;
            }
            else if (textBox_toplam_alan.Text == "" && Convert.ToInt32(textBox_toplam_alan.Text) > 0)
            {
                MessageBox.Show("Ev Toplam Alan Boş veya 0 Olamaz.");
                return;
            }
            else if (textBox_oda_sayisi.Text == "" && Convert.ToInt32(textBox_oda_sayisi.Text) > 0)
            {
                MessageBox.Show("Ev Oda Sayisi Boş veya 0 Olamaz.");
                return;
            }
            else if (comboBox_ev_turu.Text == "")
            {
                MessageBox.Show("Ev Türü Boş Olamaz.");
                return;
            }
            else if (textBox_ev_yasi.Text == "")
            {
                MessageBox.Show("Ev Yaşı Belirlemek İçin Yapım Tarihini Giriniz.");
                return;
            }
            else if (Convert.ToInt32(textBox_ev_yasi.Text) <= 0)
            {
                MessageBox.Show("Ev Yaşı 0'dan Küçük Olamaz.");
                return;
            }
            else if (radioButton_kiralik_ev.Checked == false && radioButton_satilik_ev.Checked == false)
            {
                MessageBox.Show("Lütfen Ev Tercihini Yapınız");
                return;
            }
            else if (radioButton_kiralik_ev.Checked == true)
            {
                if (textBox_kiralikEv_kira.Text == "" || Convert.ToInt32(textBox_kiralikEv_kira.Text) < 0)
                {
                    MessageBox.Show("Lütfen Ev Kira Fiyatını Giriniz. Ev Kira Fiyatı 0'dan Küçük Olamaz");
                    return;
                }
                if (textBox_kiralikEv_depozito.Text == "" || Convert.ToInt32(textBox_kiralikEv_depozito.Text) < 0)
                {
                    MessageBox.Show("Lütfen Ev Depozito Fiyatını giriniz. Ev Depozito Fiyatı 0'dan Küçük Olamaz");
                    return;
                }
            }
            else if (radioButton_satilik_ev.Checked == true)
            {
                if (textBox_satilikEv_fiyat.Text == "" || Convert.ToInt32(textBox_satilikEv_fiyat.Text) < 0)
                {
                    MessageBox.Show("Lütfen Ev Fiyatını giriniz. Ev Fiyatı 0'dan Küçük Olamaz");
                    return;
                }
            }

            try
            {
                EvKayit(); // Ev kaydını dosyaya yazma işlemi
                MessageBox.Show("Ev Bilgileri Başarılı Bir Şekilde Kaydedildi. \n Ev Emlak Kayıt Numarası : " + kaydedilen_ev_emlak_no);
                this.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        public static int kaydedilen_ev_emlak_no;
        private void EvKayit()
        {
            string kayitDosyaYolu = "ev_kayitlari.txt";
            string satilikDosyaYolu = "satilik.txt";
            string kiralikDosyaYolu = "kiralik.txt";

            int kayitSayisi = KayitSayisiniGetir(kayitDosyaYolu);
            kaydedilen_ev_emlak_no = kayitSayisi + 1;

            // Ev durum bilgisini al
            string evDurumBilgisi = radioButton_aktif.Checked ? "aktif" : "pasif";

            // Ev bilgilerini oluştur
            string evBilgisi = $"{comboBox_il.Text}|{comboBox_ilce.Text}|{textBox_adres.Text}|{textBox_kat_numarasi.Text}|{textBox_toplam_alan.Text}|{textBox_oda_sayisi.Text}|{comboBox_ev_turu.Text}|{dateTimePicker_ev_yapim_tarihi.Text}|{evDurumBilgisi}";

            try
            {
                // Dosyayı aç ve ev bilgisini yaz
                using (StreamWriter sw = File.AppendText(kayitDosyaYolu))
                {
                    sw.WriteLine(evBilgisi);
                }

                // Satılık veya kiralık kontrolü yaparak ilgili dosyaya yaz
                if (radioButton_satilik_ev.Checked)
                {
                    using (StreamWriter sw = File.AppendText(satilikDosyaYolu))
                    {
                        sw.WriteLine(evBilgisi);
                    }
                }
                else if (radioButton_kiralik_ev.Checked)
                {
                    using (StreamWriter sw = File.AppendText(kiralikDosyaYolu))
                    {
                        sw.WriteLine(evBilgisi);
                    }
                }

                MessageBox.Show("Ev Bilgileri Başarılı Bir Şekilde Kaydedildi.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
        private int KayitSayisiniGetir(string dosyaYolu)
        {
            if (File.Exists(dosyaYolu))
            {
                string[] satirlar = File.ReadAllLines(dosyaYolu);
                return satirlar.Length;
            }
            return 0;
        }


        private void radioButton_satilik_ev_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_kiralik_ev_.Hide();
            groupBox_satilik_ev.Show();
        }

        private void radioButton_kiralik_ev_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_satilik_ev.Hide();
            groupBox_kiralik_ev_.Show();
        }

        private void dateTimePicker_ev_yapim_tarihi_ValueChanged(object sender, EventArgs e)
        {
            EvYasHesaplama();
        }

        private void EvYasHesaplama()
        {
            TimeSpan ts;
            ts = DateTime.Now - Convert.ToDateTime(dateTimePicker_ev_yapim_tarihi.Text);
            textBox_ev_yasi.Text = ts.Days.ToString();
        }

        private void radioButton_kiralik_ev_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox_satilik_ev.Hide();
            groupBox_kiralik_ev_.Show();
        }

        private void textBox_adres_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) //sadece harf ve kontrol karakterleri kabul edilir diğer tuşlar işlenmez.
              && !char.IsSeparator(e.KeyChar);
        }

        private void textBox_kat_numarasi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // istenen karakterler sadece rakamlar ve kontrol karakterleridir
        }
    }
}
