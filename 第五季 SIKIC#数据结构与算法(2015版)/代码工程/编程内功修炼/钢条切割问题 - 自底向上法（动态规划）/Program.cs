using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 钢条切割问题___自底向上法_动态规划_
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] result = new int[11];//索引代表 钢条的长度，值代表最大收益
            int[] p = { 0, 1, 5, 8, 9, 10, 17, 17, 20, 24, 30 };//索引代表 钢条的长度，值代表该长度钢条的单价
            Console.WriteLine(BottomUp(0, p, result));
            Console.WriteLine(BottomUp(1, p, result));
            Console.WriteLine(BottomUp(2, p, result));
            Console.WriteLine(BottomUp(3, p, result));
            Console.WriteLine(BottomUp(4, p, result));
            Console.WriteLine(BottomUp(5, p, result));
            Console.WriteLine(BottomUp(6, p, result));
            Console.WriteLine(BottomUp(7, p, result));
            Console.WriteLine(BottomUp(8, p, result));
            Console.WriteLine(BottomUp(9, p, result));
            Console.WriteLine(BottomUp(10, p, result));

            Console.ReadKey();
        }

        public static int BottomUp(int n,int[] p,int[] result)
        {
            for (int i = 1; i <= n; i++) //i表示钢条长度
            {
                //下面取得 钢条长度为i的时候的最大收益
                int tempMaxPrice = -1;
                for (int j = 1; j <= i; j++) //result[1]到result[i-1]已经解出
                {
                    int imaxPrice = p[j] +  result[i - j];   //表示把长度为i的钢条分为两条，j和i-j
                    if (imaxPrice > tempMaxPrice)
                    {
                        tempMaxPrice = imaxPrice;
                    }
                }
                result[i] = tempMaxPrice;
            }
            return result[n];
        }
    }
}
