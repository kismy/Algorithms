using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class TrieST<Value> {



    private  const int R = 256;        // extended ASCII


    private Node root;      // root of trie
    private int n;          // number of keys in trie

    // R-way trie node
    private  class Node
    {
        public object val;
        public Node[] next = new Node[R];
    }

    /**
      * Initializes an empty string symbol table.
      */
    public TrieST()
    {
    }


    /**
     * Returns the value associated with the given key.
     * @param key the key
     * @return the value associated with the given key if the key is in the symbol table
     *     and {@code null} if the key is not in the symbol table
     * @throws IllegalArgumentException if {@code key} is {@code null}
     */
    public Value get(string key)
    {
        if (key == null) throw new Exception("argument to get() is null");
        Node x = get(root, key, 0);
        if (x == null) return (Value)(object)null;
        return (Value)x.val;
    }

    /**
     * Does this symbol table contain the given key?
     * @param key the key
     * @return {@code true} if this symbol table contains {@code key} and
     *     {@code false} otherwise
     * @throws IllegalArgumentException if {@code key} is {@code null}
     */
    public bool contains(String key)
    {
        if (key == null) throw new System.Exception("argument to contains() is null");
        return get(key) != null;
    }

    private Node get(Node x, string key, int d)
    {
        if (x == null) return null;
        if (d == key.Length) return x;
        char c = key.ToCharArray()[d];
        return get(x.next[c], key, d + 1);
    }

    /**
     * Inserts the key-value pair into the symbol table, overwriting the old value
     * with the new value if the key is already in the symbol table.
     * If the value is {@code null}, this effectively deletes the key from the symbol table.
     * @param key the key
     * @param val the value
     * @throws IllegalArgumentException if {@code key} is {@code null}
     */
    public void put(String key, Value val)
    {
        if (key == null) throw new System.Exception("first argument to put() is null");
        if (val == null) delete(key);
        else root = put(root, key, val, 0);
    }

    private Node put(Node x, String key, Value val, int d)
    {
        if (x == null) x = new Node();
        if (d == key.Length)
        {
            if (x.val == null) n++;
            x.val = val;
            return x;
        }
        char c = key.ToCharArray()[d];
        x.next[c] = put(x.next[c], key, val, d + 1);
        return x;
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
     * Is this symbol table empty?
     * @return {@code true} if this symbol table is empty and {@code false} otherwise
     */
    public bool isEmpty()
    {
        return size() == 0;
    }

    /**
     * Returns all keys in the symbol table as an {@code Iterable}.
     * To iterate over all of the keys in the symbol table named {@code st},
     * use the foreach notation: {@code for (Key key : st.keys())}.
     * @return all keys in the symbol table as an {@code Iterable}
     */
    public Queue<string> keys()
    {
        return keysWithPrefix("");
    }

    /**
     * Returns all of the keys in the set that start with {@code prefix}.
     * @param prefix the prefix
     * @return all of the keys in the set that start with {@code prefix},
     *     as an iterable
     */
    public Queue<string> keysWithPrefix(String prefix)
    {
        Queue<string> results = new Queue<string>();
        Node x = get(root, prefix, 0);
        collect(x, new StringBuilder(prefix), results);
        return results;
    }

    private void collect(Node x, StringBuilder prefix, Queue<String> results)
    {
        if (x == null) return;
        if (x.val != null) results.Enqueue(prefix.ToString());
        for (char c = (char)0; c < R; c++)
        {
            prefix.Append(c);
            collect(x.next[c], prefix, results);
            prefix.Remove(prefix.Length - 1,1);
        }
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
        Queue<string> results = new Queue<string>();
        collect(root, new StringBuilder(), pattern, results);
        return results;
    }

    private void collect(Node x, StringBuilder prefix, string pattern, Queue<String> results)
    {
        if (x == null) return;
        int d = prefix.Length;
        if (d == pattern.Length && x.val != null)
            results.Enqueue(prefix.ToString());
        if (d == pattern.Length)
            return;
        char c = pattern.ToCharArray()[d];
        if (c == '.')
        {
            for (char ch = (char)0; ch < R; ch++)
            {
                prefix.Append(ch);
                collect(x.next[ch], prefix, pattern, results);
                prefix.Remove(prefix.Length - 1,1);
            }
        }
        else
        {
            prefix.Append(c);
            collect(x.next[c], prefix, pattern, results);
            prefix.Remove(prefix.Length - 1,1);
        }
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
        if (query == null) throw new System.Exception("argument to longestPrefixOf() is null");
        int length = longestPrefixOf(root, query, 0, -1);
        if (length == -1) return null;
        else return query.Substring(0, length);
    }

    // returns the length of the longest string key in the subtrie
    // rooted at x that is a prefix of the query string,
    // assuming the first d character match and we have already
    // found a prefix match of given length (-1 if no such match)
    private int longestPrefixOf(Node x, string query, int d, int length)
    {
        if (x == null) return length;
        if (x.val != null) length = d;
        if (d == query.Length) return length;
        char c = query.ToCharArray()[d];
        return longestPrefixOf(x.next[c], query, d + 1, length);
    }

    /**
     * Removes the key from the set if the key is present.
     * @param key the key
     * @throws IllegalArgumentException if {@code key} is {@code null}
     */
    public void delete(string key)
    {
        if (key == null) throw new System.Exception("argument to delete() is null");
        root = delete(root, key, 0);
    }

    private Node delete(Node x, string key, int d)
    {
        if (x == null) return null;
        if (d == key.Length)
        {
            if (x.val != null) n--;
            x.val = null;
        }
        else
        {
            char c = key.ToCharArray()[d];
            x.next[c] = delete(x.next[c], key, d + 1);
        }

        // remove subtrie rooted at x if it is completely empty
        if (x.val != null) return x;
        for (int c = 0; c < R; c++)
            if (x.next[c] != null)
                return x;
        return null;
    }


}
