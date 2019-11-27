using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BinarySearchST<Key, Value>  : MonoBehaviour {

    private Comparer<Key> comparator;  // optional comparator
    void Start () {
        //BinarySearchST<string, int> st = new BinarySearchST<string, int>();
        //for (int i = 0; !StdIn.isEmpty(); i++)
        //{
        //    string key = StdIn.readstring();
        //    st.put(key, i);
        //}
        //foreach (string s in st.keys())
        //    print(s + " " + st.get(s));
    }

    //private const static  int INIT_CAPACITY = 2;
    private const int INIT_CAPACITY = 2;
    public Key[] keys;
    private Value[] vals;
    private int n = 0;

  

    public BinarySearchST()
    {
       
        keys = new Key[INIT_CAPACITY];
        vals = new Value[INIT_CAPACITY];
    }

  

    public BinarySearchST(int capacity)
    {
        keys =new Key[capacity];
        vals =new Value[capacity];
    }

    // resize the underlying arrays
    private void resize(int capacity)
    {
       
        Key[] tempk = new Key[capacity];
        Value[] tempv = new Value[capacity];
        for (int i = 0; i < n; i++)
        {
            tempk[i] = keys[i];
            tempv[i] = vals[i];
        }
        vals = tempv;
        keys = tempk;
    }

  

    public int size()
    {
        return n;
    }

   

    public bool isEmpty()
    {
        return size() == 0;
    }



    public bool contains(Key key)
    {
        if (key == null) throw new System.Exception("argument to contains() is null");
        return get(key) != null;
    }

 

    public Value get(Key key)
    {
        if (key == null) throw new System.Exception("argument to get() is null");
        if (isEmpty()) return (Value)(object)null;
        int i = rank(key);
        if (i < n && comparator.Compare( keys[i],key) == 0) return vals[i];
        return (Value)(object)null;
    }

   

    public int rank(Key key)
    {
        if (key == null) throw new System.Exception("argument to rank() is null");

        int lo = 0, hi = n - 1;
        while (lo <= hi)
        {
            int mid = lo + (hi - lo) / 2;
            int cmp = comparator.Compare(key, keys[mid]);
            if (cmp < 0) hi = mid - 1;
            else if (cmp > 0) lo = mid + 1;
            else return mid;
        }
        return lo;
    }



    public void put(Key key, Value val)
    {
        if (key == null) throw new System.Exception("first argument to put() is null");

        if (val == null)
        {
            delete(key);
            return;
        }

        int i = rank(key);

        // key is already in table
        if (i < n && comparator.Compare(keys[i], key) == 0)
        {
            vals[i] = val;
            return;
        }

        // insert new key-value pair
        if (n == keys.Length) resize(2 * keys.Length);

        for (int j = n; j > i; j--)
        {
            keys[j] = keys[j - 1];
            vals[j] = vals[j - 1];
        }
        keys[i] = key;
        vals[i] = val;
        n++;

      

    }

  

    public void delete(Key key)
    {
        if (key == null) throw new System.Exception("argument to delete() is null");
        if (isEmpty()) return;

        // compute rank
        int i = rank(key);

        // key not in table
        if (i == n || comparator.Compare(keys[i], key) != 0)
        {
            return;
        }

        for (int j = i; j < n - 1; j++)
        {
            keys[j] = keys[j + 1];
            vals[j] = vals[j + 1];
        }

        n--;
        keys[n] = (Key)(object)null;  // to avoid loitering
        vals[n] = (Value)(object)null;

        // resize if 1/4 full
        if (n > 0 && n == keys.Length / 4) resize(keys.Length / 2);

    }

   

    public void deleteMin()
    {
        if (isEmpty()) throw new System.Exception("Symbol table underflow error");
        delete(min());
    }

  

    public void deleteMax()
    {
        if (isEmpty()) throw new System.Exception("Symbol table underflow error");
        delete(max());
    }


    /***************************************************************************
     *  Ordered symbol table methods.
     ***************************************************************************/


    public Key min()
    {
        if (isEmpty()) throw new System.Exception("called min() with empty symbol table");
        return keys[0];
    }

 

    public Key max()
    {
        if (isEmpty()) throw new System.Exception("called max() with empty symbol table");
        return keys[n - 1];
    }

  

    public Key select(int k)
    {
        if (k < 0 || k >= size())
        {
            throw new System.Exception("called select() with invalid argument: " + k);
        }
        return keys[k];
    }

  

    public Key floor(Key key)
    {
        if (key == null) throw new System.Exception("argument to floor() is null");
        int i = rank(key);
        if (i < n && comparator.Compare(keys[i], key) == 0) return keys[i];
        if (i == 0) return (Key)(object)null;
        else return keys[i - 1];
    }

   

    public Key ceiling(Key key)
    {
        if (key == null) throw new System.Exception("argument to ceiling() is null");
        int i = rank(key);
        if (i == n) return (Key)(object)null;
        else return keys[i];
    }

  

    public int size(Key lo, Key hi)
    {
        if (lo == null) throw new System.Exception("first argument to size() is null");
        if (hi == null) throw new System.Exception("second argument to size() is null");

        if (comparator.Compare(lo, hi)> 0) return 0;
        if (contains(hi)) return rank(hi) - rank(lo) + 1;
        else return rank(hi) - rank(lo);
    }

   



    /***************************************************************************
     *  Check internal invariants.
     ***************************************************************************/

    private bool check()
    {
        return isSorted() && rankCheck();
    }


    private bool isSorted()
    {
        for (int i = 1; i < size(); i++)
            if (comparator.Compare(keys[i],keys[i - 1]) < 0) return false;
        return true;
    }


    private bool rankCheck()
    {
        for (int i = 0; i < size(); i++)
            if (i != rank(select(i))) return false;
        for (int i = 0; i < size(); i++)
            if (comparator.Compare(keys[i],select(rank(keys[i]))) != 0) return false;
        return true;
    }


  

}
