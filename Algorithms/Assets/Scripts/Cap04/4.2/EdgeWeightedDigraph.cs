using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

public class EdgeWeightedDigraph : MonoBehaviour {

    public TextAsset txt;
	void Start () {
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(txt);
        print(G);
    }

    private static  string NEWLINE ="\n";

    private  int v;                // number of vertices in this digraph
    private int e;                      // number of edges in this digraph
    private Bag<DirectedEdge>[] adj;    // adj[v] = adjacency list for vertex v
    private int[] indegree;             // indegree[v] = indegree of vertex v

   
    public EdgeWeightedDigraph(int V)
    {
        if (V < 0) throw new System.Exception("Number of vertices in a Digraph must be nonnegative");
        this.v = V;
        this.e = 0;
        this.indegree = new int[V];
        adj = new Bag<DirectedEdge>[V];
        for (int v = 0; v < V; v++)
            adj[v] = new Bag<DirectedEdge>();
    }

   
    public EdgeWeightedDigraph(int V, int E)
    {
        //this(V);
        if (V < 0) throw new System.Exception("Number of vertices in a Digraph must be nonnegative");
        this.v = V;
        this.e = 0;
        this.indegree = new int[V];
        adj = new Bag<DirectedEdge>[V];
        for (int v = 0; v < V; v++)
            adj[v] = new Bag<DirectedEdge>();




        if (E < 0) throw new System.Exception("Number of edges in a Digraph must be nonnegative");
        for (int i = 0; i < E; i++)
        {
            int v =UnityEngine. Random.Range(0,V);
            int w = UnityEngine. Random.Range(0, V);
            double weight = 0.01 * UnityEngine. Random.Range(0, 100);
            DirectedEdge e = new DirectedEdge(v, w, weight);
            addEdge(e);
        }
    }


    public EdgeWeightedDigraph(TextAsset txt)
    {
        string[] lines = txt.text.Split('\n');

        //this(lines[0]);
        int V = int.Parse(lines[0]);
        if (V < 0) throw new System.Exception("Number of vertices in a Digraph must be nonnegative");
        this.v = V;
        this.e = 0;
        this.indegree = new int[V];
        adj = new Bag<DirectedEdge>[V];
        for (int v = 0; v < V; v++)
            adj[v] = new Bag<DirectedEdge>();





        int E = int.Parse(lines[1]);
        if (E < 0) throw new System.Exception("Number of edges must be nonnegative");
        for (int i = 0; i < E; i++)
        {
            string[] strs = lines[i + 2].Split(new char[1] { ' '},StringSplitOptions.RemoveEmptyEntries);
            int v = int.Parse(strs[0]);
            int w = int.Parse(strs[1]);
            validateVertex(v);
            validateVertex(w);
            double weight = double.Parse(strs[2]);
            addEdge(new DirectedEdge(v, w, weight));
        }
    }


    public EdgeWeightedDigraph(EdgeWeightedDigraph G)
    {

        //this(G.V());
        int V = G.V();
        if (V < 0) throw new System.Exception("Number of vertices in a Digraph must be nonnegative");
        this.v = V;
        this.e = 0;
        this.indegree = new int[V];
        adj = new Bag<DirectedEdge>[V];
        for (int v = 0; v < V; v++)
            adj[v] = new Bag<DirectedEdge>();



        this.e = G.E();
        for (int v = 0; v < G.V(); v++)
            this.indegree[v] = G.Indegree(v);
        for (int v = 0; v < G.V(); v++)
        {
            // reverse so that adjacency list is in same order as original
            Stack<DirectedEdge> reverse = new Stack<DirectedEdge>();
            foreach (DirectedEdge e in G.adj[v])
            {
                reverse.push(e);
            }
            foreach (DirectedEdge e in reverse)
            {
                adj[v].Add(e);
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

   
    public void addEdge(DirectedEdge e)
    {
        int v = e.from();
        int w = e.to();
        validateVertex(v);
        validateVertex(w);
        adj[v].Add(e);
        indegree[w]++;
        this.e++;
    }


    public Bag<DirectedEdge> Adj(int v)
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

   
    public Bag<DirectedEdge> edges()
    {
        Bag<DirectedEdge> list = new Bag<DirectedEdge>();
        for (int v = 0; v < this.v; v++)
        {
            foreach (DirectedEdge e in Adj(v))
            {
                list.Add(e);
            }
        }
        return list;
    }

    public string ToString()
    {
        StringBuilder s = new StringBuilder();
        s.Append(this.v + " " + this.e + NEWLINE);
        for (int v = 0; v < this.v; v++)
        {
            s.Append(v + ": ");
            foreach (DirectedEdge e in adj[v])
            {
                s.Append(e + "  ");
            }
            s.Append(NEWLINE);
        }
        return s.ToString();
    }
    
}
