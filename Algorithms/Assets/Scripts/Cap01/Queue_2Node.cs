using UnityEngine;
using System.Collections;
using System.Collections.Generic; //迭代器命名空间



public class Queue_2Node<Item> :  IEnumerable<Item>
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

    #endregion

   
    public Node<Item> first;     // top of stack
    private Node<Item> last;
    private Node<Item> current;     // top of stack
    public static int size;                // size of the stack 

    public Queue_2Node()
    {
        
        first = null;
        size = 0;
    }

    /// <summary>
    /// Queue<Item> r=new Queue<Item>(q);
    /// r是q的独立副本，对r,q操作（入列出列）二者不会产生影响
    /// </summary>
    /// <param name="q"></param>
    public Queue_2Node(Queue_2Node<Item> q)
    {
        Node<Item> current = q.first;

        while (current != null)
        {
            this.Enqueue(current.item);
            current = current.next;
        }
    }

    public void Pointer()
    {
        Node<Item> test = first;
        test.next = null;
    }

    public bool isEmpty()
    {
        return first == null;
    }

    public int Size()
    {
        return size;
    }

    /// <summary>
    /// 在末尾添加一个元素
    /// </summary>
    /// <param name="item"></param>
    public void Enqueue(Item item)
    {
        Node<Item> oldlast = last;

        last =new Node<Item>();
        last.item = item;
        last.prior = oldlast;
        last.next = null;



        if (isEmpty()) first = last;
        else
        oldlast.next = last;
        size++;
    }

    /// <summary>
    /// 删除链表首结点
    /// </summary>
    /// <returns></returns>
    public Item Dequeue()
    {
       
        Item item = first.item;        // save item to return
        first = first.next;            // delete first node
        if (isEmpty()) first = null;
        size--;
        return item;                   // return the saved item
    }

    public Item peek()
    {
        if (isEmpty()) throw new System.Exception("Stack underflow");
        return first.item;
    }

    //第K个元素
    public Item SortPeek(int SortK)
    {
        int K = 0;
        Item item =first.item;
        Node<Item> node = first;
        while (K <= SortK-1)
        {
            item = node.item;
            node=node.next;
            K++;
        }
        return item;
    }

    //倒数第K个元素
    public Item ReversePeek(int ReverseK)
    {
        int K = 0;
        Item item = first.item;
        Node<Item> node = first;
        while (K <= size - ReverseK)
        {
            item = node.item;
            node = node.next;
            K++;
        }
        return item;
    }

    /// <summary>
    /// ？？？，为什么没有修改first的结点，而是修改first的副本p,q,就能修改first ?????  是不是修改了p,q地址的指针，就能修改first的地址的指针？  对的，具体可以参考Pointer()函数；
    /// 在单向链表中删掉/读取末尾元素需要从头遍历到末尾，比较费时，可以尝试双向链表
    /// </summary>
    /// <returns></returns>
    public Item RemoveNthFromEnd(int n)
    {
        Item item=first.item;
        
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
            throw new System.Exception("该链表仅有"+len+"个元素，倒数第"+n+"个元素不存在！");

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
        for (int i = 0; i < n-1; i++)
        {
            p = q; 
            item = p.item;

            q = q.next;  //p是q的前驱  //q为即将被删掉的元素
        }

        p.next = q.next;      

        return item;

    }

    public bool Find(Item key)
    {
        bool isFind = false;
        Node<Item> current = first;

        while (current != null)
        {
            if ((object)current.item == (object)key)
            {
                isFind = true;
                return true;
            }
            current = current.next;
        }
        return isFind;
    }

    public void Insert(Item key, Item insertKey)
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
    public void InsertInHead(Item key)
    {


        Node<Item> newHead = new Node<Item>(key);
        newHead.next = first;
        Node<Item> oldFirst = first;      
        newHead.prior = null;
        
        first = newHead;
    }

    /// <summary>
    /// 在node结点后面插入insertNode结点
    /// </summary>
    /// <param name="node"></param>
    /// <param name="insertNode"></param>
    public void InsertAfter(Node<Item> node, Node<Item> insertNode) {
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
               else  return;
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
            if ((int)(object)current.item >max)
            {
                max = (int)(object)current.item;
            }

            current = current.next;
        }
        return max;
    }

    public static Node<Item> Reverse( ref Queue_2Node<Item>  queue)
    {
        Stack<Item> stack = new Stack<Item>();

        Node<Item> current = queue.first;  
        while (current != null)
        {
            stack.push(current.item);
            current = current.next;
        }


        Queue_2Node<Item> newQueue = new Queue_2Node<Item>();
        Node <Item> stackCurrentNode= stack.first;
        while (stackCurrentNode != null)
        {
            newQueue.Enqueue(stackCurrentNode.item);
            stackCurrentNode = stackCurrentNode.next;
        }
        queue = newQueue;

        return queue.first;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void addFirst(Item data)
    {
        Node<Item> newNode = new Node<Item>(data);
        newNode.next = first.next;
        newNode.prior = first;
        first.next = newNode;
        newNode.next.prior = newNode;
        size++;
    }

    public void addLast(Item data)
    {
        Node<Item> newNode = new Node<Item>(data);
        newNode.next = first;
        newNode.prior = first.prior;
        first.prior = newNode;
        newNode.prior.next = newNode;
        size++;
    }

    public void removeFirst()
    {
        first.next = first.next.next;
        first.next.next.prior = first;
        size--;
    }

    public void removeLast()
    {
        first.prior = first.prior.prior;
        first.prior.prior.next = first;
        size--;
    }

    public void insertAfter(Item data, Node<Item> x)
    {
        Node<Item> newNode = new Node<Item>(data);
        newNode.next = x.next;
        newNode.prior = x;
        x.next = newNode;
        newNode.next.prior = newNode;
        size++;
    }

    public void insertBefore(Item data, Node<Item> x)
    {
        Node<Item> newNode = new Node<Item>(data);
        newNode.next = x;
        newNode.prior = x.prior;
        x.prior = newNode;
        newNode.prior.next = newNode;
        size++;
    }

    public void delete(Node<Item> x)
    {
        x.prior.next = x.next;
        x.next.prior = x.prior;
        size--;
    }

}