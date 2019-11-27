using UnityEngine;
using System;
using System.Collections;

public class TEST5 : MonoBehaviour {

    public TextAsset txt;
    void Start() {
        //TrieSTStart();
        TSTStart();
    }

    void TrieSTStart()
    {
        string[] strs = txt.text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        // build symbol table from standard input
        TrieST<int> st = new TrieST<int>();
        for (int i = 0; i < strs.Length; i++)
        {
            string key = strs[i];
            st.put(key, i);
        }

        // print results
        if (st.size() < 100)
        {
            string str = "keys(\"\"):";
            foreach (string key in st.keys())
            {
                str+=("\t"+key + " " + st.get(key));
            }
            print(str);

        }

        print("longestPrefixOf(\"shellsort\"):");
        print(st.longestPrefixOf("shellsort"));

        print("longestPrefixOf(\"quicksort\"):");
        print(st.longestPrefixOf("quicksort"));

        print("keysWithPrefix(\"shor\"):");
        foreach (string s in st.keysWithPrefix("shor"))
            print(s);

        print("keysThatMatch(\".he.l.\"):");
        foreach (string s in st.keysThatMatch(".he.l."))
            print(s);
    }



    void TSTStart()
    {
        string[] strs = txt.text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        // build symbol table from standard input
        TST<int> st = new TST<int>();
        for (int i = 0;i< strs.Length; i++)
        {
            string key =strs[i];
            st.Put(key, i);
        }

        // print results
        if (st.size() < 100)
        {
            print("keys(\"\"):");
            foreach (string key in st.keys())
            {
                print(key + " " + st.Get(key));
            }

        }

        print("longestPrefixOf(\"shellsort\"):");
        print(st.longestPrefixOf("shellsort"));


        print("longestPrefixOf(\"shell\"):");
        print(st.longestPrefixOf("shell"));


        print("keysWithPrefix(\"shor\"):");
        foreach (string s in st.keysWithPrefix("shor"))
            print(s);


        print("keysThatMatch(\".he.l.\"):");
        foreach (string s in st.keysThatMatch(".he.l."))
            print(s);
    }
}
