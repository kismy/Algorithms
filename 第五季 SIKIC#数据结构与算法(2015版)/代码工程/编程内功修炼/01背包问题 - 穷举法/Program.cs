using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01背包问题___穷举法
{
    class Program
    {
        static void Main(string[] args)
        {
            int m;
            int[] w = { 0, 3, 4, 5 };
            int[] p = { 0, 4, 5, 6 };
            Console.WriteLine(Exhaustivity(10, w, p));
            Console.WriteLine(Exhaustivity(3, w, p));
            Console.WriteLine(Exhaustivity(4, w, p));
            Console.WriteLine(Exhaustivity(5, w, p));
            Console.WriteLine(Exhaustivity(7, w, p));
            
            Console.ReadKey();
        }

        public static int Exhaustivity(int m,int[] w,int[] p)
        {
            int i = w.Length-1;//物品的个数

            int maxPrice = 0;
            for (int j = 0; j < Math.Pow(2, m); j++)
            {
                //取得j 上某一个位的二进制值 
                int weightTotal = 0;
                int priceTotal = 0;
                for (int number = 1; number <= i; number++)
                {
                    int result = Get2(j, number);
                    if (result == 1)
                    {
                        weightTotal += w[number];
                        priceTotal += p[number];
                    }
                }
                if (weightTotal <= m &&priceTotal>maxPrice )
                {
                    maxPrice = priceTotal;
                }
            }
            return maxPrice;
        }
        //取得j上第number位上的二进制值，是1还是0
        public static int Get2(int j,int number)
        {
            int A = j;
            int B = (int)Math.Pow(2, number - 1);
            int result = A & B;
            if (result == 0)
                return 0;
            return 1;
        }
    }
}
