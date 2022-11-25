using System;

namespace TechEvent.Concrete
{
    public class Katilimci : TechEventKullanici
    {
        public void EtkinligeKatil(Etkinlik etkinlik)
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
        public void BiletAL()
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
