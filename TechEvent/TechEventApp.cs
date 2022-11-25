using System;
using System.Linq;
using TechEvent.Concrete;

namespace TechEvent
{
    internal class TechEventApp
    {
        private Katilimci katilimci;

        public void Start()
        {
            Console.WriteLine("TechEvent uygulamasına hoş geldiniz.");
            Console.WriteLine();
            Console.WriteLine("1. Kayıt Ol");
            Console.WriteLine("2. Giriş Yap");

            int x = int.Parse(Console.ReadLine());
            if (x == 1)
            {
                bool isSuccess;
                do
                {
                    isSuccess = AddUser();
                } while (!isSuccess);
            }

            TechEventKullanici result;
            do
            {
                result = LoginUser();
            } while (result == null);

            LoadOperations(result);
        }

        void LoadOperations(TechEventKullanici user)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("1. Etkinlik Oluştur");
                Console.WriteLine("2. Etkinliğe Katıl");
                Console.WriteLine("3. Bilet Al");
                Console.WriteLine("4. Etkinlikleri Görüntüle");

                int y = int.Parse(Console.ReadLine());
                switch (y)
                {
                    case 1:
                        Create(user);
                        break;
                    case 2:
                        Join(user);
                        break;
                    case 3:
                        Sale(user);
                        break;
                    case 4:
                        OrnekEtkinlik(((Katilimci)katilimci));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Sale(TechEventKullanici user)
        {
            try
            {
                user.BiletAl();
            }
            catch (Exception ex)
            {
                foreach (BiletFirmalari item in TechEventHelper.biletFirmalari)
                {
                    item.EtkinlikleriXMLOlarakAlir();
                    item.EtkinlikleriJSONOlarakAlir();
                }

                user.BiletAl();
            }
        }

        void Join(TechEventKullanici user)
        {
            Console.WriteLine("Katılmak istediğiniz etkinliği seçin");
            foreach (Etkinlik item in TechEventHelper.etkinlikListesi)
            {
                Console.WriteLine("1. " + item.EtkinlikAdi);
            }

            int z = int.Parse(Console.ReadLine());
            Etkinlik @event = TechEventHelper.etkinlikListesi[z - 1];

            try
            {
                user.BirEtkinligeKatilma(@event);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Create(TechEventKullanici Organizator)
        {
            Console.Write("Etkinlik adı : ");
            string name = Console.ReadLine();
            Console.Write("Açıklama : ");
            string description = Console.ReadLine();
            Console.Write("Tarih : ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("Kişi Sayısı : ");
            int count = int.Parse(Console.ReadLine());
            Console.Write("Hangi şehir : ");
            string city = Console.ReadLine();
            Console.Write("Biletli mi (E\\H) : ");
            char answer = char.Parse(Console.ReadLine());

            try
            {
                Organizator.EtkinlikOLustur(name, date, city, description, count, answer == 'E');
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        TechEventKullanici LoginUser()
        {
            Console.Write("Kullanıcı adı : ");
            string user = Console.ReadLine();
            Console.Write("Şifre : ");
            string password = Console.ReadLine();

            TechEventKullanici appUser = new TechEventKullanici();
            bool result = appUser.Giris(user, password, TechEventHelper.kullaniciListesi);
            if (result)
            {
                Console.WriteLine("Giriş başarılı");
                appUser = TechEventHelper.kullaniciListesi.SingleOrDefault(a => a.kAdi == user && a.sifre == password);
                return appUser;
            }
            else
            {
                Console.WriteLine("Giriş başarısız");
                return null;
            }
        }

        bool AddUser()
        {
            Console.Write("Ad : ");
            string name = Console.ReadLine();
            Console.Write("Soyad : ");
            string surname = Console.ReadLine();
            Console.Write("Kullanıcı adı : ");
            string user = Console.ReadLine();
            Console.Write("Şifre : ");
            string password = Console.ReadLine();
            Console.Write("Organizatör için 1'e,  Kullanıcı için 2'ye basın");
            int type = int.Parse(Console.ReadLine());

            try
            {
                TechEventKullanici appUser = new TechEventKullanici();
                bool result = appUser.Kaydol(name, surname, user, password, type);
                if (result)
                {
                    TechEventHelper.kullaniciListesi.Add(new TechEventKullanici() { ad = name, soyad = surname, kAdi = user, sifre = password, tip = type });
                    return true;
                }
                else
                {
                    Console.WriteLine("Kayıt başarısız");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void OrnekEtkinlik(Katilimci katilimci)
        {

            Etkinlik etkinlik1 = new Etkinlik();
            etkinlik1.EtkinlikAdi = "Konser";
            etkinlik1.EtkinlikTarihi = DateTime.Parse("2022,11,25");
            etkinlik1.EtkinliginOlduguSehir = "İstanbul";
            etkinlik1.Aciklama = "Müslüm Gürses anısına";
            Console.WriteLine("1.Etkinlik ");
            Console.WriteLine
                ("Etkinlik Adı: " + etkinlik1.EtkinlikAdi
                + "\nEtkinlik Tarihi :" + etkinlik1.EtkinlikTarihi
                + "\nEtkinliğin olacağı şehir: " + etkinlik1.EtkinliginOlduguSehir
                + "\nEtkinlik Açıklması: " + etkinlik1.Aciklama);

            Console.WriteLine("2.Etkinlik ");
            Etkinlik etkinlik2 = new Etkinlik();
            etkinlik2.EtkinlikAdi = "Anıtkabir";
            etkinlik2.EtkinlikTarihi = DateTime.Parse("2022,12,21");
            etkinlik2.EtkinliginOlduguSehir = "Ankara";
            etkinlik2.Aciklama = "Atamıza ziyaret düzenlenecektir";

            Console.WriteLine
                ("Etkinlik Adı: " + etkinlik2.EtkinlikAdi
                + "\n Etkinlik Tarihi :" + etkinlik2.EtkinlikTarihi
                + "\n Etkinliğin olacağı şehir: " + etkinlik2.EtkinliginOlduguSehir
                + "\nEtkinlik Açıklması: " + etkinlik2.Aciklama);


        }
    }
}
