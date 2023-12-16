using System;

namespace EmlakOtomasyonu // Projenize uygun bir namespace kullanabilirsiniz
{
    public enum EvTuru
    {
        Daire,
        Bahceli,
        Dubleks,
        Mustakil,
        // Diğer ev türlerini enum olarak ekleyebilirsiniz
    }

    public class Ev
    {
        // Alanlar (Fields)
        private int odaSayisi;
        private int katNumarasi;
        private string semt;
        private double alani;
        private DateTime yapimTarihi;
        private EvTuru turu;
        private bool aktif;
        private string emlakNumarasi;

        // Kiralık ev için
        private double kira;
        private double depozito;

        // Satılık ev için
        private double fiyat;

        // Özellikler (Properties)
        public int OdaSayisi
        {
            get => odaSayisi;
            set
            {
                if (value >= 0)
                {
                    odaSayisi = value;
                }
                else
                {
                    odaSayisi = 0;
                    // Negatif değer girilmesi durumunda bir log kaydı tutulabilir
                    // Örneğin: LogKaydiOlustur("OdaSayisi", "Negatif değer girildi");
                }
            }
        }

        // Diğer özellikler için de benzer şekilde property'leri tanımlayabilirsiniz

        // Kurucu Metot (Constructor)
        public Ev(int odaSayisi, int katNumarasi, string semt, double alani, DateTime yapimTarihi, EvTuru turu, string emlakNumarasi)
        {
            this.OdaSayisi = odaSayisi;
            this.katNumarasi = katNumarasi;
            this.semt = semt;
            this.alani = alani;
            this.yapimTarihi = yapimTarihi;
            this.turu = turu;
            this.emlakNumarasi = emlakNumarasi;
            this.aktif = true; // Yeni ev oluşturulduğunda varsayılan olarak aktif olarak ayarlanabilir
        }

        // EvBilgileri metodu
        public virtual string EvBilgileri()
        {
            return string.Format("Emlak Numarası: {0}, Türü: {1}, Alanı: {2}", emlakNumarasi, turu, alani);
        }

        // FiyatHesapla metodu
        public double FiyatHesapla()
        {
            // Fiyat hesaplama işlemleri burada yapılabilir
            return 0; // Fiyat hesaplama mantığına göre döndürülecek değer
        }
    }
    public class KiralikEv : Ev
    {
        // Kiralık ev için özel özellikler
        public double Depozito { get; set; }
        public double Kira { get; set; }

        public KiralikEv(int odaSayisi, int katNumarasi, string semt, double alani, DateTime yapimTarihi, EvTuru turu, string emlakNumarasi)
            : base(odaSayisi, katNumarasi, semt, alani, yapimTarihi, turu, emlakNumarasi)
        {
            // Kiralık ev özelliklerini başlatma
        }

        public override string EvBilgileri()
        {
            // EvBilgileri metodu KiralıkEv için özelleştirilebilir
            return base.EvBilgileri() + string.Format(", Kira: {0}, Depozito: {1}", Kira, Depozito);
        }
    }

    public class SatilikEv : Ev
    {
        // Satılık ev için özel özellikler
        public double Fiyat { get; set; }

        public SatilikEv(int odaSayisi, int katNumarasi, string semt, double alani, DateTime yapimTarihi, EvTuru turu, string emlakNumarasi)
            : base(odaSayisi, katNumarasi, semt, alani, yapimTarihi, turu, emlakNumarasi)
        {
            // Satılık ev özelliklerini başlatma
        }

        public override string EvBilgileri()
        {
            // EvBilgileri metodu SatilikEv için özelleştirilebilir
            return base.EvBilgileri() + string.Format(", Fiyat: {0}", Fiyat);
        }
    }
}
