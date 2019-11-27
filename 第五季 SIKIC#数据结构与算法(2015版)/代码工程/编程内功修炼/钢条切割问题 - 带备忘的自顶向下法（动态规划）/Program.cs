using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 钢条切割问题___带备忘的自顶向下法_动态规划_
{
    class Program
    {
        static void Main(string[] args)
        {
          
            int[] result = new int[11]; //索引代表 钢条的长度，值代表最大收益
            int[] p = { 0, 1, 5, 8, 9, 10, 17, 17, 20, 24, 30 };//索引代表 钢条的长度，值代表价格

            Console.WriteLine(UpDown(0, p, result));
            Console.WriteLine(UpDown(1, p, result));
            Console.WriteLine(UpDown(2, p, result));
            Console.WriteLine(UpDown(3, p, result));
            Console.WriteLine(UpDown(4, p, result));
            Console.WriteLine(UpDown(5, p, result));
            Console.WriteLine(UpDown(6, p, result));
            Console.WriteLine(UpDown(7, p, result));
            Console.WriteLine(UpDown(8, p, result));
            Console.WriteLine(UpDown(9, p, result));
            Console.WriteLine(UpDown(10, p, result));
            Console.ReadKey();
        }
        //带备忘的自顶向下法
        public static int UpDown(int n, int[] p,int[] result)//求得长度为n的最大收益
        {
            if (n == 0) return 0;
            if (result[n] != 0)
            {
                return result[n];
            }
            int tempMaxPrice = 0;
            for (int i = 1; i < n + 1; i++)
            {
                int maxPrice = p[i] + UpDown(n - i, p, result);
                if (maxPrice > tempMaxPrice)
                {
                    tempMaxPrice = maxPrice;
                }
            }
            result[n] = tempMaxPrice;
            return tempMaxPrice;
        }
    }
}
