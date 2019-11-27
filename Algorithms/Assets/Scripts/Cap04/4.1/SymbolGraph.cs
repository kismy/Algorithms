using System;
using UnityEngine;
using System.Collections;

public class SymbolGraph : MonoBehaviour {
	
    private ST<string, int> st;  // string -> index
    private string[] keys;           // index  -> string
    private Graph graph;             // the underlying graph
    public TextAsset txt;

    void Start()
    {
        //SymbolGraph sg = new SymbolGraph(txt);
        //Graph graph = sg.Graph();

        //string source = "JFK";  //输入一个顶点名并，用于获得该顶点相连顶点链表
        //if (sg.Contains(source))
        //{
        //    int s = sg.IndexOf(source);
        //    foreach (int v in graph.Adj(s))
        //    {
        //        print("   " + sg.NameOf(v));
        //    }          
        //}
        //else print("input not contain '" + source + "'");

        SymbolGraph sg = new SymbolGraph(txt.text,new char[] { '/'});
        Graph graph = sg.Graph();

        string source = "2 Days in the Valley (1996)";  //输入一个顶点名并，用于获得该顶点相连顶点链表
        if (sg.Contains(source))
        {
            int s = sg.IndexOf(source);
            foreach (int v in graph.Adj(s))
            {
                print("   " + sg.NameOf(v));
            }
        }
        else print("input not contain '" + source + "'");
    }


    public SymbolGraph(string filename, char[] delimiter)
    {
        st = new ST<string, int>();
        In input = new In(filename);
        while (input.hasNextLine()) {
            string[] a = input.readLine().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < a.Length; i++)
            {
                if (!st.contains(a[i]))
                {

                    st.put(a[i], st.size());
                }

            }
        }

            print("Done reading " + filename);

            keys = new string[st.size()];
            foreach (string name in st.keys())
            {
                keys[st.GetValue(name)] = name;
            }

            //second pass builds the graph by connecting first vertex on each
            //line to all others
           graph = new Graph(st.size());
            //print(st.size());
            input = new In(filename);
            while (input.hasNextLine()) {
                string[] a = input.readLine().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                int v = st.GetValue(a[0]);
                for (int i = 1; i < a.Length; i++)
                {
                    int w = st.GetValue(a[i]);
                    graph.AddEdge(v, w);
                }
            }
    }
   
    public SymbolGraph(TextAsset txtInput)
    {       
        st = new ST<string, int>(); 
        string[] lines = txtInput.text.Split('\n'); 
        for (int i = 0; i < lines.Length; i++)
        {
            string[] lineStrs = lines[i].Split(' ');
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

        graph = new Graph(st.size());
        foreach (string line in lines)
        {
            string[] lineStrs = line.Split(' ');
            int v = st.GetValue(lineStrs[0]);
            for (int i = 1; i < lineStrs.Length; i++)
            {
                int w = st.GetValue(lineStrs[i]);
                graph.AddEdge(v, w);
            }
        }
    }

    public bool Contains(string s)
    {
        return st.contains(s);
    }

    public int IndexOf(string s)
    {
        return st.GetValue(s);
    }

    public string NameOf(int v)
    {
        ValidateVertex(v);
        return keys[v];
    }

    public Graph Graph()
    {
        return graph;
    }

    private void ValidateVertex(int v)
    {
        int V = graph.V();
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    } 
}
