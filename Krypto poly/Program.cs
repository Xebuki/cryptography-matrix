using System;

namespace Krypto_poly
{
    class Program
    {
        static void Main(string[] args)
        {
            //String temp;
            /*Console.WriteLine("Hello World!");*/


            Console.Write("Podaj p: ");
            String temp = Console.ReadLine();
            int p = Int32.Parse(temp);
            if (!czyPierwsza(p))
            {
                Console.Write("Wartosc p nie jest liczba pierwsza!!!");
                Environment.Exit(0);
            }
            //int p = 7;

            Console.Write("\nPodaj n: ");
            temp = Console.ReadLine();
            int n = Int32.Parse(temp);

            Console.Write("\nPodaj wymiar przestrzeni kodu: "); //k
            temp = Console.ReadLine();
            int k = Int32.Parse(temp);

            Console.Write("\nPodaj wymiar kodu: "); //m
            temp = Console.ReadLine();
            int m = Int32.Parse(temp);


            int[] plD = new int[n + 1];
            int[] Xrec = new int[k];
            

            Console.Write("\nPodaj wielomian plD: ");
            //Dodawanie plD przez uzytkownika
            //for (int i = n; i >= 0; i--)
            //{
            //    Console.Write(" plD[" + i + "] = ");
            //    temp = Console.ReadLine();
            //    plD[i] = Int32.Parse(temp);
            //}           
            for (int i = n; i >= 0; i--)
            {
                char t = Console.ReadKey().KeyChar;
                plD[i] = t - '0';
            }

            Console.Write("\nplD[ ");
            for (int i = n; i >= 0; i--)
            {
                Console.Write(plD[i] + " ");
            }
            Console.Write("]\n");

            int[] Xwej = new int[k];
            int[] Xkod = new int[k];
            int[] Syndrom = new int[k - m];


            Console.Write("Podaj X - Wejscie: ");
            for (int i = 0; i < m; i++)
            {
                Console.Write("\n[" + i + "] = ");
                temp = Console.ReadLine();
                int temp2 = Int32.Parse(temp);
                Xwej[i] = temp2;
            }


            Console.Write("\nX - Wejscie = [");
            for (int i = 0; i < m; i++)
            {
                Console.Write(" " + Xwej[i]);
            }
            Console.Write(" ]");


            

            if (m < k)
            {
                int[,] macierzO = new int[m, k - m];

                Console.WriteLine("\nLosowac liczby? (y/n)");
                char ran = Console.ReadKey().KeyChar;
                Boolean isRan;
                int[,] macierzG;
                switch (ran)
                {
                    case 'y': isRan = true; break;
                    case 'n': isRan = false; break;
                    default: isRan = false; break;
                }
                if (isRan == true)
                {
                    macierzG = ranMacierzG(k, m, p, n);
                    Console.WriteLine(isRan);
                    Console.WriteLine(ran);
                }
                else
                {
                    macierzG = usersMacierzG(k, m, p, n);
                    Console.WriteLine(isRan);
                    Console.WriteLine(ran);
                }


                //int[,] macierzG = ranMacierzG(k, m, p, n);

                int[,] macierz = Macierz(k, m, macierzG);
                int[,] macierzOdw = new int[m, k];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < k - m; j++)
                    {
                        macierzOdw[i, j] = Change(macierz[i, j], p);
                    }
                }
                int[,] macierzH = MacierzH(k, m, macierzOdw);


                Console.WriteLine("Macierz");
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < k - m; j++)
                    {
                        Console.Write(string.Format("{0, 3} ", macierz[i, j]));
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }

                Console.WriteLine("Macierz odwrotna");
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < k - m; j++)
                    {
                        Console.Write(string.Format("{0, 3} ", macierzOdw[i, j]));
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }

                Console.WriteLine("Macierz G");
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        Console.Write(string.Format("{0, 3} ", macierzG[i, j]));
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }

                Console.WriteLine("Macierz H");
                for (int i = 0; i < k - m; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        Console.Write(string.Format("{0, 3} ", macierzH[i, j]));
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }
                /* XKOD */
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        Xkod[i] = Add(Xkod[i], Prod(Xwej[j], macierzG[j, i], n, p, plD), p);
                    }
                }
                
                Console.Write("Xkod = [ ");
                for(int i = 0; i < Xkod.Length; i++)
                {
                    Console.Write(Xkod[i] + " ");
                    Xrec[i] = Xkod[i];
                }
                Console.Write("]");

                Console.Write("\nZmiana Xrec (y/n)");
                char c = Console.ReadKey().KeyChar;
                Boolean isXrec;
                switch (c)
                {
                    case 'y': isXrec = true; break;
                    case 'n': isXrec = false; break;
                    default: isXrec = false; break;
                }
                if (isXrec)
                {
                    Console.Write("\nPodaj ktory element ma zostac zmieniony: ");
                    String tempKod = Console.ReadLine();
                    int index = Int32.Parse(tempKod);
                    Console.Write("\nXrec[" + index + "]");
                    String tempValue = Console.ReadLine();
                    Xrec[index] = Int32.Parse(tempValue);
                }
                /* Generowanie syndromu */
                for (int i = 0; i < k - m; i++)
                {
                    for(int j = 0; j < k; j++)
                    {
                        Syndrom[i] = Add(Syndrom[i], Prod(Xrec[i], macierzH[i, j], n, p, plD), p);
                    }
                    
                }

                /* Wypisywanie syndromu */
                Console.Write("\nSyndrom: [ ");
                for (int i = 0; i < Syndrom.Length; i++)
                {
                    Console.Write(Syndrom[i] + " ");
                }
                Console.Write("]");

                /* yminR-C */
                int[] ymin = new int[k];
                Console.Write("\nyminR-C = [ ");
                for (int i = 0; i < k; i++)
                {
                    ymin[i] = SubAdd(Xrec[i], Xkod[i], p);
                    Console.Write(ymin[i] + " ");
                }
                Console.Write(" ]");

                /* ZDEKODOWANE SLOWO! */
                int[] xdec = new int[Xkod.Length];
                Console.Write("\nZdekodowane slowo = [ ");
                for (int i = 0; i < k; i++)
                {
                    xdec[i] = SubAdd(Xrec[i], ymin[i], p);
                    Console.Write(xdec[i] + " ");
                }
                Console.Write(" ]");
            }
            else
            {

            }

        }

        public static int[,] ranMacierzG(int k, int w, int p, int n)
        {

            Random r = new Random();
            int[,] macierzG = new int[w, k];
            Console.WriteLine("Wiersze: " + w + " Kolumny: " + k);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (i == j)
                    {
                        macierzG[i, j] = 1;
                    }
                    else
                    {
                        macierzG[i, j] = 0;
                    }
                }

            }
            //if (ran)
            //{
                for (int i = 0; i < w; i++)
                {
                    for (int j = w; j < k; j++)
                    {
                        double y = Math.Pow(p, n - 1);
                        int temp = Convert.ToInt32(y);
                        macierzG[i, j] = r.Next(0, temp);
                    }
                }
            //}
            //if (!ran)
            //{
            //    for (int i = w; i < k; i++)
            //    {
            //        for (int j = 0; j < w; j++)
            //        {
            //            Console.WriteLine("Podaj [" + j + ", " + i + "]");
            //            int temp = Console.Read();
            //            macierzG[j, i] = temp;
            //        }
            //    }
            //}

            return macierzG;

        }

        public static int[,] usersMacierzG(int k, int w, int p, int n)
        {
            int[,] macierzG = new int[w, k];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (i == j)
                    {
                        macierzG[i, j] = 1;
                    }
                    else
                    {
                        macierzG[i, j] = 0;
                    }
                }

            }
            for (int i = 0; i < w; i++)
            {
                for (int j = w; j < k; j++)
                {
                    Console.Write("Podaj [" + i + ", " + j + "] ");
                    String temp = Console.ReadLine();
                    macierzG[i, j] = Int32.Parse(temp);
                }
            }

            //dane testowe
            //macierzG[0, 4] = 3;
            //macierzG[0, 5] = 26;
            //macierzG[0, 6] = 8;
            //macierzG[0, 7] = 3;
            //macierzG[0, 8] = 8;
            //macierzG[1, 4] = 12;
            //macierzG[1, 5] = 73;
            //macierzG[1, 6] = 25;
            //macierzG[1, 7] = 2;
            //macierzG[1, 8] = 213;
            //macierzG[2, 4] = 28;
            //macierzG[2, 5] = 43;
            //macierzG[2, 6] = 29;
            //macierzG[2, 7] = 13;
            //macierzG[2, 8] = 91;
            //macierzG[3, 4] = 11;
            //macierzG[3, 5] = 47;
            //macierzG[3, 6] = 92;
            //macierzG[3, 7] = 34;
            //macierzG[3, 8] = 111;






            return macierzG;
        }

        public static int[,] Macierz(int k, int w, int[,] macierz)
        {
            int[,] result = new int[w, k - w];


            for (int i = w; i < k; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    result[j, i - w] = macierz[j, i];
                }
            }

            return result;
        }

        public static int[,] MacierzH(int k, int w, int[,] macierz)
        {
            int[,] macierzH = new int[k - w, k];

            for (int i = 0; i < k - w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    macierzH[i, j] = macierz[j, i];

                }
            }
            for(int i = 0; i < k - w; i++)
            {
                for(int j = w; j < k; j++)
                {
                    if (i == j-w)
                    {
                        macierzH[i, j] = 1;
                    }
                    else
                    {
                        macierzH[i, j] = 0;
                    }
                }
            }
            return macierzH;
        }

        public static int Change(int a, int p)
        {
            int aa = a, am = 0, pn = 1;
            while (aa != 0)
            {
                am = am + ((p - (aa % p)) % p) * pn;
                pn = pn * p;
                aa = aa / p;
            }
            return am;
        }

        public static int Prod(int a, int b, int n, int p, int[] plD)
        {
            int s, aa, bb, stpMn, k, i, j;
            int[] at = new int[n];
            int[] bt = new int[n];
            int[] ilw = new int[2 * n - 1];

            for (i = 0; i < n; i++)
            {
                at[i] = a % p;
                bt[i] = b % p;
                a = a / p;
                b = b / p;
            }

            s = 0;
            stpMn = 2 * n - 2;                   //     Mnożenie Wielomianów a*b=at[X]*bt[X]
            for (k = 2 * n - 2; k >= 0; k--)
            {
                ilw[k] = 0; 
                for (i = Math.Max(0, k - n + 1); i <= Math.Min(k, n - 1); i++) { 
                    ilw[k] = (ilw[k] + at[i] * bt[k - i]) % p; 
                }
                s = s * p + ilw[k]; 
                if (s == 0) { 
                    stpMn = k - 1; 
                }
            };

            aa = El_odw(plD[n], p);                   //     Reszta z dzielenia wielomianu  ilw    przez    plD
            for (k = stpMn; k >= n; k--)
            {
                j = k;
                bb = aa * ilw[k];
                for (i = n; i >= 0; i--)
                {
                    ilw[j] = (ilw[j] + (p - (bb * plD[i]) % p)) % p; j--;
                }
            }

            s = 0;
            for (i = n - 1; i >= 0; i--)
            {
                s = s * p + ilw[i];
            }

            return s;
        }

        public static int Add(int a, int b, int p)
        {
            int s = 0, pn = 1;
            while (a > 0 || b > 0)
            {
                s = s + (((a % p) + (b % p)) % p) * pn; pn = pn * p; a = a / p; b = b / p;
            }
            return s;
        }

        public static int El_odw(int a, int p)
        {
            int xa = 1, xb = 0, b = p, c, q;
            do
            {
                q = a / b; 
                c = a; 
                a = b; 
                b = c % b;
                c = xa;
                xa = xb; 
                xb = c - q * xb;
            } while (b > 0);

            xa = xa % p; 
            if (xa < 0) xa = xa + p; 
            return xa;
        }

        static bool czyPierwsza(int a)
        {
            if (a % 2 == 0)
                return (a == 2);
            for (int i = 3; i <= Math.Sqrt(a); i += 2)
            {
                if (a % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int SubAdd( int a,  int b, int p)
        { 
            int s = 0, pn = 1, bm; 
            while (a != 0|| b !=0) { 
                bm = (p - b % p) % p; s = s + (((a % p) + bm) % p) * pn; pn = pn * p; a = a / p; b = b / p; }; return s; }
    }
}
