using UnityEngine;
using System.Collections;
using System.Collections.Generic; //迭代器命名空间

public class Bag<Item> :  IEnumerable<Item>
{
   
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


    private Node<Item> first;     // top of stack
    private Node<Item> current;     // top of stack
    public static int n;                // size of the stack

    // helper linked list class
    private class Node<Item>
    {
        public Item item;
        public Node<Item> next;
    }

    /**
     * Initializes an empty stack.
     */
    public Bag()
    {
        
        first = null;
        n = 0;
    }


    public bool isEmpty()
    {
        return first == null;
    }


    public int size()
    {
        return n;
    }


    public void Add(Item item)
    {
        Node<Item> oldfirst = first;
        first = new Node<Item>();
        first.item = item;
        first.next = oldfirst;
        n++;
    }

    
    public Item peek()
    {
        if (isEmpty()) throw new System.Exception("Stack underflow");
        return first.item;
    }



    //public string toString()
    //{
    //    StringBuilder s = new StringBuilder();
    //    for (Item item:this)
    //    {
    //        s.Append(item);
    //        s.Append(' ');
    //    }
    //    return s.ToString();
    //}



    //链表迭代器
    //public Iterator<Item> iterator()
    //{
    //    return new ListIterator<Item>(first);

    //}
    //private class ListIterator<Item> : IEnumerable<Item>
    //{
    //    private Node<Item> current;

    //    public ListIterator(Node<Item> first)
    //    {
    //        current = first;
    //    }

    //    public bool hasNext()
    //    {
    //        return current != null;
    //    }

    //    public void remove()
    //    {
    //        throw new System.Exception("不支持此类操作");
    //    }

    //    public Item next()
    //    {
    //        if (!hasNext()) throw new System.Exception("该元素不存在");
    //        Item item = current.item;
    //        current = current.next;
    //        return item;
    //    }
    //}
}