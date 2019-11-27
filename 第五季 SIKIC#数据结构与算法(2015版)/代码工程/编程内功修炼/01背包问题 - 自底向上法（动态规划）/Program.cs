using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01背包问题___自底向上法_动态规划_
{
    class Program
    {
        static void Main(string[] args)
        {
            int m;
            int[] w = { 0, 3, 4, 5 };
            int[] p = { 0, 4, 5, 6 };

            Console.WriteLine(BottomUp(10, 3, w, p));
            Console.WriteLine(BottomUp(3, 3, w, p));
            Console.WriteLine(BottomUp(4, 3, w, p));
            Console.WriteLine(BottomUp(5, 3, w, p));
            Console.WriteLine(BottomUp(7, 3, w, p));

            Console.ReadKey();
        }
        public static int[,] result = new int[11, 4];
        //m背包容量，i为物品序号
        public static int BottomUp(int m,int i,int[] w,int[] p)
        {
            if (result[m, i] != 0) return result[m, i];
            for (int tempM = 1; tempM <= m ; tempM++)
            {
                for (int tempI = 1; tempI <= i; tempI++)
                {
                    if (result[tempM, tempI] != 0) continue; //容量为tempM的背包，放入tempI进背包的结果，已算出
                    if (w[tempI] > tempM) //tempI超过背包容量tempM,舍弃tempI
                    {
                        result[tempM, tempI] = result[tempM, tempI - 1];
                    }
                    else
                    {
                        int maxValue1 = result[tempM - w[tempI], tempI - 1] + p[tempI]; //放入
                        int maxValue2 = result[tempM, tempI - 1]; //不放入
                        if (maxValue1 > maxValue2)
                        {
                            result[tempM, tempI] = maxValue1;
                        }
                        else
                        {
                            result[tempM, tempI] = maxValue2;
                        }
                    }
                }
            }
            return result[m, i];
        }


    }
}
