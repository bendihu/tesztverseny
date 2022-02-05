namespace tesztverseny
{
    class Verseny
    {
        public string Versenyzo { get; set; }
        public string Valaszok { get; set; }
        public int Pontszam { get; set; }
    }

    public class Program
    {
        static List<Verseny> verseny = new List<Verseny>();
        static string megoldas = "";
        static string bAzonosito = "";
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //1. feladat
            Beolvas();

            //2. feladat
            Feladat2();

            //3. feladat
            Feladat3();

            //4. feladat
            Feladat4();

            //5. feladat
            Feladat5();

            //6. feladat
            Feladat6();

            //7. feladat
            Feladat7();

            Console.ReadLine();
        }
        private static void Beolvas()
        {
            Console.WriteLine("1. feladat: Adatok beolvasása");
            Console.WriteLine();

            StreamReader sr = new StreamReader(@"valaszok.txt");
             megoldas = sr.ReadLine();            

            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(' ');
                Verseny uj = new Verseny();
                uj.Versenyzo = line[0];
                uj.Valaszok = line[1];

                verseny.Add(uj);
            }

            sr.Close();
        }
        private static void Feladat2()
        {
            Console.WriteLine("2. feladat: A vetélkedőn {0} versenyző indult.", verseny.Count);
            Console.WriteLine();
        }
        private static void Feladat3()
        {
            Console.Write("3. feladat: A versenyző azonosítója = ");
            bAzonosito = Console.ReadLine().ToUpper();
            var szures = verseny.Where(x => x.Versenyzo.Equals(bAzonosito)).ToList();

            Console.WriteLine("{0}\t(a versenyző válasza)", szures.FirstOrDefault().Valaszok);
            Console.WriteLine();
        }
        private static void Feladat4()
        {
            Console.WriteLine("4. feladat:");

            var szures = verseny.Where(x => x.Versenyzo.Equals(bAzonosito)).ToList();
            string valasz = szures.FirstOrDefault().Valaszok;
            string helyes = "";

            for (int i = 0; i < megoldas.Length; i++)
            {
                if (megoldas.ElementAt(i) == valasz.ElementAt(i))
                {
                    helyes += "+";
                }
                else helyes += " ";
            }

            Console.WriteLine($"{megoldas}\t(a helyes megoldás)");
            Console.WriteLine($"{helyes}\t(a versenyző helyes válaszai) ");
            Console.WriteLine();
        }
        private static void Feladat5()
        {
            Console.Write("5. feladat: A feladat sorszáma = ");

            int sorszam = Convert.ToInt32(Console.ReadLine());
            int count = 0;

            foreach (var item in verseny)
            {
                if (item.Valaszok.ElementAt(sorszam - 1) == megoldas.ElementAt(sorszam - 1)) count++;
            }

            decimal szazalek = Math.Round((decimal)count / verseny.Count * 100, 2);

            Console.WriteLine($"A feladatra {count} fő, a versenyzők {szazalek}%-a adott helyes választ.");
            Console.WriteLine();
        }
        private static int Pont(int sorszam)
        {
            int pont = 0;
            switch (sorszam)
            {
                case <= 5:
                    pont = 3;
                    break;

                case int n when n > 5 && n <= 10:
                    pont = 4;
                    break;

                case int n when n > 10 && n <= 13:
                    pont = 5;
                    break;

                case 14:
                    pont = 6;
                    break;

                default:
                    break;
            }

            return pont;
        }
        private static void Feladat6()
        {
            Console.WriteLine("6. feladat: A versenyzők pontszámának meghatározása");

            StreamWriter sw = new StreamWriter(@"pontok.txt");
            int osszPont = 0;

            foreach (var item in verseny)
            {
                for (int i = 0; i < megoldas.Length; i++)
                {
                    if (megoldas.ElementAt(i) == item.Valaszok.ElementAt(i)) osszPont += Pont(i + 1);
                }

                sw.WriteLine($"{item.Versenyzo} {osszPont}");
                osszPont = 0;
            }

            sw.Close();
            Console.WriteLine();
        }
        private static void Feladat7()
        {
            Console.WriteLine("7. feladat: A verseny legjobbjai:");

            int osszPont = 0;
            List<Verseny> osszesitett = new List<Verseny>();

            foreach (var item in verseny)
            {
                for (int i = 0; i < megoldas.Length; i++)
                {
                    if (megoldas.ElementAt(i) == item.Valaszok.ElementAt(i)) osszPont += Pont(i + 1);
                }

                Verseny ossz = new Verseny();
                ossz.Versenyzo = item.Versenyzo;
                ossz.Pontszam = osszPont;
                
                osszesitett.Add(ossz);
                osszPont = 0;
            }

            var top = osszesitett.OrderByDescending(x => x.Pontszam).ToList();
            int helyezes = 1, elozo = 0;
           
            for (int i = 0; i < top.Count; i++)
            {
                if (elozo == 0 || elozo == top[i].Pontszam)
                {
                    Console.WriteLine($"{helyezes}.díj({top[i].Pontszam} pont): {top[i].Versenyzo}");
                    elozo = top[i].Pontszam;
                }
                else if (helyezes == 3 && elozo != top[i].Pontszam) break;
                else
                {
                    helyezes++;
                    Console.WriteLine($"{helyezes}.díj({top[i].Pontszam} pont): {top[i].Versenyzo}");
                    elozo = top[i].Pontszam;
                }
                
            }
        }
    }
}

