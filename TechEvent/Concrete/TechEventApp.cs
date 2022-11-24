using System;
using System.Linq;

namespace TechEvent.Concrete
{
    internal class TechEventApp
    {
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
                Console.WriteLine("4. Hesabini Kapat");

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
                        user.HesapKapatma(user.kAdi, TechEventHelper.kullaniciListesi);
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

        void Create(TechEventKullanici user)
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
                user.OrganizatorKullanicisiIcinEtkinlikOlustur(name, date, city, description, count, answer == 'E');
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
            Console.Write("Organizatör için 1'e, diğer için 2'ye basın");
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

    }
}
