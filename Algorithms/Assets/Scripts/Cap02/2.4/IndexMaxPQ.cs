using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IndexMaxPQ<Key> : SortBase, IEnumerable
{

	// Use this for initialization
	void Start () {
        // insert a bunch of strings
        string[] strings = { "it", "was", "the", "best", "of", "times", "it", "was", "the", "worst" };

        IndexMaxPQ<string> pq = new IndexMaxPQ<string>(strings.Length);
        for (int i = 0; i < strings.Length; i++)
        {
            pq.insert(i, strings[i]);
        }

        // print each key using the iterator
        foreach (string i in pq)
        {
            print(i + " " + strings[int.Parse(i)]);
        }


        // increase or decrease the key
        for (int i = 0; i < strings.Length; i++)
        {
            if (Random.Range(0.0f,1.0f) < 0.5)  // Random.uniform() 返回一个随机的范围在[0,1)之间的double类型的数
                pq.increaseKey(i, strings[i] + strings[i]);
            else
                pq.decreaseKey(i, strings[i].Substring(0, 1));
        }

        // delete and print each key
        while (!pq.isEmpty())
        {
            string key = pq.maxKey();
            int i = pq.delMax();
            print(i + " " + key);
        }

        // reinsert the same strings
        for (int i = 0; i < strings.Length; i++)
        {
            pq.insert(i, strings[i]);
        }

        // delete them in random order
        int[] perm = new int[strings.Length];
        for (int i = 0; i < strings.Length; i++)
            perm[i] = i;

        UnSort(perm);  // StdRandom.shuffle() 随机打乱指定的object型数组
        for (int i = 0; i < perm.Length; i++)
        {
            string key = pq.keyOf(perm[i]);
            pq.delete(perm[i]);
            print(perm[i] + " " + key);
        }

    }


    private int n;           // number of elements on PQ
    private int[] pq;        // binary heap using 1-based indexing
    private int[] qp;        // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
    private Key[] keys;      // keys[i] = priority of i
    private Comparer<Key> comparator;  // optional comparator




    public IndexMaxPQ(int maxN)
    {
        if (maxN < 0) throw new System.Exception("参数不合法！");
        n = 0;
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
        return qp[i] != -1;
    }


    public int size()
    {
        return n;
    }

   

    public void insert(int i, Key key)
    {
        if (contains(i)) throw new System.Exception("index is already in the priority queue");
        n++;
        qp[i] = n;
        pq[n] = i;
        keys[i] = key;
        swim(n);
    }

 

    public int maxIndex()
    {
        if (n == 0) throw new System.Exception("Priority queue underflow");
        return pq[1];
    }

   

    public Key maxKey()
    {
        if (n == 0) throw new System.Exception("Priority queue underflow");
        return keys[pq[1]];
    }

   

    public int delMax()
    {
        if (n == 0) throw new System.Exception("Priority queue underflow");
        int min = pq[1];
        exch(1, n--);
        sink(1);

      
        qp[min] = -1;        // delete
        keys[min] = (Key)(object)null;    // to help with garbage collection
        pq[n + 1] = -1;        // not needed
        return min;
    }

   

    public Key keyOf(int i)
    {
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        else return keys[i];
    }

   

    public void changeKey(int i, Key key)
    {
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        keys[i] = key;
        swim(qp[i]);
        sink(qp[i]);
    }

   

    public void change(int i, Key key)
    {
        changeKey(i, key);
    }


    public void increaseKey(int i, Key key)
    {
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        if (comparator.Compare( keys[i],key) >= 0)
            throw new System.Exception("Calling increaseKey() with given argument would not strictly increase the key");

        keys[i] = key;
        swim(qp[i]);
    }

  

    public void decreaseKey(int i, Key key)
    {
        if (!contains(i)) throw new System.Exception("index is not in the priority queue");
        if (comparator.Compare(keys[i], key) <= 0)
            throw new System.Exception("Calling decreaseKey() with given argument would not strictly decrease the key");

        keys[i] = key;
        sink(qp[i]);
    }

  

    public void delete(int i)
    {
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
    private bool less(int i, int j)
    {
        return comparator.Compare(keys[pq[i]], keys[pq[j]]) <0;
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
