using UnityEngine;
using System.Collections;

public class DirectedDFS : MonoBehaviour {

    public TextAsset txt;
	void Start () {
       
        Digraph G = new Digraph(txt);

       
        Bag<int> sources = new Bag<int>();
        sources.Add(1);
        sources.Add(2);
        sources.Add(6);

        DirectedDFS dfs = new DirectedDFS(G, sources);

        string str = null;
        for (int v = 0; v < G.V(); v++)
        {
            if (dfs.Marked(v)) str+=(v + " --- ");
        }
        print(str);
    }

    private bool[] marked;  // marked[v] = true if v is reachable
                               // from source (or sources)
    private int count;         // number of vertices reachable from s

   
    public DirectedDFS(Digraph G, int s)
    {
        marked = new bool[G.V()];
        validateVertex(s);
        dfs(G, s);
    }

  
    public DirectedDFS(Digraph G, Bag<int> sources)
    {
        marked = new bool[G.V()];
        validateVertices(sources);
        foreach (int v in sources)
        {
            if (!marked[v]) dfs(G, v);
        }
    }

    private void dfs(Digraph G, int v)
    {
        count++;
        marked[v] = true;
        foreach (int w in G.Adj(v))
        {
            if (!marked[w]) dfs(G, w);
        }
    }

    public bool Marked(int v)
    {
        validateVertex(v);
        return marked[v];
    }

   
    public int Count()
    {
        return count;
    }
    
    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

    //    private void validateVertices(Iterable<Integer> vertices)
    private void validateVertices(Bag<int> vertices)
    {
        if (vertices == null)
        {
            throw new System.Exception("argument is null");
        }
        int V = marked.Length;
        foreach (int v in vertices)
        {
            if (v < 0 || v >= V)
            {
                throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
            }
        }
    }

    
}
