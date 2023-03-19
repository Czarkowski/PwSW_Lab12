using System;
using System.IO;
using Konta;

namespace TestKonta
{
    static class PolitykaBezpieczenstwa
    {
        public static int MinimalnaLiczbaZnakow { get; set; }
        public static string NazwaPliku { get; set; }

        static PolitykaBezpieczenstwa()
        {
            MinimalnaLiczbaZnakow = 5;
            NazwaPliku = "Audyt.txt";
        }

        public static void SprawdzDlogoscHasla(object sender, PrzedZmianaHaslaArgs e)
        {
            if (e.NoweHaslo.Length<MinimalnaLiczbaZnakow)
            {
                e.Cancel = true;
            }
        }

        public static void SprawdzCzyHasloJestPowtorzone(object sender,
            PrzedZmianaHaslaArgs e)
        {
            if (e.NoweHaslo == e.StareHaslo)
            {
                e.Cancel = true;
            }
        }

        public static void ZapisDoPliku(object sender, EventArgs e)
        {
            Console.WriteLine("zapis do pliku");
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(NazwaPliku, true);
                Konto k = (Konto)sender;
                sw.WriteLine("Zmiana hasła dla użytkownika {0}",
                    k.NazwaUzytkownika);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Konto k1 = new Konto("JanKowalski", "password");
            Konto k2 = new Konto("AndrzejNowak", "haslo");
            Konto.PrzedZmianaHasla += new PrzedZmianaHaslaHandler(
                PolitykaBezpieczenstwa.SprawdzCzyHasloJestPowtorzone);
            Konto.PrzedZmianaHasla += new PrzedZmianaHaslaHandler(
                PolitykaBezpieczenstwa.SprawdzDlogoscHasla);

            Konto.PoZmianieHasla += new EventHandler(PolitykaBezpieczenstwa.ZapisDoPliku);
            Konto.PoZmianieHasla += new EventHandler(PolitykaBezpieczenstwa.ZapisDoPliku);
            Konto.PoZmianieHasla -= new EventHandler(PolitykaBezpieczenstwa.ZapisDoPliku);
            Konto.PoZmianieHasla -= new EventHandler(PolitykaBezpieczenstwa.ZapisDoPliku);
            if (k1.ZmienHaslo("password","123"))
            {
                Console.WriteLine("Hasło Zmienione.");
            }
            else
            {
                Console.WriteLine("Hasła nie udało się zmienić.");
            }


            if (k2.ZmienHaslo("haslo", "haslo"))
            {
                Console.WriteLine("Hasło Zmienione.");
            }
            else
            {
                Console.WriteLine("Hasła nie udało się zmienić.");
            }


            if (k1.ZmienHaslo("password", "12345"))
            {
                Console.WriteLine("Hasło Zmienione.");
            }
            else
            {
                Console.WriteLine("Hasła nie udało się zmienić.");
            }


            if (k2.ZmienHaslo("haslo", "123haslo"))
            {
                Console.WriteLine("Hasło Zmienione.");
            }
            else
            {
                Console.WriteLine("Hasła nie udało się zmienić.");
            }
        }
    }
}
