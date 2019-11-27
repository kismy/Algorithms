using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二叉排序树___链式存储
{
    class BSNode
    {
        public BSNode LeftChild { get; set; }
        public BSNode RightChild { get; set; }
        public BSNode Parent { get; set; }
        public int Data { get; set; }
        public BSNode()
        {
        }
        public BSNode(int item)
        {
            this.Data = item;
        }
    }
}
