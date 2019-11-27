using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二叉树___顺序结构存储
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] data = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };//这个是我们要存储的数据
            BiTree<char> tree = new BiTree<char>(10);
            for (int i = 0; i < data.Length; i++)
            {
                tree.Add(data[i]);
            }
            tree.FirstTraversal();
            Console.WriteLine();
            tree.MiddleTraversal();
            Console.WriteLine();
            tree.LastTraversal();
            Console.WriteLine();
            tree.LayerTraversal();
            Console.ReadKey();
        }
    }
}
