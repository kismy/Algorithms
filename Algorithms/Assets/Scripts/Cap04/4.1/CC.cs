using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CC : MonoBehaviour {
    public TextAsset txt;
	void Start () {
      
        Graph G = new Graph(txt);
        CC cc = new CC(G);

        // number of connected components
        int m = cc.Count();
        print(m + " components");

       List<Bag<int>>   components = cc.bagList;

        for (int i = 0; i < m; i++)
        {
            string str = null;
            foreach (int v in components[i])
            {
                str += (v + " ");              
            }
            print(str);
        }
    }

    private bool[] marked;   // marked[v] = has vertex v been marked?
    private int[] id;           // id[v] = id of connected component containing v
    private int[] size;         // size[id] = number of vertices in given component
    private int count;          // number of connected components
    public List<Bag<int>> bagList = new List<Bag<int>>();

    public CC(Graph G)
    {
        marked = new bool[G.V()];
        id = new int[G.V()];
        size = new int[G.V()];
        for (int v = 0; v < G.V(); v++)
        {
            if (!marked[v])
            {
                dfs(G, v);

                count++;
                bagList.Add(new Bag<int>());
            }

            bagList[count - 1].Add(v);
        }
    }

    public CC(EdgeWeightedGraph G)
    {
        marked = new bool[G.V()];
        id = new int[G.V()];
        size = new int[G.V()];
        for (int v = 0; v < G.V(); v++)
        {
            if (!marked[v])
            {
                dfs(G, v);
                count++;
            }
        }
    }

    // depth-first search for a Graph
    private void dfs(Graph G, int v)
    {
        marked[v] = true;
        id[v] = count;
        size[count]++;
        foreach (int w in G.Adj(v))
        {
            if (!marked[w])
            {
                dfs(G, w);
            }
        }
    }
    
    private void dfs(EdgeWeightedGraph G, int v)
    {
        marked[v] = true;
        id[v] = count;
        size[count]++;
        foreach (Edge e in G.Adj(v))
        {
            int w = e.other(v);
            if (!marked[w])
            {
                dfs(G, w);
            }
        }
    }



    public int ID(int v)
    {
        validateVertex(v);
        return id[v];
    }

   

    public int Size(int v)
    {
        validateVertex(v);
        return size[id[v]];
    }


    public int Count()
    {
        return count;
    }

    public bool connected(int v, int w)
    {
        validateVertex(v);
        validateVertex(w);
        return ID(v) == ID(w);
    }

    

    public bool areConnected(int v, int w)
    {
        validateVertex(v);
        validateVertex(w);
        return ID(v) == ID(w);
    }

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

    

}