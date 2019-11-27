using UnityEngine;
using System.Collections;

public class SequentialSearchST<Key, Value> : MonoBehaviour {


	void Start () {
        //SequentialSearchST<String, Integer> st = new SequentialSearchST<String, Integer>();
        //for (int i = 0; !StdIn.isEmpty(); i++)
        //{
        //    String key = StdIn.readString();
        //    st.put(key, i);
        //}
        //for (String s : st.keys())
        //    StdOut.println(s + " " + st.get(s));
    }

    private int n;           // number of key-value pairs
    private Node first;      // the linked list of key-value pairs

    // a helper linked list data type
    private class Node
    {
        public Key key;
        public Value val;
        public Node next;

        public Node(Key key, Value val, Node next)
        {
            this.key = key;
            this.val = val;
            this.next = next;
        }
    }

  

    public SequentialSearchST()
    {
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
        for (Node x = first; x != null; x = x.next)
        {
            if (key.Equals(x.key))
                return x.val;
        }
        return (Value)(object)null;
    }

   

    public void put(Key key, Value val)
    {
        if (key == null) throw new System.Exception("first argument to put() is null");
        if (val == null)
        {
            delete(key);
            return;
        }

        for (Node x = first; x != null; x = x.next)
        {
            if (key.Equals(x.key))
            {
                x.val = val;
                return;
            }
        }
        first = new Node(key, val, first);
        n++;
    }

   

    public void delete(Key key)
    {
        if (key == null) throw new System.Exception("argument to delete() is null");
        first = delete(first, key);
    }


    private Node delete(Node x, Key key)
    {
        if (x == null) return null;
        if (key.Equals(x.key))
        {
            n--;
            return x.next;
        }
        x.next = delete(x.next, key);
        return x;
    }

}
