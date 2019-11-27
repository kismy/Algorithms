using UnityEngine;
using System.Collections;
using System.Text;

public class TST<Value>  {

   
    private int n;              // size
    private Node<Value> root;   // root of TST

    private  class Node<Value>
    {
        public char c;                        // character
        public Node<Value> left, mid, right;  // left, middle, and right subtries
        public Value val;                     // value associated with string
    }

    /**
     * Initializes an empty string symbol table.
     */
    public TST()
    {
    }

    /**
     * Returns the number of key-value pairs in this symbol table.
     * @return the number of key-value pairs in this symbol table
     */
    public int size()
    {
        return n;
    }

    /**
     * Does this symbol table contain the given key?
     * @param key the key
     * @return {@code true} if this symbol table contains {@code key} and
     *     {@code false} otherwise
     * @throws IllegalArgumentException if {@code key} is {@code null}
     */
    public bool contains(string key)
    {
        if (key == null)
        {
            throw new System.Exception("argument to contains() is null");
        }
        UnityEngine.MonoBehaviour.print(Get(key));
        return Get(key) != null;
    }

    /**
     * Returns the value associated with the given key.
     * @param key the key
     * @return the value associated with the given key if the key is in the symbol table
     *     and {@code null} if the key is not in the symbol table
     * @throws IllegalArgumentException if {@code key} is {@code null}
     */
    public Value Get(string key)
    {
        if (key == null)
        {
            throw new System.Exception("calls get() with null argument");
        }
        if (key.Length == 0) throw new System.Exception("key must have length >= 1");
        Node<Value> x = Get(root, key, 0);
        if (x == null) return (Value)(object)null;
        return x.val;
    }

    // return subtrie corresponding to given key
    private Node<Value> Get(Node<Value> x, string key, int d)
    {
        if (x == null)
        {
            UnityEngine.MonoBehaviour.print("NULL");
            return null;
        }
        if (key.Length == 0) throw new System.Exception("key must have length >= 1");
        char c = key[d];
        if (c < x.c)
        {
            UnityEngine.MonoBehaviour.print("c < x.c");
            return Get(x.left, key, d);
        }
        else if (c > x.c)
        {
            UnityEngine.MonoBehaviour.print("c > x.c");
            return Get(x.right, key, d);
        }
        else if (d < key.Length - 1) {
            UnityEngine.MonoBehaviour.print("d < key.Length - 1");
            return Get(x.mid, key, d + 1);
        }
        else return x;
    }

    /**
     * Inserts the key-value pair into the symbol table, overwriting the old value
     * with the new value if the key is already in the symbol table.
     * If the value is {@code null}, this effectively deletes the key from the symbol table.
     * @param key the key
     * @param val the value
     * @throws IllegalArgumentException if {@code key} is {@code null}
     */
    public void Put(string key, Value val)
    {
        if (key == null)
        {
            throw new System.Exception("calls put() with null key");
        }
        if (!contains(key)) n++;
        root = Put(root, key, val, 0);
    }

    private Node<Value> Put(Node<Value> x, string key, Value val, int d)
    {
        char c = key[d];
        if (x == null)
        {
            x = new Node<Value>();
            x.c = c;
        }
        if (c < x.c) x.left = Put(x.left, key, val, d);
        else if (c > x.c) x.right = Put(x.right, key, val, d);
        else if (d < key.Length - 1) x.mid = Put(x.mid, key, val, d + 1);
        else x.val = val;
        return x;
    }

    /**
     * Returns the string in the symbol table that is the longest prefix of {@code query},
     * or {@code null}, if no such string.
     * @param query the query string
     * @return the string in the symbol table that is the longest prefix of {@code query},
     *     or {@code null} if no such string
     * @throws IllegalArgumentException if {@code query} is {@code null}
     */
    public string longestPrefixOf(string query)
    {
        if (query == null)
        {
            throw new System.Exception("calls longestPrefixOf() with null argument");
        }
        if (query.Length == 0) return null;
        int length = 0;
        Node<Value> x = root;
        int i = 0;
        while (x != null && i < query.Length)
        {
            char c = query[i];
            if (c < x.c) x = x.left;
            else if (c > x.c) x = x.right;
            else
            {
                i++;
                if (x.val != null) length = i;
                x = x.mid;
            }
        }
        return query.Substring(0, length);
    }

    /**
     * Returns all keys in the symbol table as an {@code Iterable}.
     * To iterate over all of the keys in the symbol table named {@code st},
     * use the foreach notation: {@code for (Key key : st.keys())}.
     * @return all keys in the symbol table as an {@code Iterable}
     */
    public Queue<string> keys()
    {
        Queue<string> queue = new Queue<string>();
        Collect(root, new StringBuilder(), queue);
        return queue;
    }

    /**
     * Returns all of the keys in the set that start with {@code prefix}.
     * @param prefix the prefix
     * @return all of the keys in the set that start with {@code prefix},
     *     as an iterable
     * @throws IllegalArgumentException if {@code prefix} is {@code null}
     */
    public Queue<string> keysWithPrefix(string prefix)
    {
        if (prefix == null)
        {
            throw new System.Exception("calls keysWithPrefix() with null argument");
        }
        Queue<string> queue = new Queue<string>();
        Node<Value> x = Get(root, prefix, 0);
        if (x == null) return queue;
        if (x.val != null) queue.Enqueue(prefix);
        Collect(x.mid, new StringBuilder(prefix), queue);
        return queue;
    }

    // all keys in subtrie rooted at x with given prefix
    private void Collect(Node<Value> x, StringBuilder prefix, Queue<string> queue)
    {
        if (x == null) return;
        Collect(x.left, prefix, queue);
        if (x.val != null) queue.Enqueue(prefix.ToString() + x.c);
        Collect(x.mid, prefix.Append(x.c), queue);
        prefix.Remove(prefix.Length - 1,1);
        Collect(x.right, prefix, queue);
    }


    /**
     * Returns all of the keys in the symbol table that match {@code pattern},
     * where . symbol is treated as a wildcard character.
     * @param pattern the pattern
     * @return all of the keys in the symbol table that match {@code pattern},
     *     as an iterable, where . is treated as a wildcard character.
     */
    public Queue<string> keysThatMatch(string pattern)
    {
        Queue<string> queue = new Queue<string>();
        Collect(root, new StringBuilder(), 0, pattern, queue);
        return queue;
    }

    private void Collect(Node<Value> x, StringBuilder prefix, int i, string pattern, Queue<string> queue)
    {
        if (x == null) return;
        char c = pattern[i];
        if (c == '.' || c < x.c) Collect(x.left, prefix, i, pattern, queue);
        if (c == '.' || c == x.c)
        {
            if (i == pattern.Length - 1 && x.val != null) queue.Enqueue(prefix.ToString() + x.c);
            if (i < pattern.Length - 1)
            {
                Collect(x.mid, prefix.Append(x.c), i + 1, pattern, queue);
                prefix.Remove(prefix.Length - 1,1);
            }
        }
        if (c == '.' || c > x.c) Collect(x.right, prefix, i, pattern, queue);
    }



}
