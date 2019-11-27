using UnityEngine;
using System.Collections;

public class Cycle : MonoBehaviour {

    public TextAsset txt;
	void Start () {
        Graph G = new Graph(txt);
        Cycle finder = new Cycle(G);
        if (finder.hasCycle())
        {
            foreach (int v in finder.cycle())
            {
                print(v + " ");
            }
        }
        else
        {
            print("Graph is acyclic");
        }
    }
    private bool[] marked;
    private int[] edgeTo;
    private Stack<int> thisCycle;

    public Cycle(Graph G)
    {
        if (hasSelfLoop(G)) return;
        if (hasParallelEdges(G)) return;
        marked = new bool[G.V()];
        edgeTo = new int[G.V()];
        for (int v = 0; v < G.V(); v++)
            if (!marked[v])
                dfs(G, -1, v);
    }



    private bool hasSelfLoop(Graph G)
    {
        for (int v = 0; v < G.V(); v++)
        {
            foreach (int w in G.Adj(v))
            {
                if (v == w)
                {
                    thisCycle = new Stack<int>();
                    thisCycle.push(v);
                    thisCycle.push(v);
                    return true;
                }
            }
        }
        return false;
    }


    private bool hasParallelEdges(Graph G)
    {
        marked = new bool[G.V()];

        for (int v = 0; v < G.V(); v++)
        {

            // check for parallel edges incident to v
            foreach (int w in G.Adj(v))
            {
                if (marked[w])
                {
                    thisCycle = new Stack<int>();
                    thisCycle.push(v);
                    thisCycle.push(w);
                    thisCycle.push(v);
                    return true;
                }
                marked[w] = true;
            }

            // reset so marked[v] = false for all v
            foreach (int w in G.Adj(v))
            {
                marked[w] = false;
            }
        }
        return false;
    }

   

    public bool hasCycle()
    {
        return thisCycle != null;
    }

   

    public Stack<int> cycle()
    {
        return thisCycle;
    }

    private void dfs(Graph G, int u, int v)
    {
        marked[v] = true;
        foreach (int w in G.Adj(v))
        {

            // short circuit if cycle already found
            if (thisCycle != null) return;

            if (!marked[w])
            {
                edgeTo[w] = v;
                dfs(G, v, w);
            }

            // check for cycle (but disregard reverse of edge leading to v)
            else if (w != u)
            {
                thisCycle = new Stack<int>();
                for (int x = v; x != w; x = edgeTo[x])
                {
                    thisCycle.push(x);
                }
                thisCycle.push(w);
                thisCycle.push(v);
            }
        }
    }

}
