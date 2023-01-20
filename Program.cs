using System;
using System.IO;
using System.Linq;
using System.Text;

class Program
{

    //program porównuje dwa pliki csv jak są podobne wiersze w 50% wstawia je do nowego pliku 
    static void Main(string[] args)
    {
        string[] lines1 = File.ReadAllLines("spis1.csv");
        string[] lines2 = File.ReadAllLines("spis2.csv");
        string[] result = new string[lines1.Length];
        int index = 0;
        for (int i = 0; i < lines1.Length; i++)
        {
            for (int j = 0; j < lines2.Length; j++)
            {
                int distance = ComputeLevenshteinDistance(lines1[i], lines2[j]);
                double similarity = 1.0 - (double)distance / Math.Max(lines1[i].Length, lines2[j].Length);
                if (similarity >= 0.5)
                {
                    result[index] = lines1[i];
                    index++;
                }
            }
        }
        File.WriteAllLines("result.csv", result.Where(x => !string.IsNullOrWhiteSpace(x)));
    }

//Poniższy kod przedstawia przykład użycia algorytmu Levenshteina do porównywania dwóch ciągów i określenia ich podobieństwa na poziomie co najmniej 50%:
    public static int ComputeLevenshteinDistance(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        if (n == 0)
        {
            return m;
        }

        if (m == 0)
        {
            return n;
        }

        for (int i = 0; i <= n; d[i, 0] = i++)
        {
        }

        for (int j = 0; j <= m; d[0, j] = j++)
        {
        }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }
        return d[n, m];

        Console.WriteLine("is ok");
    }
}