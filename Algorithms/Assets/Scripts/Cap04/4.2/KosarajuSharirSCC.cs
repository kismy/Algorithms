using UnityEngine;
using System.Collections;

public class KosarajuSharirSCC : MonoBehaviour {


    public TextAsset txt;
	void Start () {
      
        Digraph G = new Digraph(txt);
        KosarajuSharirSCC scc = new KosarajuSharirSCC(G);

        // number of connected components
        int m = scc.Count();
       print(m + " strong components");

        // compute list of vertices in each strong component
        Queue<int>[] components = new Queue<int>[m];
        for (int i = 0; i < m; i++)
        {
            components[i] = new Queue<int>();
        }
        for (int v = 0; v < G.V(); v++)
        {
            components[scc.ID(v)].Enqueue(v);
        }

        // print results
        for (int i = 0; i < m; i++)
        {
            string str = null;
            foreach (int v in components[i])
            {
                str+=(v + " ");
            }
           print(str);
        }
    }
    private bool[] marked;     // marked[v] = has vertex v been visited?
    private int[] id;             // id[v] = id of strong component containing v
    private int count;            // number of strongly-connected components

    public KosarajuSharirSCC(Digraph G)
    {

        // compute reverse postorder of reverse graph
        DepthFirstOrder dfs = new DepthFirstOrder(G.Reverse());

        // run DFS on G, using reverse postorder to guide calculation
        marked = new bool[G.V()];
        id = new int[G.V()];
        foreach (int v in dfs.reversePost())
        {
            if (!marked[v])
            {
                DFS(G, v);
                count++;
            }
        }

      
    }

   
    private void DFS(Digraph G, int v)
    {
        marked[v] = true;
        id[v] = count;
        foreach (int w in G.Adj(v))
        {
            if (!marked[w]) DFS(G, w);
        }
    }

   
    public int Count()
    {
        return count;
    }

   
    public bool stronglyConnected(int v, int w)
    {
        validateVertex(v);
        validateVertex(w);
        return id[v] == id[w];
    }

  
    public int ID(int v)
    {
        validateVertex(v);
        return id[v];
    }

  
    private bool check(Digraph G)
    {
        TransitiveClosure tc = new TransitiveClosure(G);
        for (int v = 0; v < G.V(); v++)
        {
            for (int w = 0; w < G.V(); w++)
            {
                if (stronglyConnected(v, w) != (tc.reachable(v, w) && tc.reachable(w, v)))
                    return false;
            }
        }
        return true;
    }

  
    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

  
  
}
