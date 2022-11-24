using System;
using System.Collections.Generic;
using System.Linq;

namespace TechEvent.Concrete
{
    public class Etkinlik
    {
        public Etkinlik(string etkinlikAdi, DateTime etkinlikTarihi, string etkinliginOlduguSehir, string aciklama, int katilacakKisiSayisi, bool biletliMi)
        {
            EtkinlikSehriniKontrolEt(etkinliginOlduguSehir);
            EtkinlikTarihiKontrolEt(etkinlikTarihi);
            KisiSayisiniKontrolEt(katilacakKisiSayisi);

            EtkinlikAdi = etkinlikAdi;
            EtkinlikTarihi = etkinlikTarihi;
            EtkinliginOlduguSehir = etkinliginOlduguSehir;
            Aciklama = aciklama;
            KatilacakKisiSayisi = katilacakKisiSayisi;
            BiletliMi = biletliMi;
            KatilacakKisiler = new List<TechEventKullanici>();
        }

        public string EtkinlikAdi { get; set; }
        public DateTime EtkinlikTarihi { get; set; }
        public string EtkinliginOlduguSehir { get; set; }
        public string Aciklama { get; set; }
        public int KatilacakKisiSayisi { get; set; }
        public bool BiletliMi { get; set; }
        public List<TechEventKullanici> KatilacakKisiler { get; set; }

        void EtkinlikTarihiKontrolEt(DateTime dateTime)
        {
            if (dateTime < DateTime.Now.AddMonths(1))
            {
                throw new Exception("Etkinlik tarihi en erken 1 ay sonra olmalıdır");
            }
        }

        void EtkinlikSehriniKontrolEt(string sehir)
        {
            string[] sehirler = { "Istanbul", "Ankara", "Izmir" };
            if (!sehirler.Contains(sehir))
            {
                throw new Exception("Şu an için İstanbul,Ankara ve İzmir'de etkinlik düzenlenebilir");
            }
        }

        void KisiSayisiniKontrolEt(int sayi)
        {
            if (sayi < 15)
            {
                throw new Exception("En az 15 kişilik etkinlik oluşturulabilir");
            }
        }
    }
}
