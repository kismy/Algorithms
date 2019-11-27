using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class IndexMinPQ<Key> :MonoBehaviour, IEnumerable
{


	//void Start () {
 //       // insert a bunch of strings
 //       string[] strings = { "it", "was", "the", "best", "of", "times", "it", "was", "the", "worst" };

 //       IndexMinPQ<string> pq = new IndexMinPQ<string>(strings.Length);
 //       for (int i = 0; i < strings.Length; i++)
 //       {
 //           pq.insert(i, strings[i]);
 //       }

 //       // delete and print each key
 //       while (!pq.isEmpty())
 //       {
 //           int i = pq.delMin();
 //           print(i + " " + strings[i]);
 //       }

 //       // reinsert the same strings
 //       for (int i = 0; i < strings.Length; i++)
 //       {
 //           pq.insert(i, strings[i]);
 //       }

 //       // print each key using the iterator
 //       foreach (string i in pq)
 //       {
 //           print(i + " "+ strings[int.Parse(i)]);
 //       }
 //       while (!pq.isEmpty())
 //       {
 //           pq.delMin();
 //       }
 //   }

    private int maxN;        // maximum number of elements on PQ
    private int n;           // number of elements on PQ
    private int[] pq;        // binary heap using 1-based indexing
    private int[] qp;        // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
    private Key[] keys;      // keys[i] = priority of i
    private Comparer<Key> comparator;  // optional comparator



    public IndexMinPQ(int maxN)
    {
       
        if (maxN < 0) throw new System.Exception("参数不合法！");
        this.maxN = maxN;
        n = 0;
        //keys = (Key[])new Comparable[maxN + 1];    // make this of length maxN??
        keys = new Key[maxN + 1];    // make this of length maxN??
        pq = new int[maxN + 1];
        qp = new int[maxN + 1];                   // make this of length maxN??
        for (int i = 0; i <= maxN; i++)
            qp[i] = -1;
    }
    public IndexMinPQ(int maxN,Comparer<Key> comparator)
    {
        this.comparator = comparator;
        if (maxN < 0) throw new System.Exception("参数不合法！");
        this.maxN = maxN;
        n = 0;
        //keys = (Key[])new Comparable[maxN + 1];    // make this of length maxN??
        keys = new Key[maxN + 1];    // make this of length maxN??
        pq = new int[maxN + 1];
        qp = new int[maxN + 1];                   // make this of length maxN??
        for (int i = 0; i <= maxN; i++)
            qp[i] = -1;
    }

   

    public bool isEmpty()
    {
        return n == 0;
    }

  

    public bool contains(int i)
    {
        if (i < 0 || i >= maxN) throw new System.Exception("参数不合法！");
        return qp[i] != -1;
    }


    public int size()
    {
        return n;
    }

  

    public void insert(int i, Key key)
    {
        if (i < 0 || i >= maxN)  throw new System.Exception("参数不合法！");
        if (contains(i)) throw new System.Exception("index is already in the priority queue");
        n++;
        qp[i] = n;
        pq[n] = i;
        keys[i] = key;
        swim(n);
    }

  

    public int minIndex()
    {
        if (n == 0) throw new System.Exception("Priority queue underflow");
        return pq[1];
    }

 

    public Key minKey()
    {
        if (n == 0) throw new System.Exception("Priority queue underflow");
        return keys[pq[1]];
    }

  

    public int delMin()
    {
        if (n == 0) throw new System.Exception("Priority queue underflow");
        int min = pq[1];
        exch(1, n--);
        sink(1);
       
        
        qp[min] = -1;        // delete
        keys[min] = (Key)(object)double.NaN;   // to help with garbage collection
        pq[n + 1] = -1;        // not needed
        return min;
    }

  

    public Key keyOf(int i)
    {
        if (i < 0 || i >= maxN) throw new System.Exception("参数不合法！");
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        else return keys[i];
    }


    public void changeKey(int i, Key key)
    {
        if (i < 0 || i >= maxN) throw new System.Exception("参数不合法！");
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        keys[i] = key;
        swim(qp[i]);
        sink(qp[i]);
    }

 

    public void change(int i, Key key)
    {
        changeKey(i, key);
    }

   

    public void decreaseKey(int i, Key key)
    {
        if (i < 0 || i >= maxN) throw new System.Exception("参数不合法！");
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        if (comparator.Compare(keys[i], key) <= 0)
            throw new System.Exception("Calling decreaseKey() with given argument would not strictly decrease the key");
        keys[i] = key;
        swim(qp[i]);
    }

   

    public void increaseKey(int i, Key key)
    {
        if (i < 0 || i >= maxN) throw new System.Exception("参数不合法！");
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        if (comparator.Compare( keys[i],key) >= 0)
            throw new System.Exception("Calling increaseKey() with given argument would not strictly increase the key");
        keys[i] = key;
        sink(qp[i]);
    }

  

    public void delete(int i)
    {
        if (i < 0 || i >= maxN) throw new System.Exception();
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        int index = qp[i];
        exch(index, n--);
        swim(index);
        sink(index);
        keys[i] = (Key)(object)null;
        qp[i] = -1;
    }


    /***************************************************************************
     * General helper functions.
     ***************************************************************************/
    private bool greater(int i, int j)
    {       
        return comparator.Compare(keys[pq[i]], keys[pq[j]]) > 0;
    }

    private void exch(int i, int j)
    {
        int swap = pq[i];
        pq[i] = pq[j];
        pq[j] = swap;
        qp[pq[i]] = i;
        qp[pq[j]] = j;
    }


    /***************************************************************************
     * Heap helper functions.
     ***************************************************************************/
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
