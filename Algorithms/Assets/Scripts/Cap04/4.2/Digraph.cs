using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System;

public class Digraph : MonoBehaviour {


	void Start () {

        Digraph G = new Digraph(txt);
        print(G.ToString());
    }
    private static  string NEWLINE ="\n";

    private  int v;           // number of vertices in this digraph
    private int e;                 // number of edges in this digraph
    private Bag<int>[] adj;    // adj[v] = adjacency list for vertex v
    private int[] indegree;        // indegree[v] = indegree of vertex v
    public TextAsset txt;
  


    public Digraph(int V)
    {
        if (V < 0) throw new System.Exception("Number of vertices in a Digraph must be nonnegative");
        this.v = V;
        this.e = 0;
        indegree = new int[V];
        adj =new Bag<int>[V];
        for (int v = 0; v < V; v++)
        {
            adj[v] = new Bag<int>();
        }
    }


    public Digraph(TextAsset txt)
    {
        try
        {
            string[] lines = txt.text.Split('\n');
            this.v = int.Parse(lines[0]);
            if (this.v < 0) throw new System.Exception("number of vertices in a Digraph must be nonnegative");
            indegree = new int[this.v];
            adj = new Bag<int>[this.v];
            for (int v = 0; v < this.v; v++)
            {
                adj[v] = new Bag<int>();
            }
            int E = int.Parse(lines[1]);
            if (E < 0) throw new System.Exception("number of edges in a Digraph must be nonnegative");
            for (int i = 0; i < E; i++)
            {
                string[] pairs = lines[i+2].Split( new char[3]{ ' ','-','|'},StringSplitOptions.RemoveEmptyEntries);
                int v = int.Parse(pairs[0]);
                int w = int.Parse(pairs[1]);
                AddEdge(v, w);
            }
        }
        catch (UnityException e)
        {
            throw new System.Exception("invalid input format in Digraph constructor", e);
        }
    }



    public Digraph(Digraph G)
    {
        //this(G.V());
        int vetexts=G.V();
        if (vetexts < 0) throw new System.Exception("Number of vertices in a Digraph must be nonnegative");
        this.v = vetexts;
        this.e = 0;
        indegree = new int[vetexts];
        adj = new Bag<int>[vetexts];
        for (int v = 0; v < vetexts; v++)
        {
            adj[v] = new Bag<int>();
        }


       //...............................

        this.e = G.E();
        for (int v = 0; v < this.v; v++)
            this.indegree[v] = G.Indegree(v);
        for (int v = 0; v < G.V(); v++)
        {
            // reverse so that adjacency list is in same order as original
            Stack<int> reverse = new Stack<int>();
            foreach (int w in G.adj[v])
            {
                reverse.push(w);
            }
            foreach (int w in reverse)
            {
                adj[v].Add(w);
            }
        }
    }

   

    public int V()
    {
        return v;
    }

  

    public int E()
    {
        return e;
    }



    private void validateVertex(int v)
    {
        if (v < 0 || v >= this.v)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (this.v - 1));
    }

  

    public void AddEdge(int v, int w)
    {
        validateVertex(v);
        validateVertex(w);
        adj[v].Add(w);
        indegree[w]++;
        this.e++;
    }

   

    public IEnumerable Adj(int v)
    {
        validateVertex(v);
        return adj[v];
    }

  

    public int outdegree(int v)
    {
        validateVertex(v);
        return adj[v].size();
    }

  

    public int Indegree(int v)
    {
        validateVertex(v);
        return indegree[v];
    }

  

    public Digraph Reverse()
    {
        Digraph reverse = new Digraph(this.v);
        for (int v = 0; v < this.v; v++)
        {
            foreach (int w in Adj(v))
            {
                reverse.AddEdge(w, v);
            }
        }
        return reverse;
    }

  

    public string ToString()
    {
        StringBuilder s = new StringBuilder();
        s.Append(this.v + " vertices, " + this.e + " edges " + NEWLINE);
        for (int v = 0; v < this.v; v++)
        {
            s.Append(v+": ");
            foreach (int w in adj[v])
            {
                s.Append(w+" ");
            }
            s.Append(NEWLINE);
        }
        return s.ToString();
    }

  
}
