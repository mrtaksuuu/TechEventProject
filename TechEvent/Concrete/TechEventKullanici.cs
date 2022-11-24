﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TechEvent.Concrete
{
    public class TechEventKullanici
    {
        public string ad;
        public string soyad;
        public string kAdi;
        public string sifre;
        public int tip; // 1 olursa organizatör, 2 olursa bilet satın alacak kullanıcı


        public bool Kaydol(string ad, string soyad, string kAdi, string sifre, int tip)
        {
            foreach (TechEventKullanici item in TechEventHelper.kullaniciListesi)
            {
                if (item.kAdi == kAdi)
                {
                    throw new Exception("Bu kullanıcı adı zaten kayıtlı");
                }
            }

            if (sifre.Length < 6 || TechEventHelper.BuyukHarfliSifre(sifre) == false || TechEventHelper.KucukHarfliSifre(sifre) == false)
            {
                throw new Exception("Şifre en az 6 karakterli olmalı ve en az 2 büyük ve küçük harf içermelidir");
            }

            if (!(tip == 1 || tip == 2))
            {
                throw new Exception("Organizatör için 1'e, diğer için 2'ye basılması gerekmektedir.");
            }

            return true;
        }

        public bool Giris(string kadi, string sifre, List<TechEventKullanici> kullanicilar)
        {
            foreach (TechEventKullanici item in kullanicilar)
            {
                if (item.kAdi == kadi && item.sifre == sifre)
                {
                    return true;
                }
            }

            return false;
        }

        public void HesapKapatma(string kullanici, List<TechEventKullanici> kullaniciListesi)
        {
            // Hata veriyor, düzeltilecek
            //foreach (TechEventKullanici item in kullaniciListesi)
            //{
            //    if (item.kAdi == kullanici)
            //    {
            //        kullaniciListesi.Remove(item);
            //    }
            //}
        }

        public void OrganizatorKullanicisiIcinEtkinlikOlustur(string etkinlikAdi, DateTime etkinlikTarihi, string etkinliginOlduguSehir, string aciklama, int katilacakKisiSayisi, bool biletliMi)
        {
            if (tip == 1)
            {
                try
                {
                    Etkinlik etkinlik = new Etkinlik(etkinlikAdi, etkinlikTarihi, etkinliginOlduguSehir, aciklama, katilacakKisiSayisi, biletliMi);
                    TechEventHelper.etkinlikListesi.Add(etkinlik);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Sadece organizatör rolündeki kullanıcılar etkinlik oluşturabilir");
            }
        }

        public void BirEtkinligeKatilma(Etkinlik etkinlik)
        {
            if (tip == 2)
            {
                if (etkinlik.KatilacakKisiler.Count < etkinlik.KatilacakKisiSayisi)
                {
                    etkinlik.KatilacakKisiler.Add(this);
                }
                else
                {
                    throw new Exception("Etkinliğin kontenjanı dolmuştur");
                }
            }
            else
            {
                throw new Exception("Sadece katilimci rolündeki kullanıcılar bir etkinliğe katılabilir");
            }
        }

        public void BiletAl()
        {
            foreach (BiletFirmalari item in TechEventHelper.biletFirmalari)
            {
                if (!item.DataCekildiMi)
                {
                    throw new Exception($"{item.FirmaAdi} firmasında biletler henüz satışa çıkmamıştır");
                }
            }

            Console.WriteLine("Aşağıdaki adreslerden bilet alabilirsiniz");
            TechEventHelper.biletFirmalari.ForEach(firma =>
            {
                Console.WriteLine($"{firma.FirmaAdi} -> {firma.WebSitesi}");
            });
        }
    }
}
