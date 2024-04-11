using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly List<string> povodneData;
        private readonly List<string> spracovaneData;
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
            Console.WriteLine("Zapisujem skonvertované dáta do súboru {0}\n", nazovSuboru);
            while (File.Exists(nazovSuboru))
            {
                try
                {
                    Console.Write("Súbor už existuje. Prepísať? (y/n): ");
                    if (!char.TryParse(Console.ReadLine(), out char volba))
                        throw new NespravnyVstupException("Nesprávne zadaná odpoveď.");
                    if (!(volba.Equals('y') || volba.Equals('n')))
                        throw new NespravnyVstupException("Odpoveď nerozpoznaná.");
                    if (volba.Equals('n'))
                    {
                        Console.WriteLine("\nSúbor nebol prepísaný.\n");
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
            File.WriteAllLines(nazovSuboru, spracovaneData, Encoding.Default);
            Console.WriteLine("\nOperácia konverzie dokončená!\n");
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
            string[] casti = riadokNaSpracovanie.Split(' ');
            if (casti.Length < 3)
            {
                return "Neplatné údaje pre konverziu!";
            }
            string spracovanyRiadok = casti[0] + ' ' + casti[1] + "\t" + (casti.Length - 2) + "\t";
            if ((casti.Length - 2) == 1)
            {
                spracovanyRiadok += casti[2];
                return spracovanyRiadok;
            }
            else
            {
                double[] meranie = new double[casti.Length - 2];

                for (int i = 2; i < casti.Length; i++)
                {
                    try
                    {
                        if (!double.TryParse(casti[i], out meranie[i - 2]))
                            throw new NespravnyVstupException("Konverzia_neúspešná_pre_nesprávny_formát_vstupných_údajov!");
                    }
                    catch (NespravnyVstupException ex)
                    {
                        return spracovanyRiadok + ex.Message;
                    }
                }
                double average = Math.Round(meranie.Average(), 2);
                spracovanyRiadok += average;
                return spracovanyRiadok;
            }
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
                konv.ZapisDoSuboru("Zadanie_03_Vystup.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Vstupný súbor nenájdený!\n");
            }
            catch (IOException)
            {
                Console.WriteLine("Chyba pri práci so súborom!\n");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Prístup pre zápis do súboru je zamietnutý!\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Nastala neočakávaná chyba!");
            }
            finally
            {
                Console.WriteLine("Pre ukončenie programu stlačte ľubovoľnú klávesu.");
                Console.ReadKey();
            }
        }
    }
}