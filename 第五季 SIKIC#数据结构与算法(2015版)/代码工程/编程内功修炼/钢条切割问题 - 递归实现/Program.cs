using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 钢条切割问题___递归实现
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;//我们要切割售卖的钢条的长度
            int[] p = { 0, 1, 5, 8, 9, 10, 17, 17, 20, 24, 30 };//索引代表 钢条的长度，值代表价格
            Console.WriteLine(UpDown(0, p));
            Console.WriteLine(UpDown(1, p));
            Console.WriteLine(UpDown(2, p));
            Console.WriteLine(UpDown(3, p));
            Console.WriteLine(UpDown(4, p));
            Console.WriteLine(UpDown(5, p));
            Console.WriteLine(UpDown(6, p));
            Console.WriteLine(UpDown(7, p));
            Console.WriteLine(UpDown(8, p));
            Console.WriteLine(UpDown(9, p));
            Console.WriteLine(UpDown(10, p));

            //Console.WriteLine(UpDown(11, p));
            Console.WriteLine(UpDown(20, p));  //不可求解，因为11~20的价格没有,数组越界
            Console.ReadKey();
        }

        public static int UpDown(int n,int[] p)//求得长度为n的最大收益
        {
            if (n > p.Length)
            {
                Console.WriteLine("钢条长度不可大于价格表长度，因为大于数组长度的钢条，单价没有, 数组越界");
                return -1;
            }
            if (n == 1) return p[1];
            int tempMaxPrice = 0;
            for (int i = 1; i < n + 1; i++)
            {

                int maxPrice = p[i] + UpDown(n - i, p);
                if (maxPrice > tempMaxPrice)
                {
                    tempMaxPrice = maxPrice;
                }
            }
            return tempMaxPrice;
        }
    }
}
