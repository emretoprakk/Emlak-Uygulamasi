using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emlak
{
    public partial class EvDuzenleme : Form
    {
        public string seciliEvBilgisi { get; set; }
        public string emlak_numarasi_form_aktarma { get; set; }


        public EvDuzenleme()
        {
            InitializeComponent();
        }

        public EvTur evtur;
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

        private void EvDuzenleme_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(seciliEvBilgisi))
            {
                // Seçili evin bilgilerini işle
                string[] evBilgileri = seciliEvBilgisi.Split('|');

                comboBox_il.SelectedItem = evBilgileri[0];
                comboBox_ilce.SelectedItem = evBilgileri[1];
                groupBox_kiralik_ev_.Hide();
                groupBox_satilik_ev.Show();
                evtur = EvTur.Bilinmiyor;
                IlListYukleme();
                EvTurBelirleme();
                label_emlak_numarasi.Text = emlak_numarasi_form_aktarma;
                EvBilgileriIilkGirisYukleme(Convert.ToInt32(emlak_numarasi_form_aktarma));
                EvYasHesaplama();
            }
        }
    
    private void EvTurBelirleme() //enum içerisindeki ev türlerini comboboxa ekler
        {
            string[] evturleri = Enum.GetNames(typeof(EvTur));
            foreach (var item in evturleri)
            {
                comboBox_ev_turu.Items.Add(item);
            }
        }

        private void IlListYukleme() //illerin bulundugu dosyadan illeri okur comboboxa ekler
        {
            string ilDosyaYolu = "iller.txt";

            try
            {
                if (File.Exists(ilDosyaYolu))
                {
                    string[] iller = File.ReadAllLines(ilDosyaYolu);
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

        private void EvBilgileriIilkGirisYukleme(int emlak_numarasi_form_)
        {
            string dosyaAdi = $"ev_kayitlari/{emlak_numarasi_form_}.txt";
            if (File.Exists(dosyaAdi))
            {
                string[] satirlar = File.ReadAllLines(dosyaAdi);
                if (satirlar.Length > 0)
                {
                    string[] bilgiler = satirlar[0].Split('|');
                    comboBox_il.SelectedItem = bilgiler[0];
                    comboBox_ilce.SelectedItem = bilgiler[1];
                    textBox_adres.Text = bilgiler[2];
                    textBox_kat_numarasi.Text = bilgiler[3];
                    textBox_toplam_alan.Text = bilgiler[4];
                    textBox_oda_sayisi.Text = bilgiler[5];
                    comboBox_ev_turu.SelectedItem = bilgiler[6];

                    if (DateTime.TryParse(bilgiler[7], out DateTime yapimTarihi))
                    {
                        dateTimePicker_ev_yapim_tarihi.Value = yapimTarihi;
                    }
                    else
                    {
                        MessageBox.Show("Yapım tarihi geçersiz format");
                    }

                    string evDurumu = bilgiler[8].Trim();
                    if (evDurumu == "aktif")
                    {
                        radioButton_aktif.Checked = true;
                    }
                    else if (evDurumu == "pasif")
                    {
                        radioButton_pasif.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz ev durumu");
                    }
                 
                }
                else
                {
                    MessageBox.Show("Dosya boş");
                }
            }
            else
            {
                MessageBox.Show("Dosya bulunamadı");
            }
        }


        private void button_ev_bilgileri_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox_il.Text))
            {
                MessageBox.Show("Lütfen İl Seçimi Yapınız.");
                return;
            }

            if (string.IsNullOrEmpty(comboBox_ilce.Text))
            {
                MessageBox.Show("Lütfen İlçe Seçimi Yapınız.");
                return;
            }

            if (string.IsNullOrEmpty(textBox_adres.Text))
            {
                MessageBox.Show("Lütfen Adres Bilgilerini Giriniz.");
                return;
            }

            if (!(radioButton_aktif.Checked || radioButton_pasif.Checked))
            {
                MessageBox.Show("Lütfen Ev Durumunu Aktif veya Pasif Yapınız");
                return;
            }

            if (string.IsNullOrEmpty(textBox_kat_numarasi.Text))
            {
                MessageBox.Show("Kat Numarası Boş Olamaz.");
                return;
            }

            if (string.IsNullOrEmpty(textBox_toplam_alan.Text) || Convert.ToInt32(textBox_toplam_alan.Text) <= 0)
            {
                MessageBox.Show("Ev Toplam Alan Boş veya 0 Olamaz.");
                return;
            }

            if (string.IsNullOrEmpty(textBox_oda_sayisi.Text) || Convert.ToInt32(textBox_oda_sayisi.Text) <= 0)
            {
                MessageBox.Show("Ev Oda Sayisi Boş veya 0 Olamaz.");
                return;
            }

            if (string.IsNullOrEmpty(comboBox_ev_turu.Text))
            {
                MessageBox.Show("Ev Türü Boş Olamaz.");
                return;
            }

            if (string.IsNullOrEmpty(textBox_ev_yasi.Text))
            {
                MessageBox.Show("Ev Yaşı Belirlemek İçin Yapım Tarihini Giriniz.");
                return;
            }

            if (Convert.ToInt32(textBox_ev_yasi.Text) <= 0)
            {
                MessageBox.Show("Ev Yaşı 0'dan Küçük Olamaz.");
                return;
            }

            if (!(radioButton_kiralik_ev.Checked || radioButton_satilik_ev.Checked))
            {
                MessageBox.Show("Lütfen Ev Tercihini Yapınız");
                return;
            }

            if (radioButton_kiralik_ev.Checked)
            {
                if (string.IsNullOrEmpty(textBox_kiralikEv_kira.Text) || Convert.ToInt32(textBox_kiralikEv_kira.Text) < 0)
                {
                    MessageBox.Show("Lütfen Ev Kira Fiyatını Giriniz. Ev Kira Fiyatı 0'dan Küçük Olamaz");
                    return;
                }

                if (string.IsNullOrEmpty(textBox_kiralikEv_depozito.Text) || Convert.ToInt32(textBox_kiralikEv_depozito.Text) < 0)
                {
                    MessageBox.Show("Lütfen Ev Depozito Fiyatını giriniz. Ev Depozito Fiyatı 0'dan Küçük Olamaz");
                    return;
                }
            }
            else if (radioButton_satilik_ev.Checked)
            {
                if (string.IsNullOrEmpty(textBox_satilikEv_fiyat.Text) || Convert.ToInt32(textBox_satilikEv_fiyat.Text) < 0)
                {
                    MessageBox.Show("Lütfen Ev Fiyatını giriniz. Ev Fiyatı 0'dan Küçük Olamaz");
                    return;
                }
            }

            try
            {
                if (int.TryParse(label_emlak_numarasi.Text, out int emlak_no_update))
                {
                    EvUpdate(emlak_no_update);
                    MessageBox.Show("Ev Bilgileri Başarılı Bir Şekilde Güncellendi.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Geçersiz emlak numarası");
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }

        private void EvUpdate(int emlak_no_update)
        {
            string evDurumBilgisi = radioButton_aktif.Checked ? "aktif" : "pasif";

            string kayitDosyaYolu = "ev_kayitlari.txt";

            try
            {
                string[] evKayitlari = File.ReadAllLines(kayitDosyaYolu);

                for (int i = 0; i < evKayitlari.Length; i++)
                {
                    string[] evBilgileri = evKayitlari[i].Split('|');
                    if (evBilgileri.Length >= 9 && evBilgileri[8] == emlak_no_update.ToString())
                    {
                        evKayitlari[i] = $"{comboBox_il.SelectedItem}|{comboBox_ilce.SelectedItem}|{textBox_adres.Text}|{textBox_kat_numarasi.Text}|{textBox_toplam_alan.Text}|{textBox_oda_sayisi.Text}|{comboBox_ev_turu.SelectedItem}|{dateTimePicker_ev_yapim_tarihi.Value.ToString("dd.MM.yyyy")}|{evDurumBilgisi}";
                        break;
                    }
                }

                File.WriteAllLines(kayitDosyaYolu, evKayitlari);
                MessageBox.Show("Ev Bilgileri Başarılı Bir Şekilde Güncellendi.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }


        private void comboBox_il_SelectedIndexChanged(object sender, EventArgs e) //seçilen ile göre ilçeleri yükler
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

        private void button_ev_resimleri_Click(object sender, EventArgs e)
        {

        }

        private void textBox_adres_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBox_kat_numarasi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

    }
}
