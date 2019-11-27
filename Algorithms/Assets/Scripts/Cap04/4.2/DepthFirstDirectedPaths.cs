using UnityEngine;
using System.Collections;

public class DepthFirstDirectedPaths : MonoBehaviour {

    public TextAsset txt;
	void Start () {
       
        Digraph G = new Digraph(txt);

        int s = 2;    // 1、2、6
        DepthFirstDirectedPaths dfs = new DepthFirstDirectedPaths(G, s);

        for (int v = 0; v < G.V(); v++)
        {
            if (dfs.hasPathTo(v))
            {
                string str = (s + " to " + v + " :  ");
             
                foreach (int x in dfs.pathTo(v))
                {
                    if (x == s) str+=x;
                    else str+=("-" + x);
                }
                print(str);
            }

            else
            {               
                print(s+" to "+ v+" :  not connected\n");
            }

        }
    }
    private bool[] marked;  // marked[v] = true if v is reachable from s
    private int[] edgeTo;      // edgeTo[v] = last edge on path from s to v
    private  int s;       // source vertex

    public DepthFirstDirectedPaths(Digraph G, int s)
    {
        marked = new bool[G.V()];
        edgeTo = new int[G.V()];
        this.s = s;
        validateVertex(s);
        dfs(G, s);
    }

    private void dfs(Digraph G, int v)
    {
        marked[v] = true;
        foreach (int w in G.Adj(v))
        {
            if (!marked[w])
            {
                edgeTo[w] = v;
                dfs(G, w);
            }
        }
    }

 
    public bool hasPathTo(int v)
    {
        validateVertex(v);
        return marked[v];
    }


    
    public IEnumerable pathTo(int v)
    {
        validateVertex(v);
        if (!hasPathTo(v)) return null;
        Stack<int> path = new Stack<int>();
        for (int x = v; x != s; x = edgeTo[x])
            path.push(x);
        path.push(s);
        return path;
    }

    
    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

   
   

}
