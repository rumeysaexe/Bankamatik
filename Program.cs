using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankaQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double bakiye = 10000; //Bakiye
            string sifre = "nask123"; //Şifre
            anaMenu(bakiye, sifre); // anaMenu void'ine şifre(string) ve bakiyeyi(double) gönderdik.
        }
        static void gitMenu(double bakiye, string sifre, bool isCard) // to menu
        {
            Thread.Sleep(2000);
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("9) Ana menü\n0) Çıkış");
                Console.ResetColor();

                string choice1 = Console.ReadLine();
                switch (choice1)
                {
                    case "9":
                        islemMenusu(bakiye, sifre, isCard);
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Lütfen Geçerli Bir Değer Giriniz!!");
                        Console.ResetColor();
                        break;
                }
            }
        }
        static void anaMenu(double bakiye, string sifre) // cardMenu
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1) Kartlı işlemler\n2) Kartsız işlemler");
            Console.ResetColor();

            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":
                    girisKontrol(bakiye, sifre, false);
                    islemMenusu(bakiye, sifre, true);
                    break;
                case "2":
                    islemMenusu(bakiye, sifre, false);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lütfen geçerli bir menü seçiniz");
                    Console.ResetColor();
                    anaMenu(bakiye, sifre);
                    break;
            }
        }
        static void girisKontrol(double bakiye, string sifre, bool giriskontrol) // login
        {
            int denemehakki = 3;
            while (denemehakki > 0)
            {
                Console.WriteLine("Şifreyi giriniz: ");
                string girilenSifre = Console.ReadLine();
                denemehakki--;

                if (girilenSifre == sifre)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Giriş başarılı");
                    Console.ResetColor();
                    break;
                }
                else if (denemehakki == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Giriş hakkınız kalmadı");
                    Console.ResetColor();

                    if (giriskontrol)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("3 Kez hatalı şifre girdiniz, ana menüye yönlendiriliyorsunuz.");
                        Console.ResetColor();
                        anaMenu(bakiye, sifre);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("3 Kez hatalı şifre girdiniz, sistem kilitlendi.");
                        Console.ResetColor();
                        Thread.Sleep(5000);
                        anaMenu(bakiye, sifre);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Giriş başarısız kalan hak: " + denemehakki + "\nTekrar deneyiniz: ");
                    Console.ResetColor();
                }
            }
        }
        static void islemMenusu(double bakiye, string sifre, bool isCard) // mainMenu
        {
            Thread.Sleep(2000);
            if (isCard)
            {
                Console.WriteLine("1) Para çekme\n2) Para yatırma\n3) Para transferi\n4) Eğitim ödemeleri\n5) Diğer ödemeler\n6) Bilgilerim");
            }
            else
            {
                Console.WriteLine("1) Cepbank para çekme\n2) Cepbank para yatırma\n3) Eğitim ödemeleri\n4) Diğer ödemeler");
            }

            int secim = 0; // Kullanıcı girdisinin int değeri.
            bool girdi = false; // Girilen değeri kontrol eder.

            do // Girdi kontrolü için do-while döngüsü
            {
                string s = Console.ReadLine(); // Kullanıcının girdisini s(string) 'e yazar.

                if (int.TryParse(s, out secim)) //TryParse ifadesi bir değeri(sting s) başka bir değere(int secim) dönüştürmeyi dener.
                {
                    if (!isCard && (secim > 4 || secim <= 0) || (isCard && (secim > 6 || secim <= 0)))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Geçerli bir değer giriniz.");
                        Console.ResetColor();
                    }
                    else // Seçimin başarılı bir şekilde int değere dönüştürüldüğünü ifade eder.
                    {
                        girdi = true;
                    }
                }
                else // Girdinin string değerden int değere dönüştürülemediğini ifade eder.
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Geçerli bir değer giriniz.");
                    Console.ResetColor();
                }
            } while (!girdi); // do bağlamını kontrol eder.

            switch (secim)
            {
                case 1:
                    if (isCard)
                    {
                        bakiye = paraCek(bakiye, sifre);
                    }
                    else
                    {
                        paraCekCepBank(bakiye, sifre);
                    }
                    islemMenusu(bakiye, sifre, isCard);
                    break;
                case 2:
                    Console.WriteLine("1) Kredi Kartına para yatırma işlemleri\n2) Kendi hesabına para yatırma işlemleri\n9) Ana menü\n0) Çıkış");
                    int secim2;
                    bool girdi2 = false;
                    do
                    {
                        string s = Console.ReadLine();
                        if (int.TryParse(s, out secim2))
                        {
                            if ((secim2 >= 1 && secim2 <= 2) || (secim2 == 9 || secim2 == 0))
                            {
                                girdi2 = true;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                            Console.ResetColor();
                        }
                    } while (!girdi2);

                    switch (secim2)
                    {
                        case 1:
                            krediKartinaYatir(bakiye, sifre, isCard);
                            break;
                        case 2:
                            kendiHesabinaYatir(bakiye, sifre, isCard);
                            break;
                        case 9:
                            islemMenusu(bakiye, sifre, isCard);
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                    }
                    break;
                case 3:
                    if (!isCard)
                    {
                        egitimOdemesi(bakiye, sifre, isCard);
                        islemMenusu(bakiye, sifre, isCard);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("1) Başka hesaba EFT\n2) Başka hesaba havale\n9) Ana menü\n0) Çıkış");
                        bool girdi3 = false;
                        do
                        {
                            string secim3 = Console.ReadLine();
                            if (secim3 == "1" || secim3 == "2")
                            {
                                bakiye = transfer(bakiye, secim3);
                                girdi3 = true;
                            }
                            else if (secim3 == "9")
                            {
                                girdi3 = true;
                            }
                            else if (secim3 == "0")
                            {
                                Environment.Exit(0);
                                girdi3 = true;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                                Console.ResetColor();
                            }
                        } while (!girdi3);
                    }
                    islemMenusu(bakiye, sifre, isCard);
                    break;
                case 4:
                    if (isCard)
                    {
                        egitimOdemesi(bakiye, sifre, isCard);
                        islemMenusu(bakiye, sifre, isCard);
                        break;
                    }
                    else
                    {
                        odemeler(bakiye, sifre, isCard);
                        islemMenusu(bakiye, sifre, isCard);
                        break;
                    }
                    islemMenusu(bakiye, sifre, isCard);
                    break;
                case 5:
                    odemeler(bakiye, sifre, isCard);
                    islemMenusu(bakiye, sifre, isCard);
                    break;
                case 6:
                    if (isCard)
                    {
                        sifre = sifreDegistir(bakiye, sifre);
                        islemMenusu(bakiye, sifre, isCard);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                        Console.ResetColor();
                    }
                    break;
            }
        }
        static double paraCek(double bakiye, string sifre) // withdraw
        {
            if (bakiye > 0)
            {
                Console.WriteLine("Çekilecek tutarı giriniz: ");
                double cekilcekTutar = 0;
                bool girdi = false;
                do
                {
                    string s = Console.ReadLine();
                    if (double.TryParse(s, out cekilcekTutar))
                    {
                        girdi = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                        Console.ResetColor();
                    }
                } while (!girdi);

                if (cekilcekTutar <= bakiye && cekilcekTutar > 5)
                {
                    bakiye -= cekilcekTutar;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("İşlem başarılı. Ana menüye yönlendiriliyorsunuz.\nKalan bakiye: " + bakiye);
                    Console.ResetColor();
                }
                else if (cekilcekTutar <= bakiye && cekilcekTutar < 5)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("En az 5TL işlem yapmalısınız. Ana menüye yönlendiriliyorsunuz.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bakiyeniz yetersiz!\nYapılmak istenen tutar: " + cekilcekTutar + "\nBakiyeniz: " + bakiye + "\nGerekli tutar: " + (cekilcekTutar - bakiye));
                    Console.ResetColor();
                    gitMenu(bakiye, sifre, true);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bakiye Yetersiz!!");
                Console.ResetColor();
                gitMenu(bakiye, sifre, true);
            }
            return bakiye;
        }
        static void paraCekCepBank(double bakiye, string sifre)// withdrawCepBank
        {
            long tcno = 0;
            long telno = 0;
            bool control = false;

            do
            {
                Console.WriteLine("Lütfen TC kimlik numaranızı giriniz: ");
                string stcno = Console.ReadLine();

                if (long.TryParse(stcno, out tcno) && stcno.Length == 11)
                {
                    bool contol2 = false;
                    do
                    {
                        Console.WriteLine("Lütfen başında '0' olmadan telefon numaranızı giriniz: ");
                        string stelno = Console.ReadLine();

                        if (long.TryParse(stelno, out telno) && stelno.Length == 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Ödeme başarılı.");
                            Console.ResetColor();
                            gitMenu(bakiye, sifre, false);
                            contol2 = true;
                            control = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Telefon numarası hatalı.");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("1) Tekrar dene\n9) Ana menü\n0) Çıkış");
                            Console.ResetColor();

                            string secim = Console.ReadLine();
                            if (secim == "9")
                            {
                                islemMenusu(bakiye, sifre, false);
                                break;
                            }
                            else if (secim == "0")
                            {
                                Environment.Exit(0);
                            }
                        }
                    } while (!contol2);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("TC Kimlik numarası hatalı.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1) Tekrar dene\n9) Ana menü\n0) Çıkış");
                    Console.ResetColor();

                    string secim = Console.ReadLine();
                    if (secim == "9")
                    {
                        islemMenusu(bakiye, sifre, false);
                        break;
                    }
                    else if (secim == "0")
                    {
                        Environment.Exit(0);
                    }
                }
            } while (!control);
        }
        static void krediKartinaYatir(double bakiye, string sifre, bool isCard) // depositToCredit
        {
            bool control = false;
            long cc = 0;
            double yatirilcaktutar = 0;
            do
            {
                Console.WriteLine("Kredi kartı numarasını giriniz: ");
                string s = Console.ReadLine();
                if (long.TryParse(s, out cc) && s.Length == 12)
                {
                    bool control2 = false;
                    do
                    {
                        Console.WriteLine("Yatırmak istediğiniz tutarı giriniz: ");
                        string s2 = Console.ReadLine();
                        if (double.TryParse(s2, out yatirilcaktutar))
                        {
                            if (yatirilcaktutar > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Hesabınıza " + yatirilcaktutar + " TL yatırılmıştır.");
                                Console.ResetColor();
                                control = true;
                                control2 = true;
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("En az 1TL işlem yapılmalıdır.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("E) Menüye dön.\nLütfen geçerli bir değer giriniz. (ör. 650.30)");
                            string secim2 = Console.ReadLine();
                            if (secim2.ToUpper() == "E")
                            {
                                gitMenu(bakiye, sifre, isCard);
                                break;
                            }
                            else
                            {
                                Console.ResetColor();
                            }
                        }
                    } while (!control2);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hatalı kredi kartı numarası girdiniz. Kredi kartı numarası 12 haneli olmalıdır.\n1) Tekrar dene\n9) Ana menü\n0) Çıkış");
                    Console.ResetColor();
                    bool control3 = false;
                    do
                    {
                        string secim = Console.ReadLine();
                        if (secim == "1")
                        {
                            control3 = true;
                        }
                        else if (secim == "9")
                        {
                            islemMenusu(bakiye, sifre, isCard);
                            break;
                        }
                        else if (secim == "0")
                        {
                            Environment.Exit(0);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                        }
                    } while (!control3);
                }
            } while (!control);
            gitMenu(bakiye, sifre, isCard);
        }
        static void kendiHesabinaYatir(double bakiye, string sifre, bool isCard) // depositToOwnAccount
        {
            long aliciNO = 0;
            double yatirilcaktutar = 0;
            if (isCard)
            {
                bool control = false;
                do
                {
                    Console.WriteLine("Yatırmak istediğiniz tutarı giriniz: ");
                    string s = Console.ReadLine();
                    if (double.TryParse(s, out yatirilcaktutar))
                    {
                        if (yatirilcaktutar > 0)
                        {
                            bakiye += yatirilcaktutar;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Hesabınıza " + yatirilcaktutar + " TL yatırılmıştır.\nGüncel bakiyeniz: " + bakiye);
                            Console.ResetColor();
                            control = true;
                            gitMenu(bakiye, sifre, isCard);
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("En az 1TL işlem yapılmalıdır.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("E) Menüye dön.\nLütfen geçerli bir değer giriniz. (ör. 650.30)");
                        string secim2 = Console.ReadLine();
                        if (secim2.ToUpper() == "E")
                        {
                            gitMenu(bakiye, sifre, isCard);
                            break;
                        }
                        else
                        {
                            Console.ResetColor();
                        }
                    }
                } while (!control);
                gitMenu(bakiye, sifre, true);
            }
            else
            {
                bool control2 = false;
                do
                {
                    Console.WriteLine("Hesap Numarasını giriniz (11 Haneli olmalı): ");
                    string s2 = Console.ReadLine();
                    if (long.TryParse(s2, out aliciNO) && s2.Length == 11)
                    {
                        bool control3 = false;
                        do
                        {
                            Console.WriteLine("İşlem tutarını giriniz: ");
                            string s3 = Console.ReadLine();
                            if (double.TryParse(s3, out yatirilcaktutar))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Hesabınıza {yatirilcaktutar} TL yatırılmıştır.\nAna menüye yönlendiriliyorsunuz...");
                                Console.ResetColor();
                                control3 = true;
                                control2 = true;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Geçerli bir tutar giriniz.");
                                Console.ResetColor();
                            }
                        } while (!control3);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("E) Menüye dön.\nHesap numarasını hatalı girdiniz.");
                        Console.ResetColor();
                        string secim2 = Console.ReadLine();
                        if (secim2.ToUpper() == "E")
                        {
                            gitMenu(bakiye, sifre, false);
                            break;
                        }
                    }
                } while (!control2);
                gitMenu(bakiye, sifre, false);
            }
        }
        static double transfer(double bakiye, string secim3) // transfer
        {
            string aliciIBAN = " ";
            long aliciNO = 0;

            while (true)
            {
                if (secim3 == "1")
                {
                    Console.WriteLine("IBAN Numarasını giriniz (TR ile başlamalı): ");
                    aliciIBAN = Console.ReadLine().ToUpper();
                    if (aliciIBAN.Length == 26 && aliciIBAN.StartsWith("TR"))
                    {
                        bakiye = transferMetodu(bakiye);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Transfer işlemi başarılı. Yeni bakiye: " + bakiye);
                        Console.ResetColor();
                        break;
                    }
                    else // Yanlış girilmesi durumunda bir hakkı daha oluyor kullanıcının
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Hatalı IBAN. Lütfen IBAN'ınızın TR ile başladığından ve toplamda 26 karakter uzunluğunda rakamlardan oluştuğundan emin olun.");
                        Console.ResetColor();
                        Console.WriteLine("IBAN Numarasını giriniz (TR ile başlamalı): ");
                        aliciIBAN = Console.ReadLine().ToUpper();
                        if (aliciIBAN.Length == 26 && aliciIBAN.StartsWith("TR"))
                        {
                            bakiye = transferMetodu(bakiye);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Transfer işlemi başarılı. Yeni bakiye: " + bakiye);
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Hatalı IBAN. Ana menüye yönlendiriliyorsunuz.");
                            Console.ResetColor();
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Hesap Numarasını giriniz (11 Haneli olmalı): ");
                    string girdi = Console.ReadLine();
                    if (long.TryParse(girdi, out aliciNO) && girdi.Length == 11)
                    {
                        bakiye = transferMetodu(bakiye);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Transfer işlemi başarılı. Yeni bakiye: " + bakiye);
                        Console.ResetColor();
                        break;
                    }
                    else // Yanlış girilmesi durumunda bir hakkı daha oluyor kullanıcının
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Hatalı hesap numarası. Lütfen hesap numaranızın 11 karakter uzunluğunda olduğundan ve sadece rakamlardan oluştuğundan emin olun.");
                        Console.ResetColor();
                        Console.WriteLine("Hesap Numarasını giriniz: ");
                        string girdi2 = Console.ReadLine();
                        if (long.TryParse(girdi2, out aliciNO) && girdi2.Length == 11)
                        {
                            bakiye = transferMetodu(bakiye);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Transfer işlemi başarılı. Yeni bakiye: " + bakiye);
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Hatalı hesap numarası. Ana menüye yönlendiriliyorsunuz.");
                            Console.ResetColor();
                            break;
                        }
                    }
                }
            }

            return bakiye;
        }
        static double transferMetodu(double bakiye) // transferMethod
        {
            bool control = false;
            do
            {
                Console.WriteLine("Lütfen işlem tutarı giriniz: ");
                string s = Console.ReadLine();
                double islemtutari = 0;
                if (double.TryParse(s, out islemtutari))
                {
                    if (islemtutari <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("İşlem tutarı en az 1TL olmak zorundadır.");
                        Console.ResetColor();
                    }
                    else if (islemtutari > bakiye)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("İşlem tutarı bakiyenizi aşıyor.\nMevcut bakiyeniz: " + bakiye);
                        Console.ResetColor();
                    }
                    else
                    {
                        bakiye -= islemtutari;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("İşlem başarılı.\nGüncel bakiye: " + bakiye);
                        Console.ResetColor();
                        control = true;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                    Console.ResetColor();
                }
            } while (!control);

            return bakiye;
        }
        static string sifreDegistir(double bakiye, string sifre) // changePassword
        {

            girisKontrol(bakiye, sifre, true);

            Console.WriteLine("Yeni Şifreyi Giriniz: ");
            string yeniSifre = Console.ReadLine();

            Console.WriteLine("Yeni Şifreyi Tekrar Giriniz: ");
            string yeniSifreTekrar = Console.ReadLine();


            if (yeniSifre == yeniSifreTekrar)
            {

                if (yeniSifre != sifre)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Şifre başarıyla değiştirildi.");
                    Console.ResetColor();
                    sifre = yeniSifre;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Yeni şifreniz mevcut şifrenizle aynı olamaz.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Girdiğiniz şifreler uyuşmuyor.");
                Console.ResetColor();
                gitMenu(bakiye, sifre, true);
            }

            return sifre;
        }
        static void egitimOdemesi(double bakiye, string sifre, bool isCard)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bu hizmet geçici süreyle durdurulmuştur.");
            Console.ResetColor();
            gitMenu(bakiye, sifre, isCard);
        }
        static void odemeler(double bakiye, string sifre, bool isCard)
        {
            double tutar = 0;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1) Elektrik faturası\n2) Su faturası\n3) İnternet faturası\n4) Telefon faturası\n5) OGS faturası\n9) Ana menü\n0) Çıkış");
                Console.ResetColor();
                string secim = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                if (secim == "1")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Elektrik");
                    Console.ResetColor();
                    break;
                }
                else if (secim == "2")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Su");
                    Console.ResetColor();
                    break;
                }
                else if (secim == "3")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("İnternet");
                    Console.ResetColor();
                    break;
                }
                else if (secim == "4")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Telefon");
                    Console.ResetColor();
                    break;
                }
                else if (secim == "5")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("OGS");
                    Console.ResetColor();
                    break;
                }
                else if (secim == "9")
                {
                    gitMenu(bakiye, sifre, isCard);
                    break;
                }
                else if (secim == "0")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                    Console.ResetColor();
                }
            }


            while (true) // Sonsuz döngü
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" faturası tutarını giriniz: ");
                Console.ResetColor();
                string stutar = Console.ReadLine();

                if (double.TryParse(stutar, out tutar))
                {
                    if (tutar < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Fatura tutarı sıfırdan düşük olamaz.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Fatura başarıyla ödendi.");
                        Console.ResetColor();
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Geçersiz tutar girdiniz.");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("1) Tekrar dene\n9) Ana menü\n0) Çıkış");
                    Console.ResetColor();
                    string girdi = Console.ReadLine();

                    if (girdi == "1")
                    {

                    }
                    else if (girdi == "9")
                    {
                        gitMenu(bakiye, sifre, isCard);
                        break;
                    }
                    else if (girdi == "0")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz cevap, tekrar denemek için 1'e basın.");
                    }
                }
            }


        }
    }
}
