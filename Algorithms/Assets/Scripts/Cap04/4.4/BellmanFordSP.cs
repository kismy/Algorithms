using UnityEngine;
using System.Collections;


//图的最短路径之 贝尔曼·福特（Bellman-Ford）算法
public class BellmanFordSP : MonoBehaviour {
    public TextAsset txt;
	void Start () {
      
        int s = 0;
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(txt);

        BellmanFordSP sp = new BellmanFordSP(G, s);

        // print negative cycle
        if (sp.hasNegativeCycle())
        {
            foreach (DirectedEdge e in sp.negativeCycle())
                print(e);
        }

        // print shortest paths
        else
        {
            for (int v = 0; v < G.V(); v++)
            {
                if (sp.hasPathTo(v))
                {
                    string str=(s+" to "+ v+"  Distance="+ sp.DistTo(v)+"     ");
                    foreach (DirectedEdge e in sp.PathTo(v))
                    {
                       str+=(e + "   ");
                    }
                   print(str);
                }
                else
                {
                   print(s+" to "+ v+"           no path\n");
                }
            }
        }
    }

    private double[] distTo;               // distTo[v] = distance  of shortest s->v path
    private DirectedEdge[] edgeTo;         // edgeTo[v] = last edge on shortest s->v path
    private bool[] onQueue;             // onQueue[v] = is v currently on the queue?
    private Queue<int> queue;          // queue of vertices to relax
    private int cost;                      // number of calls to relax()
    private Stack<DirectedEdge> cycle;  // negative cycle (or null if no such cycle)

    /**
     * Computes a shortest paths tree from {@code s} to every other vertex in
     * the edge-weighted digraph {@code G}.
     * @param G the acyclic digraph
     * @param s the source vertex
     * @throws IllegalArgumentException unless {@code 0 <= s < V}
     */
    public BellmanFordSP(EdgeWeightedDigraph G, int s)
    {
        distTo = new double[G.V()];
        edgeTo = new DirectedEdge[G.V()];
        onQueue = new bool[G.V()];
        for (int v = 0; v < G.V(); v++)
            distTo[v] = double.PositiveInfinity;
        distTo[s] = 0.0;

        // Bellman-Ford algorithm
        queue = new Queue<int>();
        queue.Enqueue(s);
        onQueue[s] = true;
        while (!queue.isEmpty() && !hasNegativeCycle())
        {
            int v = queue.Dequeue();
            onQueue[v] = false;
            relax(G, v);
        }
    }

    // relax vertex v and put other endpoints on queue if changed
    private void relax(EdgeWeightedDigraph G, int v)
    {
        foreach (DirectedEdge e in G.Adj(v))
        {
            int w = e.to();
            if (distTo[w] > distTo[v] + e.Weight())
            {
                distTo[w] = distTo[v] + e.Weight();
                edgeTo[w] = e;
                if (!onQueue[w])
                {
                    queue.Enqueue(w);
                    onQueue[w] = true;
                }
            }
            if (cost++ % G.V() == 0)
            {
                findNegativeCycle();
                if (hasNegativeCycle()) return;  // found a negative cycle
            }
        }
    }

    /**
     * Is there a negative cycle reachable from the source vertex {@code s}?
     * @return {@code true} if there is a negative cycle reachable from the
     *    source vertex {@code s}, and {@code false} otherwise
     */
    public bool hasNegativeCycle()
    {
        return cycle != null;
    }

    /**
     * Returns a negative cycle reachable from the source vertex {@code s}, or {@code null}
     * if there is no such cycle.
     * @return a negative cycle reachable from the soruce vertex {@code s} 
     *    as an iterable of edges, and {@code null} if there is no such cycle
     */
    public Stack<DirectedEdge> negativeCycle()
    {
        return cycle;
    }

    // by finding a cycle in predecessor graph
    private void findNegativeCycle()
    {
        int V = edgeTo.Length;
        EdgeWeightedDigraph spt = new EdgeWeightedDigraph(V);
        for (int v = 0; v < V; v++)
            if (edgeTo[v] != null)
                spt.addEdge(edgeTo[v]);

        EdgeWeightedDirectedCycle finder = new EdgeWeightedDirectedCycle(spt);
        cycle = finder.Cycle();
    }

    /**
     * Returns the length of a shortest path from the source vertex {@code s} to vertex {@code v}.
     * @param  v the destination vertex
     * @return the length of a shortest path from the source vertex {@code s} to vertex {@code v};
     *         {@code Double.POSITIVE_INFINITY} if no such path
     * @throws UnsupportedOperationException if there is a negative cost cycle reachable
     *         from the source vertex {@code s}
     * @throws IllegalArgumentException unless {@code 0 <= v < V}
     */
    public double DistTo(int v)
    {
        validateVertex(v);
        if (hasNegativeCycle())
            throw new System.Exception("Negative cost cycle exists");
        return distTo[v];
    }

    /**
     * Is there a path from the source {@code s} to vertex {@code v}?
     * @param  v the destination vertex
     * @return {@code true} if there is a path from the source vertex
     *         {@code s} to vertex {@code v}, and {@code false} otherwise
     * @throws IllegalArgumentException unless {@code 0 <= v < V}
     */
    public bool hasPathTo(int v)
    {
        validateVertex(v);
        return distTo[v] < double.PositiveInfinity;
    }

    /**
     * Returns a shortest path from the source {@code s} to vertex {@code v}.
     * @param  v the destination vertex
     * @return a shortest path from the source {@code s} to vertex {@code v}
     *         as an iterable of edges, and {@code null} if no such path
     * @throws UnsupportedOperationException if there is a negative cost cycle reachable
     *         from the source vertex {@code s}
     * @throws IllegalArgumentException unless {@code 0 <= v < V}
     */
    public Stack<DirectedEdge> PathTo(int v)
    {
        validateVertex(v);
        if (hasNegativeCycle())
            throw new System.Exception("Negative cost cycle exists");
        if (!hasPathTo(v)) return null;
        Stack<DirectedEdge> path = new Stack<DirectedEdge>();
        for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.from()])
        {
            path.push(e);
        }
        return path;
    }

    // check optimality conditions: either 
    // (i) there exists a negative cycle reacheable from s
    //     or 
    // (ii)  for all edges e = v->w:            distTo[w] <= distTo[v] + e.weight()
    // (ii') for all edges e = v->w on the SPT: distTo[w] == distTo[v] + e.weight()
    private bool check(EdgeWeightedDigraph G, int s)
    {

        // has a negative cycle
        if (hasNegativeCycle())
        {
            double weight = 0.0;
            foreach (DirectedEdge e in negativeCycle())
            {
                weight += e.Weight();
            }
            if (weight >= 0.0)
            {
               throw new System.Exception("error: weight of negative cycle = " + weight);
                return false;
            }
        }

        // no negative cycle reachable from source
        else
        {

            // check that distTo[v] and edgeTo[v] are consistent
            if (distTo[s] != 0.0 || edgeTo[s] != null)
            {
                throw new System.Exception("distanceTo[s] and edgeTo[s] inconsistent");
                return false;
            }
            for (int v = 0; v < G.V(); v++)
            {
                if (v == s) continue;
                if (edgeTo[v] == null && distTo[v] != double.PositiveInfinity)
                {
                    throw new System.Exception("distTo[] and edgeTo[] inconsistent");
                    return false;
                }
            }

            // check that all edges e = v->w satisfy distTo[w] <= distTo[v] + e.weight()
            for (int v = 0; v < G.V(); v++)
            {
                foreach (DirectedEdge e in G.Adj(v))
                {
                    int w = e.to();
                    if (distTo[v] + e.Weight() < distTo[w])
                    {
                        throw new System.Exception("edge " + e + " not relaxed");
                        return false;
                    }
                }
            }

            // check that all edges e = v->w on SPT satisfy distTo[w] == distTo[v] + e.weight()
            for (int w = 0; w < G.V(); w++)
            {
                if (edgeTo[w] == null) continue;
                DirectedEdge e = edgeTo[w];
                int v = e.from();
                if (w != e.to()) return false;
                if (distTo[v] + e.Weight() != distTo[w])
                {
                    throw new System.Exception("edge " + e + " on shortest path not tight");
                    return false;
                }
            }
        }

        print("Satisfies optimality conditions");
        return true;
    }

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertex(int v)
    {
        int V = distTo.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

   

}
