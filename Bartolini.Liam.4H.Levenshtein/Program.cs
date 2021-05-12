using System;
using System.Collections.Generic;
using System.Threading;
using Pastel;
 
/*--PACCHETTO NuGet DA SCARICARE -Pastel--*/
namespace Bartolini.Liam._4H.Levenshtein
{
    class Program
    {
        static List<(string, int)> _distanzeParole = new();

        static void Main(string[] args)
        {
            Console.WriteLine("Distanza di Levenshtein, Bartolini Liam");

            List<string> parole = new List<string> { "ciao", "miao", "gatto", "pesce", "cane", "canetto", "palo"};
            Console.WriteLine("Parola da cercare: ");
            string parola = Console.ReadLine();

            // Aggiungo dentro la lista la parola - parola cercata, e la loro distanza 
            foreach (string s in parole)
                _distanzeParole.Add((s, DistanzaLevenshtein(s, parola)));
            
            // Cerco il minimo
            int min = _distanzeParole[0].Item2;
            int index = 0;
            for (int i = 1; i < _distanzeParole.Count - 1; i++)
                min = Minimo(min, _distanzeParole[i].Item2, _distanzeParole[i + 1].Item2);
            
            // Cerco la parola con la distanza minore, salvo l'indice
            for (int i = 0; i < _distanzeParole.Count; i++)
                if (_distanzeParole[i].Item2 == min)
                {
                    index = i;
                    break;
                }
            
            // output del messaggio
            Console.WriteLine($"Forse cercavi `{_distanzeParole[index].Item1.Pastel("#00FF0F0")}` con distanza {_distanzeParole[index].Item2}");
        }

        /// <summary>
        /// Calcola la distanza tra due parole
        /// </summary>
        /// <param name="s">prima stringa</param>
        /// <param name="t">seconda string</param>
        /// <returns>La distanza tra due parole</returns>
        static int DistanzaLevenshtein(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;

            // punto 2.1
            if (n == 0)
                return m;
            
            // punto 2.2
            if (m == 0)
                return n;

            // inizializzare la matrice m+1 righe, n+1 colonne
            int[,] distanze = new int[m + 1, n + 1];

            // punto 3.1
            for (int i = 0; i < n + 1; i++)
                distanze[0, i] = i;

            // punto 3.2
            for (int j = 0; j < m + 1; j++)
                distanze[j, 0] = j;

            // punto 4 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    // punto 4.1
                    // impostazione valore del 'costo' con l'operatore ternario
                    //int costo = (s[i] == t[j]) ? 0 : 1;
                    int costo;
                    if (t[j] == s[i])
                        costo = 0;
                    else
                        costo = 1;

                    // punto 4.2
                    distanze[j + 1, i + 1] = Minimo(distanze[j + 1, i] + 1, distanze[j, i + 1] + 1, distanze[j, i] + costo);
                    //StampaMatrice(distanze, j + 1, i + 1);
                    //Thread.Sleep(700);
                    //Console.Clear();
                }
            }
            // punto 5
            return distanze[m, n];
        }

        static void StampaMatrice(int[,] matrix, int x, int k)
        {
            string ret = "\n\n";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == x && j == k)
                        ret += $"{matrix[i, j]}".Pastel("#FF0000").PastelBg("#2096f7") + "    ";
                    else
                        ret += $"{matrix[i, j]}    ";
                }
                ret += "\n\n";
            }
            Console.WriteLine(ret);
        }

        static int Minimo(int a, int b, int c)
        {
            int ret = a;
            if (b < ret)
                ret = b;

            if (c < ret)
                ret = c;

            return ret;
        }
    }
}