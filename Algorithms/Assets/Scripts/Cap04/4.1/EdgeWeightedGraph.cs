using UnityEngine;
using System.Collections;
using System.Text;
using System;

public class EdgeWeightedGraph : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //In in = new In(args[0]);
        //EdgeWeightedGraph G = new EdgeWeightedGraph(in);
        //StdOut.println(G);
    }

    private static string NEWLINE = "\n";

    private int v;
    private int e;
    private Bag<Edge>[] adj;



    public EdgeWeightedGraph(int V)
    {
        if (V < 0) throw new System.Exception("Number of vertices must be nonnegative");
        this.v = V;
        this.e = 0;
        adj = new Bag<Edge>[V];
        for (int v = 0; v < V; v++)
        {
            adj[v] = new Bag<Edge>();
        }
    }



    public EdgeWeightedGraph(int V, int E)
    {
        //this(V);
        if (V < 0) throw new System.Exception("Number of vertices must be nonnegative");
        this.v = V;
        this.e = 0;
        adj = new Bag<Edge>[V];
        for (int v = 0; v < V; v++)
        {
            adj[v] = new Bag<Edge>();
        }

        if (E < 0) throw new System.Exception("Number of edges must be nonnegative");
        for (int i = 0; i < E; i++)
        {
            int v = UnityEngine.Random.Range(0, V);
            int w = UnityEngine.Random.Range(0, V);
            double weight = Mathf.Round(100 * UnityEngine.Random.Range(0, 1.0f)) / 100.0;
            Edge edge = new Edge(v, w, weight);
            addEdge(edge);
        }
    }



    public EdgeWeightedGraph(TextAsset txt, char sparator)
    {
        string[] lines = txt.text.Split('\n');

        int V = int.Parse(lines[0]);

        //this(V);
        if (V < 0) throw new System.Exception("Number of vertices must be nonnegative");
        this.v = V;
        this.e = 0;
        adj = new Bag<Edge>[V];
        for (int v = 0; v < V; v++)
        {
            adj[v] = new Bag<Edge>();
        }



        int E = int.Parse(lines[1]);
        if (E < 0) throw new System.Exception("Number of edges must be nonnegative");
        for (int i = 0; i < E; i++)
        {
            string[] linestrs = lines[i + 2].Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int v = int.Parse(linestrs[0]);
            int w = int.Parse(linestrs[1]);
            validateVertex(v);
            validateVertex(w);
            double weight = double.Parse(linestrs[2]);
            Edge edge = new Edge(v, w, weight);
            addEdge(edge);
           
        }
    }



    public EdgeWeightedGraph(EdgeWeightedGraph G)
    {
        //this(G.V());
        int V = G.V();
        if (V < 0) throw new System.Exception("Number of vertices must be nonnegative");
        this.v = V;
        this.e = 0;
        adj = new Bag<Edge>[V];
        for (int v = 0; v < V; v++)
        {
            adj[v] = new Bag<Edge>();
        }



        this.e = G.E();
        print(G.V());
        for (int v = 0; v < G.V(); v++)
        {
            
            // reverse so that adjacency list is in same order as original
            Stack<Edge> reverse = new Stack<Edge>();
            foreach (Edge e in G.adj[v])
            {
                reverse.push(e);
               
            }
            foreach (Edge e in reverse)
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

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertex(int v)
    {
        if (v < 0 || v >= this.v)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (this.v - 1));
    }



    public void addEdge(Edge e)
    {
        int v = e.either();
        int w = e.other(v);
        validateVertex(v);
        validateVertex(w);
        adj[v].Add(e);
        adj[w].Add(e);
        this.e++;
    }



    public Bag<Edge> Adj(int v)
    {
        validateVertex(v);
        return adj[v];
    }



    public int degree(int v)
    {
        validateVertex(v);
        return adj[v].size();
    }



    public Bag<Edge> edges()
    {
        Bag<Edge> list = new Bag<Edge>();
        for (int v = 0; v < this.v; v++)
        {
            int selfLoops = 0;
            foreach (Edge e in Adj(v))
            {
                if (e.other(v) > v)
                {
                    list.Add(e);
                }
                // only add one copy of each self loop (self loops will be consecutive)
                else if (e.other(v) == v)
                {
                    if (selfLoops % 2 == 0) list.Add(e);
                    selfLoops++;
                }
            }
        }
        return list;
    }



    public string toString()
    {
        StringBuilder s = new StringBuilder();
        s.Append(this.v + " " + this.e + NEWLINE);
        for (int v = 0; v < this.v; v++)
        {
            s.Append(v + ": ");
            foreach (Edge e in adj[v])
            {
                s.Append(e + "  ");
            }
            s.Append(NEWLINE);
        }
        return s.ToString();
    }
}


    

