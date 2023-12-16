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
    public partial class EvListelemeSorgulama : Form
    {
        public EvListelemeSorgulama()
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
       
        private void EvListelemeSorgulama_Load(object sender, EventArgs e)
        {
            evtur = EvTur.Bilinmiyor;
            IlListYukleme();
            EvTurBelirleme();

        }
        private void EvTurBelirleme() //ev türlerini enumdan alarak comboboxa ekler
        {
            string[] evturleri = Enum.GetNames(typeof(EvTur));
            foreach (var item in evturleri)
            {
                comboBox_ev_turu.Items.Add(item);
            }
        }

        private void IlListYukleme() //il listesini bir dosyadan alarak comboboxa ekler
        {
            string ilDosyaYolu = "iller.txt";

            try
            {
                if (File.Exists(ilDosyaYolu)) //dosyanın var olup olmadıgını kontrol eder
                {
                    string[] iller = File.ReadAllLines(ilDosyaYolu);//dosyanın tum satırlarını dizi olarak okur ve dosyadaki her satırı diziye atar
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

        private void button_ev_adres_bilgi_sorgula_Click(object sender, EventArgs e) //seçilen il ve ilçeye göre sorgulama
        {
            string selectedCity = comboBox_il.SelectedItem?.ToString();
            string selectedDistrict = comboBox_ilce.SelectedItem?.ToString();

            if (selectedCity != null && selectedDistrict != null)
            {
                string kayitDosyaYolu = "ev_kayitlari.txt";
                string kiralikDosyaYolu = "kiralik.txt";
                string satilikDosyaYolu = "satilik.txt";

                List<string> evler = new List<string>();

                try
                {
                    string[] evKayitlari = File.ReadAllLines(kayitDosyaYolu); //her bir dosyadan satırlar okunur
                    string[] kiralikEvler = File.Exists(kiralikDosyaYolu) ? File.ReadAllLines(kiralikDosyaYolu) : null; //dosya var mı
                    string[] satilikEvler = File.Exists(satilikDosyaYolu) ? File.ReadAllLines(satilikDosyaYolu) : null;

                    foreach (string evKaydi in evKayitlari)
                    {
                        string[] evBilgileri = evKaydi.Split('|');
                        if (evBilgileri[0] == selectedCity && evBilgileri[1] == selectedDistrict)
                        {
                            evler.Add(evKaydi); //eşleşen ev bilgileri evler listesine eklenir.
                        }
                    }

                    if (kiralikEvler != null)
                    {
                        foreach (string kiralikEv in kiralikEvler)
                        {
                            string[] evBilgileri = kiralikEv.Split('|');
                            if (evBilgileri[0] == selectedCity && evBilgileri[1] == selectedDistrict)
                            {
                                evler.Add(kiralikEv);
                            }
                        }
                    }

                    if (satilikEvler != null)
                    {
                        foreach (string satilikEv in satilikEvler)
                        {
                            string[] evBilgileri = satilikEv.Split('|');
                            if (evBilgileri[0] == selectedCity && evBilgileri[1] == selectedDistrict)
                            {
                                evler.Add(satilikEv);
                            }
                        }
                    }

                    if (evler.Count > 0)
                    {
                        listBox_evler.Items.Clear();
                        listBox_evler.Items.AddRange(evler.ToArray());
                    }
                    else
                    {
                        MessageBox.Show("Belirtilen il ve ilçeye ait ev bilgisi bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Lütfen il ve ilçe seçiniz.");
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

        private void button_ev_durum_bilgisi_sorgula_Click(object sender, EventArgs e) //secilen duruma(aktif,pasif) göre ev bilgisini sorgular
        {
            listBox_evler.Items.Clear();

            string[] dosyalar = { "ev_kayitlari.txt", "kiralik.txt", "satilik.txt" };
            string selectedCity = comboBox_il.SelectedItem?.ToString();
            string selectedDistrict = comboBox_ilce.SelectedItem?.ToString();
            string evDurum = radioButton_aktif.Checked ? "aktif" : "pasif";

            try
            {
                foreach (string dosya in dosyalar)
                {
                    if (File.Exists(dosya))
                    {
                        string[] evler = File.ReadAllLines(dosya);
                        foreach (string evBilgisi in evler)
                        {
                            string[] evDetay = evBilgisi.Split('|');
                            if (evDetay[0] == selectedCity && evDetay[1] == selectedDistrict && evDetay[8] == evDurum)
                            {
                                listBox_evler.Items.Add(evBilgisi);
                            }
                        }
                    }
                }

                if (listBox_evler.Items.Count == 0)
                {
                    MessageBox.Show("Belirtilen durumda ev bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e) //secilen il ve ilceye göre satılık veya kiralık evleri sorgular
        {
            listBox_evler.Items.Clear();

            string[] dosyalar = radioButton_satilik_ev.Checked ? new string[] { "satilik.txt" } : new string[] { "kiralik.txt" };

            string selectedCity = comboBox_il.SelectedItem?.ToString();
            string selectedDistrict = comboBox_ilce.SelectedItem?.ToString();

            try
            {
                foreach (string dosya in dosyalar)
                {
                    if (File.Exists(dosya))
                    {
                        string[] evler = File.ReadAllLines(dosya);
                        foreach (string evBilgisi in evler)
                        {
                            string[] evDetay = evBilgisi.Split('|');
                            if (evDetay[0] == selectedCity && evDetay[1] == selectedDistrict)
                            {
                                listBox_evler.Items.Add(evBilgisi);
                            }
                        }
                    }
                }

                if (listBox_evler.Items.Count == 0)
                {
                    MessageBox.Show("Belirtilen durumda ev bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button_genelEvVilgileri_sorgula_Click(object sender, EventArgs e) //il,ilçe,ev türü,alan,oda sayısı)
        {
            listBox_evler.Items.Clear();

            string selectedCity = comboBox_il.SelectedItem?.ToString();
            string selectedDistrict = comboBox_ilce.SelectedItem?.ToString();
            string selectedEvTur = comboBox_ev_turu.SelectedItem?.ToString();
            string evToplamAlani = textBox_toplam_alan.Text;
            string evOdaSayisi = textBox_oda_sayisi.Text;

            try
            {
                string kayitDosyaYolu = "ev_kayitlari.txt";
                string kiralikDosyaYolu = "kiralik.txt";
                string satilikDosyaYolu = "satilik.txt";

                string[] dosyalar = { kayitDosyaYolu, kiralikDosyaYolu, satilikDosyaYolu };

                foreach (string dosya in dosyalar)
                {
                    if (File.Exists(dosya))
                    {
                        string[] evler = File.ReadAllLines(dosya);
                        foreach (string evBilgisi in evler)
                        {
                            string[] evDetay = evBilgisi.Split('|');

                            // Seçilen şartlara göre filtreleme
                            if (evDetay[0] == selectedCity &&
                                evDetay[1] == selectedDistrict &&
                                evDetay[6] == selectedEvTur &&
                                (evToplamAlani == "" || evDetay[4].Contains(evToplamAlani)) &&
                                (evOdaSayisi == "" || evDetay[3].Contains(evOdaSayisi)))
                            {
                                listBox_evler.Items.Add(evBilgisi);
                            }
                        }
                    }
                }

                if (listBox_evler.Items.Count == 0)
                {
                    MessageBox.Show("Belirtilen şartlara uygun ev bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button_ev_silme_Click(object sender, EventArgs e) //seçilen evin bilgisini siler
        {
            if (listBox_evler.SelectedIndex != -1)
            {
                listBox_evler.Items.RemoveAt(listBox_evler.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz bir öğe seçin.");
            }
        }

        private void button_duzenle_Click(object sender, EventArgs e) //secilen ev bilgisini düzenle formuna aktarır
        {
            if (listBox_evler.SelectedItem != null)
            {
                string seciliSatir = listBox_evler.SelectedItem.ToString();
                if (seciliSatir != null)
                {
                    EvDuzenleme e_duzenleme = new EvDuzenleme();

                    // Seçili satırı EvDuzenleme formuna aktar
                    e_duzenleme.seciliEvBilgisi = seciliSatir;

                    e_duzenleme.Show();
                }
                else
                {
                    MessageBox.Show("Lütfen Bir Sonuç Seçiniz");
                }
            }
        }

    }
}
