using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.Collections;

public class Graph:MonoBehaviour {

    public TextAsset txt;
    void Start()
    {
        Graph G = new Graph(txt);
       

    }
    private static  string NEWLINE = "\n";

    private  int v;
    private int e;
    private Bag<int>[] adj;
    
   

    public Graph(int V) {
        if (V < 0) throw new System.Exception("Number of vertices must be nonnegative");
        this.v = V;
        this.e = 0;
        adj =  new Bag<int>[V];
        for (int v = 0; v < V; v++) {
            adj[v] = new Bag<int>();
        }
    }

    //txt 第一行表示V：顶点数
    //txt 第2行表示E：边数
    //接下来是E条边的整数对，整数对中的值范围是0-到V-1 ，表示顶点值。
    public Graph(TextAsset txt)
    {
        try
        {
            string[] Lines = txt.text.Split('\n');

            this.v = int.Parse(Lines[0]);
            if (this.v < 0) throw new System.Exception("number of vertices in a Graph must be nonnegative");

            //初始化邻接表
            adj = new Bag<int>[this.v];
            for (int v = 0; v < this.v; v++)
            {
                adj[v] = new Bag<int>();
            }

            int E = int.Parse(Lines[1]);
            if (E < 0) throw new System.Exception("number of edges in a Graph must be nonnegative");

            //添加边
            for (int i = 0; i < E; i++)
            {
                string[] strs = Lines[i + 2].Split(' ');
                int v = int.Parse(strs[0]);
                int w = int.Parse(strs[1]);
                ValidateVertex(v);
                ValidateVertex(w);
                AddEdge(v, w);
            }
        }
        catch (System.Exception e)
        {
            throw new System.Exception("invalid input format in Graph constructor", e);
        }

        print(this.ToString());
    }

    public Graph(Graph G) {
        //this(G.V());
        this.v=G.V();
        this.e = G.E();
        for (int v = 0; v < G.V(); v++) {
            // reverse so that adjacency list is in same order as original
            Stack<int> reverse = new Stack<int>();
            foreach (int w in G.adj[v]) {
                reverse.push(w);
            }
            foreach (int w in reverse) {
                adj[v].Add(w);
            }
        }
    } 

    public int V() {
        return v;
    }   

    public int E() {
        return e;
    }

    private void ValidateVertex(int v) {
        if (v < 0 || v >=this.v)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (this.v-1));
    }  

    public void AddEdge(int v, int w) {
        ValidateVertex(v);
        ValidateVertex(w);
        e++;
        adj[v].Add(w);
        adj[w].Add(v);
    }

    public Bag<int> Adj(int v)
    { 
        ValidateVertex(v);
        return adj[v];
    }

    public int Degree(int v) {
        ValidateVertex(v);
        return adj[v].size();
    }

    public string ToString() {
        StringBuilder s = new StringBuilder();
        s.Append(this. v + " vertices, " +this. e + " edges " + NEWLINE);
        for (int v = 0; v < this.v; v++) {
            s.Append(v + ": ");
            foreach (int w in adj[v]) {
                s.Append(w + " ");
            }
            s.Append(NEWLINE);
        }
        return s.ToString();
    }


}