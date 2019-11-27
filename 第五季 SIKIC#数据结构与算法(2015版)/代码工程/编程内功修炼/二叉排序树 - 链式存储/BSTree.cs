using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二叉排序树___链式存储
{
    public enum TraversalType
    {
        Front,  //前序遍历
        Middle, //中序遍历
        Back    //后续遍历
    }
    class BSTree
    {

        BSNode root = null;

        //添加数据
        public void Add(int item)
        {
            BSNode newNode = new BSNode(item);
            if (root == null)  root = newNode;
            else
            {
                BSNode temp = root;
                while (true)
                {
                    if (item >= temp.Data)//放在temp的右边
                    {
                        if (temp.RightChild == null)
                        {
                            temp.RightChild = newNode;
                            newNode.Parent = temp;
                            break;
                        }
                        else temp = temp.RightChild;
                    }
                    else//放在temp的左边
                    {
                        if (temp.LeftChild == null)
                        {
                            temp.LeftChild = newNode;
                            newNode.Parent = temp;
                            break;
                        }
                        else temp = temp.LeftChild;
                    }
                }
            }
        }

        public void Traversal(TraversalType type)
        {
            Traversal(type,root);
        }
        private void Traversal(TraversalType typ,BSNode node)
        {
            if (node == null) return;
            switch (typ)
            {
                case TraversalType.Front: //前序遍历 ：中—>左—>右
                    Console.Write(node.Data + " ");
                    Traversal(typ, node.LeftChild);                  
                    Traversal(typ, node.RightChild);
                    break;

                case TraversalType.Middle: //中序遍历：左—>中—>右
                    Traversal(typ,node.LeftChild);
                    Console.Write(node.Data + " ");
                    Traversal(typ,node.RightChild);
                    break;


                case TraversalType.Back:    //左—> 右—> 中
                    Traversal(typ, node.LeftChild);
                    Traversal(typ, node.RightChild);
                    Console.Write(node.Data + " ");
                    break;
                default:
                    break;
            }


        }
        public bool Find(int item)
        {
            //return Find(item, root);

            BSNode temp = root;
            while (true)
            {
                if (temp == null) return false;
                if (temp.Data == item) return true;
                if (item > temp.Data)
                    temp = temp.RightChild;
                else
                    temp = temp.LeftChild;
            }
        }


        private bool Find(int item,BSNode node)
        {
            if (node == null) return false;
            if (node.Data == item) return true;
            else
            {
                if (item > node.Data) return Find(item, node.RightChild);
                else return Find(item, node.LeftChild);
            }
        }

        public bool Delete(int item)
        {
            BSNode temp = root;
            while (true)
            {
                if (temp == null) return false;
                if (temp.Data == item)
                {
                    Delete(temp);
                    return true;
                }
                if (item > temp.Data) temp = temp.RightChild;
                else temp = temp.LeftChild;
            }
        }
        public void Delete(BSNode node)
        {
            if (node.LeftChild == null && node.RightChild == null)
            {
                if (node.Parent == null) root = null;
                else if (node.Parent.LeftChild == node) node.Parent.LeftChild = null;
                else if(node.Parent.RightChild==node) node.Parent.RightChild = null;
                return;
            }
            if (node.LeftChild == null && node.RightChild != null)
            {
                node.Data = node.RightChild.Data;
                node.RightChild = null;
                return;
            }
            if (node.LeftChild != null && node.RightChild == null)
            {
                node.Data = node.LeftChild.Data;
                node.LeftChild = null;
                return;
            }

            BSNode temp = node.RightChild;
            while (true)
            {
                if (temp.LeftChild != null) temp = temp.LeftChild;
                else break;
            }
            node.Data = temp.Data;
            Delete(temp);
        }
    }
}