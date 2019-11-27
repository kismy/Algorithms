using UnityEngine;
using System.Collections;
using System.Collections.Generic; //迭代器命名空间


public class Node<Item>  
{
    public Item item;
    public Node<Item> next;
    public Node<Item> prior;
    public Node(Item data)
    {
        item =data;
    }
    public Node()
    {     

    }
}



public class Stack<Item> :  IEnumerable<Item>
{

#region 迭代器
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Item> GetEnumerator()
    {
        Node<Item> current = first;

        while (current != null)
        {
            yield return current.item;
            current = current.next;
        }
    }

# endregion


    public Node<Item> first;     // top of stack
    private Node<Item> current;     // top of stack
    public static int n;                // size of the stack

   

  
    public Stack()
    {        
        first = null;
        n = 0;
    }


    /// <summary>
    /// Queue<Item> r=new Queue<Item>(q);
    /// r是q的独立副本，对r,q操作（入列出列）二者不会产生影响
    /// </summary>
    /// <param name="q"></param>
    public Stack(Stack<Item> q)
    {
        Node<Item> current = q.first;

        while (current != null)
        {
            this.push(current.item);
            current = current.next;
        }
    }


    public bool isEmpty()
    {
        return first == null;
    }


    public int size()
    {
        return n;
    }

    //顶部添加元素
    public void push(Item item)
    {
        Node<Item> oldfirst = first;
        first = new Node<Item>();
        first.item = item;
        first.next = oldfirst;
        n++;
    }

    //删除顶部元素
    public Item pop()
    {
        if (isEmpty()) throw new System.Exception("Stack underflow");
        Item item = first.item;        // save item to return
        first = first.next;            // delete first node
        n--;
        return item;                   // return the saved item
    }


    public Item peek()
    {
        if (isEmpty()) throw new System.Exception("Stack underflow");
        return first.item;
    }
    public Item RemoveNthFromEnd(int n)
    {
        Item item = first.item;

        //求出链表长度  
        int len = 0;//链表长度  
        Node<Item> p, q;
        p = first;
        while (p != null)
        {
            len++;
            p = p.next;
        }
        if (n > len)
            throw new System.Exception("该链表仅有" + len + "个元素，倒数第" + n + "个元素不存在！");

        p = first;
        q = first;
        for (int i = 0; i < len - n; i++)
        {
            p = q;
            q = q.next;  //p是q的前驱，q为即将被删掉的元素
        }
        //当删除的是第一个节点的时候 。 即i == len - n==0时
        if (p == q) first = p.next;


        if (q != null)
        {
            item = q.item;  //q为被删掉的元素

            p.next = q.next;//q被删除, p是q的后一个元素的前驱 ||  原理：修改地址p.next的指针
            q.next = null;  //？？？？
        }

        return item;

    }
    public Item RemoveNthFromStart(int n)
    {
        Item item = first.item;

        //求出链表长度  
        int len = 0;//链表长度  
        Node<Item> p, q;
        p = first;
        while (p != null)
        {
            len++;
            p = p.next;
        }
        if (n > len)
            throw new System.Exception("该链表仅有" + len + "个元素，第" + n + "个元素不存在！");

        p = first;
        q = first;
        for (int i = 0; i < n - 1; i++)
        {
            p = q;
            item = p.item;

            q = q.next;  //p是q的前驱  //q为即将被删掉的元素
        }

        p.next = q.next;

        return item;

    }

    public static Stack<Item> Copy(Stack<Item> stack)
    {
        Stack<Item> newStack = new Stack<Item>();
        foreach (Item item in stack)
        {
            newStack.push(item);
        }

        return newStack;
    }

    public bool Find(Item key)
    {
        bool isFind = false;
        Node<Item> current = first;

        while (current != null)
        {
            if ((object)current.item == (object)key) isFind = true;
            current = current.next;
        }
        return isFind;
    }

    /// <summary>
    /// 在链表value==key结点后面插入Node<Item> t
    /// </summary>
    /// <param name="t"></param>
    /// <param name="key"></param>
    public void Insert(Item key,Item insertKey)
    {
        Node<Item> t = new Node<Item>();
        t.item = insertKey;
        Node<Item> current = first;

        while (current != null)
        {
            if ((object)current.item == (object)key)
            {
                t.next = current.next;
                current.next = t;
            }
            current = current.next;
        }
    }


    /// <summary>
    /// 在node结点后面插入insertNode结点
    /// </summary>
    /// <param name="node"></param>
    /// <param name="insertNode"></param>
    public void InsertAfter(Node<Item> node, Node<Item> insertNode)
    {
        if (node == null || insertNode == null) return;
        //Node<Item> tmp = node.next;
        //node.next =insertNode;
        //insertNode.next = tmp;
        insertNode.next = node.next;
        node.next = insertNode;
    }

    /// <summary>
    /// 删除node结点的后续结点
    /// </summary>
    /// <param name="node"></param>
    public void RemoveAfter(Node<Item> node)
    {
        if (node == null || node.next == null) return;
        node.next = node.next.next;
    }
    public void RemoveKey(Item key)
    {

        if (first == null) return;

        //仅有first一个元素
        if (first.next == null)
        {
            if ((object)first.item == (object)key) first = null;
            else return;
        }



        Node<Item> current = first;
        while (current.next.next != null)//执行到倒数第三个元素
        {
            if ((object)current.next.item == (object)key) //跳过第一个元素不检查
            {
                current.next = current.next.next;
                continue;
            }
            current = current.next;
        }
        //至此，current是倒数第二个元素，且current.item !=key

        if ((object)current.next.item == (object)key) current.next = null;
        if ((object)first.item == (object)key) first = first.next;  //while 循环跳过第一个元素不检查


    }
    public int Max()
    {
        Node<Item> current = first;
        int max = (int)(object)current.item;

        if (current != null)
        {
            if ((int)(object)current.item > max)
            {
                max = (int)(object)current.item;
            }

            current = current.next;
        }
        return max;
    }

    public int RecursiveMax()
    {
        Node<Item> current = first;
        int max = (int)(object)current.item;

        while (current != null)
        {
            if ((int)(object)current.item > max)
            {
                max = (int)(object)current.item;
            }

            current = current.next;
        }
        return max;

    }
    public static Node<Item> Reverse(ref Stack<Item> stack)
    {
        Queue<Item> queue = new Queue<Item>();

        Node<Item> current = stack.first;
        while (current != null)
        {
            queue.Enqueue(current.item);
            current = current.next;
        }


        Stack<Item> newStack = new Stack<Item>();
        Node<Item> queueCurrentNode = queue.first;
        while (queueCurrentNode != null)
        {
            newStack.push(queueCurrentNode.item);
            queueCurrentNode = queueCurrentNode.next;
        }
        stack = newStack;

        return stack.first;
    }

}