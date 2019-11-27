using UnityEngine;
using System.Collections;
using System;

public class SymbolDigraph : MonoBehaviour {

    public TextAsset txt;
    void Start()
    {
      
        //SymbolDigraph sg = new SymbolDigraph(txt, '/');
        //Digraph graph = sg.digraph();
        //int t = sg.index("Algorithms");
        //foreach (int v in graph.Adj(t))
        //{
        //    print("   " + sg.Name(v));
        //}
    }

    private ST<string, int> st;  // string -> index
    private string[] keys;           // index  -> string
    private Digraph graph;           // the underlying digraph


    public SymbolDigraph(TextAsset txt,char sparator)  //jobs.txt
    {

        st = new ST<string, int>();

        // First pass builds the index by reading strings to associate
        // distinct strings with an index
        string[] lines = txt.text.Split(new char[1] { '\n'}, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++) {

            string[] lineStrs = lines[i].Split(new char[1] { sparator }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in lineStrs)
            {
                if (!st.contains(str))
                    st.put(str, st.size());
            }
        }

        keys = new string[st.size()];
        foreach (string name in st.keys())
        {
            keys[st.GetValue(name)] = name;
        }

        // second pass builds the digraph by connecting first vertex on each
        // line to all others
        graph = new Digraph(st.size());
        
        foreach (string line in lines)
        {
            string[] lineStrs = line.Split(new char[1] { sparator }, StringSplitOptions.RemoveEmptyEntries);          
            int v = st.GetValue(lineStrs[0]);
            for (int i = 1; i < lineStrs.Length; i++)
            {               
                int w = st.GetValue(lineStrs[i]);
                graph.AddEdge(v, w);
            }
        }
    }

   
    public bool contains(String s)
    {
        return st.contains(s);
    }

   
    public int index(String s)
    {
        return st.GetValue(s);
    }

    
    public int indexOf(String s)
    {
        return st.GetValue(s);
    }

  
    public String Name(int v)
    {
        validateVertex(v);
        return keys[v];
    }

   
    public String nameOf(int v)
    {
        validateVertex(v);
        return keys[v];
    }

  
    public Digraph G()
    {
        return graph;
    }

   
    public Digraph digraph()
    {
        return graph;
    }

    
    private void validateVertex(int v)
    {
        int V = graph.V();
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

   
  
}
