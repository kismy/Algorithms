using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinPQ<Key> :IEnumerable
{


private Key[] pq;                    // store items at indices 1 to n
    private int n;                       // number of items on priority queue
    private Comparer<Key> comparator;  // optional comparator

  

    public MinPQ(int initCapacity)
    {
        pq = new Key[initCapacity + 1];
        n = 0;
    }


    public MinPQ()
    {
        pq = new Key[1];
        n = 0;
    }

   

    public MinPQ(int initCapacity, Comparer<Key> comparator)
    {
        this.comparator = comparator;
        pq = new Key[initCapacity + 1];
        n = 0;
    }

   

    public MinPQ(Comparer<Key> comparator)
    {
        //this(1, comparator);
        this.comparator = comparator;
        pq = new Key[1 + 1];
        n = 0;
    }

   

    public MinPQ(Key[] keys)
    {
        n = keys.Length;
        pq = new Key[keys.Length + 1];
        for (int i = 0; i < n; i++)
            pq[i + 1] = keys[i];
        for (int k = n / 2; k >= 1; k--)
            sink(k);
         isMinHeap();
    }

   
    public bool isEmpty()
    {
        return n == 0;
    }


    public int size()
    {
        return n;
    }

  

    public Key min()
    {
        if (isEmpty()) throw new System.Exception("Priority queue underflow");
        return pq[1];
    }

    private void resize(int capacity)
    {

        Key[] temp = new Key[capacity];
        for (int i = 1; i <= n; i++)
        {
            temp[i] = pq[i];
        }
        pq = temp;
    }

  

    public void insert(Key x)
    {
        // double size of array if necessary
        if (n == pq.Length - 1) resize(2 * pq.Length);

        // add x, and percolate it up to maintain heap invariant
        pq[++n] = x;
        swim(n);
        isMinHeap();
    }

  
    public Key delMin()
    {
        if (isEmpty()) throw new System.Exception("Priority queue underflow");
        Key min = pq[1];
        exch(1, n--);
        sink(1);
        pq[n + 1] = (Key)(object)null;     // to avoid loiterig and help with garbage collection
        if ((n > 0) && (n == (pq.Length - 1) / 4)) resize(pq.Length / 2);
        isMinHeap();
        return min;
    }


    private void swim(int k)
    {
        while (k > 1 && greater(k / 2, k))
        {
            exch(k, k / 2);
            k = k / 2;
        }
    }

    private void sink(int k)
    {
        while (2 * k <= n)
        {
            int j = 2 * k;
            if (j < n && greater(j, j + 1)) j++;
            if (!greater(k, j)) break;
            exch(k, j);
            k = j;
        }
    }

   
    private bool greater(int i, int j)
    {        
        return comparator.Compare(pq[i], pq[j]) > 0;
    }

    private void exch(int i, int j)
    {
        Key swap = pq[i];
        pq[i] = pq[j];
        pq[j] = swap;
    }

    private bool isMinHeap()
    {
        return isMinHeap(1);
    }

    
    private bool isMinHeap(int k)
    {
        if (k > n) return true;
        int left = 2 * k;
        int right = 2 * k + 1;
        if (left <= n && greater(k, left)) return false;
        if (right <= n && greater(k, right)) return false;
        return isMinHeap(left) && isMinHeap(right);
    }

    //public static void main(String[] args)
    //{
    //    MinPQ<String> pq = new MinPQ<String>();
    //    while (!StdIn.isEmpty())
    //    {
    //        String item = StdIn.readString();
    //        if (!item.equals("-")) pq.insert(item);
    //        else if (!pq.isEmpty()) StdOut.print(pq.delMin() + " ");
    //    }
    //    StdOut.println("(" + pq.size() + " left on pq)");
    //}

    #region 迭代器
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Key> GetEnumerator()  //todo
    {
        Node<Key> current = null;

        while (current != null)
        {
            yield return current.item;
            current = current.next;
        }
    }

    #endregion
}
