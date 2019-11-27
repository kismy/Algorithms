using UnityEngine;
using System.Collections;

public class DirectedCycle : MonoBehaviour {

    public TextAsset txt;
	void Start () {
      
        Digraph G = new Digraph(txt);

        DirectedCycle finder = new DirectedCycle(G);
        if (finder.hasCycle())
        {
            string str = "Directed cycle: ";
           
            foreach (int v in finder.Cycle())
            {
                str+=(v + " ");
            }
            print(str);
        }

        else
        {
            print("No directed cycle");
        }
       
    }

    private bool[] marked;        // marked[v] = has vertex v been marked?
    private int[] edgeTo;            // edgeTo[v] = previous vertex on path to v
    private bool[] onStack;       // onStack[v] = is vertex on the stack?
    private Stack<int> cycle;    // directed cycle (or null if no such cycle)

   
    public DirectedCycle(Digraph G)
    {
        marked = new bool[G.V()];
        onStack = new bool[G.V()];
        edgeTo = new int[G.V()];
        for (int v = 0; v < G.V(); v++)
            if (!marked[v] && cycle == null) dfs(G, v);
    }

    
    private void dfs(Digraph G, int v)
    {
        onStack[v] = true;
        marked[v] = true;
        foreach (int w in G.Adj(v))
        {

            // short circuit if directed cycle found
            if (cycle != null) return;

            // found new vertex, so recur
            else if (!marked[w])
            {
                edgeTo[w] = v;
                dfs(G, w);
            }

            // trace back directed cycle
            else if (onStack[w])
            {
                cycle = new Stack<int>();
                for (int x = v; x != w; x = edgeTo[x])
                {
                    cycle.push(x);
                }
                cycle.push(w);
                cycle.push(v);
               
            }
        }
        onStack[v] = false;
    }

 
    public bool hasCycle()
    {
        return cycle != null;
    }

  
    public Stack<int> Cycle()
    {
        return cycle;
    }


    private bool check()
    {

        if (hasCycle())
        {
            // verify cycle
            int first = -1, last = -1;
            foreach (int v in Cycle())
            {
                if (first == -1) first = v;
                last = v;
            }
            if (first != last)
            {
                throw new System.Exception("cycle begins with "+ first +" and ends with "+ last);
                return false;
            }
        }


        return true;
    }

  
}
