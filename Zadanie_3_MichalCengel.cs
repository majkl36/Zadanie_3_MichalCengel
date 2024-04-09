using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zadanie_3_MichalCengel
{
    public class NespravnyVstupException : Exception
    {
        public NespravnyVstupException() : base() { }
        public NespravnyVstupException(string message) : base(message) { }
        public NespravnyVstupException(string message, Exception innerException) : base(message, innerException) { }
    }
    class Konverzia
    {
        private List<string> povodneData;
        private List<string> spracovaneData;
        public Konverzia(string vstupnySubor)
        {
            povodneData = NacitajSubor(vstupnySubor);
            spracovaneData = VyhodnotData(povodneData);
        }
        private List<string> NacitajSubor(string nazovSuboru)
        {
            Console.WriteLine("Načítavam súbor s adresou:\n" + Path.GetFullPath(nazovSuboru) + "\n");
            List<string> obsahSuboru = new List<string>(File.ReadAllLines(Path.GetFullPath(nazovSuboru)));
            Console.WriteLine("Súbor načítaný!\n");
            return obsahSuboru;
        }
        public void ZapisDoSuboru(string nazovSuboru)
        {
            Console.WriteLine("Zapisujem skonvertované dáta do súboru {0}", nazovSuboru);
            char volba;
            while (File.Exists(nazovSuboru))
            {
                try
                {
                    Console.Write("Súbor už existuje. Prepísať? (y/n): ");
                    if (!char.TryParse(Console.ReadLine(), out volba))
                        throw new NespravnyVstupException("Odpoveď nerozpoznaná.");
                    if (!(volba.Equals('y') || volba.Equals('n')))
                        throw new NespravnyVstupException("Odpoveď nerozpoznaná.");
                    if (volba.Equals('n'))
                    {
                        Console.WriteLine("Súbor nebol prepísaný.");
                        return;
                    }
                }
                catch (NespravnyVstupException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                break;
            }
            File.WriteAllLines(nazovSuboru, spracovaneData,Encoding.Default);
            Console.WriteLine("Skonvertované dáta zapísané úspešne!");
        }
        private List<string> VyhodnotData(List<string> dataNaAnalyzu)
        {
            List<string> analyzovaneData = new List<string>();
            foreach (string s in dataNaAnalyzu)
            {
                analyzovaneData.Add(SpracujRiadok(s));
            }
            return analyzovaneData;
        }
        private string SpracujRiadok(string riadokNaSpracovanie)
        {
            string spracovanyRiadok = null;

            spracovanyRiadok = riadokNaSpracovanie;

            return spracovanyRiadok;
        }
        public void VypisPovodne()
        {
            foreach (string s in povodneData)
                Console.WriteLine(s);
        }
        public void VypisSpracovane()
        {
            foreach (string s in spracovaneData)
                Console.WriteLine(s);
        }
    }

    internal class Zadanie_3_MichalCengel
    {
        static void Main(string[] args)
        {
            try
            {
                Konverzia konv = new Konverzia("Zadanie_03_Vstup.txt");
                konv.ZapisDoSuboru("zidan.txt");
                //konv.VypisPovodne();
                //konv.VypisSpracovane();

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Vstupný súbor nenájdený!");
            }
            catch (IOException)
            {
                Console.WriteLine("Chyba pri práci so súborom");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}