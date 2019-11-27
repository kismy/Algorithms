using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MaxPQ<Key> :MonoBehaviour, IEnumerable
{

    private Key[] pq;                    // store items at indices 1 to n
    private int n;                       // number of items on priority queue
    private Comparer<Key> comparator;  // optional comparator

   

    public MaxPQ(int initCapacity)
    {
        pq = new Key[initCapacity + 1];
        n = 0;
    }


    public MaxPQ()
    {
        //this(1);
        pq = new Key[1];
        n = 0;
    }

   

    public MaxPQ(int initCapacity, Comparer<Key> comparator)
    {
        this.comparator = comparator;
        pq = new Key[initCapacity + 1];
        n = 0;
    }

   

    public MaxPQ(Comparer<Key> comparator)
    {
        //this(1, comparator);
    }

   

    public MaxPQ(Key[] keys)
    {
        n = keys.Length;
        pq =new Key[keys.Length + 1];
        for (int i = 0; i < n; i++)
            pq[i + 1] = keys[i];
        for (int k = n / 2; k >= 1; k--)
            sink(k);
        //assert isMaxHeap();
    }




    public bool IsEmpty()
    {
        return n == 0;
    }

   

    public int size()
    {
        return n;
    }

    

    public Key max()
    {
        if (IsEmpty()) throw new Exception("Priority queue underflow");
        return pq[1];
    }


    private void resize(int capacity)
    {
        //assert capacity > n;
        Key[] temp =new Key[capacity];
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
        isMaxHeap();
    }

   

    public Key delMax()
    {
        if (IsEmpty()) throw new Exception("Priority queue underflow");
        Key max = pq[1];
        exch(1, n--);
        sink(1);
        pq[n + 1] = (Key)(object)null;     // to avoid loiterig and help with garbage collection
        if ((n > 0) && (n == (pq.Length - 1) / 4)) resize(pq.Length / 2);
        isMaxHeap();
        return max;
    }


    /***************************************************************************
     * Helper functions to restore the heap invariant.
     ***************************************************************************/

    private void swim(int k)
    {
        while (k > 1 && less(k / 2, k))
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
            if (j < n && less(j, j + 1)) j++;
            if (!less(k, j)) break;
            exch(k, j);
            k = j;
        }
    }

    /***************************************************************************
     * Helper functions for compares and swaps.
     ***************************************************************************/
    private bool less(int i, int j)
    {
        //if (comparator == null) return comparator.Compare( pq[i],pq[j]) < 0;
        //else  return comparator.Compare(pq[i], pq[j]) < 0;
        return comparator.Compare(pq[i], pq[j]) < 0;
    }

    private void exch(int i, int j)
    {
        Key swap = pq[i];
        pq[i] = pq[j];
        pq[j] = swap;
    }

    // is pq[1..N] a max heap?
    private bool isMaxHeap()
    {
        return isMaxHeap(1);
    }

    // is subtree of pq[1..n] rooted at k a max heap?
    private bool isMaxHeap(int k)
    {
        if (k > n) return true;
        int left = 2 * k;
        int right = 2 * k + 1;
        if (left <= n && less(k, left)) return false;
        if (right <= n && less(k, right)) return false;
        return isMaxHeap(left) && isMaxHeap(right);
    }


   




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

    // Use this for initialization
    void Start()
    {

    }

    public void maxpq()
    {
        MaxPQ<string> pq = new MaxPQ<string>();

        string item = "vonNeumann  2/12/1994  4732.35";
        if (!item.Equals("-")) pq.insert(item);
        else if (!pq.IsEmpty()) print(pq.delMax() + " ");
        print("(" + pq.size() + " left on pq)");
    }
}
