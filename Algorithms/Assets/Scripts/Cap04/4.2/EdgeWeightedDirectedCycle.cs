using UnityEngine;
using System.Collections;

public class EdgeWeightedDirectedCycle : MonoBehaviour {

	// Use this for initialization
	//void Start () {

 //       // create random DAG with V vertices and E edges; then add F random edges
 //       int V = Integer.parseInt(args[0]);
 //       int E = Integer.parseInt(args[1]);
 //       int F = Integer.parseInt(args[2]);
 //       EdgeWeightedDigraph G = new EdgeWeightedDigraph(V);
 //       int[] vertices = new int[V];
 //       for (int i = 0; i < V; i++)
 //           vertices[i] = i;
 //       StdRandom.shuffle(vertices);
 //       for (int i = 0; i < E; i++)
 //       {
 //           int v, w;
 //           do
 //           {
 //               v = StdRandom.uniform(V);
 //               w = StdRandom.uniform(V);
 //           } while (v >= w);
 //           double weight = StdRandom.uniform();
 //           G.addEdge(new DirectedEdge(v, w, weight));
 //       }

 //       // add F extra edges
 //       for (int i = 0; i < F; i++)
 //       {
 //           int v = StdRandom.uniform(V);
 //           int w = StdRandom.uniform(V);
 //           double weight = StdRandom.uniform(0.0, 1.0);
 //           G.addEdge(new DirectedEdge(v, w, weight));
 //       }

 //       StdOut.println(G);

 //       // find a directed cycle
 //       EdgeWeightedDirectedCycle finder = new EdgeWeightedDirectedCycle(G);
 //       if (finder.hasCycle())
 //       {
 //           StdOut.print("Cycle: ");
 //           for (DirectedEdge e : finder.cycle())
 //           {
 //               StdOut.print(e + " ");
 //           }
 //           StdOut.println();
 //       }

 //       // or give topologial sort
 //       else
 //       {
 //           StdOut.println("No directed cycle");
 //       }
 //   }
    private bool[] marked;             // marked[v] = has vertex v been marked?
    private DirectedEdge[] edgeTo;        // edgeTo[v] = previous edge on path to v
    private bool[] onStack;            // onStack[v] = is vertex on the stack?
    private Stack<DirectedEdge> cycle;    // directed cycle (or null if no such cycle)

   
    public EdgeWeightedDirectedCycle(EdgeWeightedDigraph G)
    {
        marked = new bool[G.V()];
        onStack = new bool[G.V()];
        edgeTo = new DirectedEdge[G.V()];
        for (int v = 0; v < G.V(); v++)
            if (!marked[v]) dfs(G, v);
    }

    private void dfs(EdgeWeightedDigraph G, int v)
    {
        onStack[v] = true;
        marked[v] = true;
        foreach (DirectedEdge e in G.Adj(v))
        {
            int w = e.to();

            // short circuit if directed cycle found
            if (cycle != null) return;

            // found new vertex, so recur
            else if (!marked[w])
            {
                edgeTo[w] = e;
                dfs(G, w);
            }

            // trace back directed cycle
            else if (onStack[w])
            {
                cycle = new Stack<DirectedEdge>();

                DirectedEdge f = e;
                while (f.from() != w)
                {
                    cycle.push(f);
                    f = edgeTo[f.from()];
                }
                cycle.push(f);

                return;
            }
        }

        onStack[v] = false;
    }

  
    public bool hasCycle()
    {
        return cycle != null;
    }

 
    public Stack<DirectedEdge> Cycle()
    {
        return cycle;
    }


    // certify that digraph is either acyclic or has a directed cycle
    private bool check()
    {

        // edge-weighted digraph is cyclic
        if (hasCycle())
        {
            // verify cycle
            DirectedEdge first = null, last = null;
            foreach (DirectedEdge e in Cycle())
            {
                if (first == null) first = e;
                if (last != null)
                {
                    if (last.to() != e.from())
                    {
                        throw new System.Exception("cycle edges "+ last+" and "+ e+" not incident\n");
                        return false;
                    }
                }
                last = e;
            }

            if (last.to() != first.from())
            {
                throw new System.Exception("cycle edges "+ last+" and "+ first+" not incident\n");
                return false;
            }
        }


        return true;
    }

   
}
