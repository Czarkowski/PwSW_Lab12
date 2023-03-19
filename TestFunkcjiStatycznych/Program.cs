using System;
using Statystyka;

namespace TestFunkcjiStatycznych
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] tab = { 2, 3, 6, 9 };
            Console.WriteLine("Liczba elementów tablicy większych od pięciu: {0}",
                FunkcjeStatyczne.Ilosc(tab, p => p > 5));

            Console.WriteLine("Suma elementów tablicy większych od pięciu: {0}",
                FunkcjeStatyczne.Suma(tab, p => p > 5));

            Console.WriteLine("Średnia elementów tablicy większych od pięciu: {0}",
                FunkcjeStatyczne.SredniaArytmetyczna(tab, p => p > 5));

            Console.WriteLine("Największy elementów tablicy większych od pięciu: {0}",
                FunkcjeStatyczne.Maksimum(tab, p => p > 5));

            Console.WriteLine("Najmniejszy elementów tablicy większych od pięciu: {0}",
                FunkcjeStatyczne.Minimum(tab, p => p > 5));
        }
    }
}
