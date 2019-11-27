using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DijkstraSP : MonoBehaviour {

    public TextAsset Graph;
    void Start()
    {
        double a = double.PositiveInfinity;
        double b = (double.PositiveInfinity + 10.0);
        print(a > b);
        print(a < b);
        print(a == b);

        EdgeWeightedDigraph G = new EdgeWeightedDigraph(Graph);
        int s = 0;

        // compute shortest paths
        DijkstraSP sp = new DijkstraSP(G, s);


        // print shortest path
        for (int t = 0; t < G.V(); t++)
        {
            if (sp.hasPathTo(t))
            {
                string str = s + " to " + t + "  Distance=" + sp.DistTo(t);
                
                foreach (DirectedEdge e in sp.PathTo(t))
                {
                    str+=(e + "\t");
                }
                print(str);
            }
            else
            {
                print(s+" to "+ t+"         no path\n");
            }
        }
    }

    private double[] distTo;          // distTo[v] = distance  of shortest s->v path
    private DirectedEdge[] edgeTo;    // edgeTo[v] = last edge on shortest s->v path
    private IndexMinPQ<double> pq;    // priority queue of vertices

   
    public DijkstraSP(EdgeWeightedDigraph G, int s)
    {
        foreach (DirectedEdge e in G.edges())
        {
            if (e.Weight() < 0)
                throw new System.Exception("edge " + e + " has negative weight");
        }

        distTo = new double[G.V()];
        edgeTo = new DirectedEdge[G.V()];

        validateVertex(s);

        for (int v = 0; v < G.V(); v++)
            distTo[v] = double.PositiveInfinity;
        distTo[s] = 0.0;


        // relax vertices in order of distance from s
        IndexMinPQComparer comparator =new IndexMinPQComparer();
        pq = new IndexMinPQ<double>(G.V(),(Comparer<double>)comparator);
        pq.insert(s, distTo[s]);
        while (!pq.isEmpty())
        {
            int v = pq.delMin();
            foreach (DirectedEdge e in G.Adj(v))
                relax(e);
        }

       
    }

    // relax edge e and update pq if changed
    private void relax(DirectedEdge e)
    {
        int v = e.from(), w = e.to();
        if (distTo[w] > distTo[v] + e.Weight())
        {
            distTo[w] = distTo[v] + e.Weight();
            edgeTo[w] = e;
            if (pq.contains(w)) pq.decreaseKey(w, distTo[w]);
            else pq.insert(w, distTo[w]);
        }
    }

    private void relax(EdgeWeightedDigraph G,int v)
    {
        foreach (DirectedEdge e in G.Adj(v))
        {
            int  w = e.to();
            if (distTo[w] > distTo[v] + e.Weight())
            {
                distTo[w] = distTo[v] + e.Weight();
                edgeTo[w] = e;
            }

        }
       
    }


    //起点s到v的距离，如果路径不存在，则为无穷大
    public double DistTo(int v)
    {
        validateVertex(v);
        return distTo[v];
    }

   

    //是否存在s到v的路径
    public bool hasPathTo(int v)
    {
        validateVertex(v);
        return distTo[v] < double.PositiveInfinity;
    }

  //起点s到终点v的最短有向路径，不存在则返回null
    public Stack<DirectedEdge> PathTo(int v)
    {
        validateVertex(v);
        if (!hasPathTo(v)) return null;
        Stack<DirectedEdge> path = new Stack<DirectedEdge>();
        for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.from()])
        {
            path.push(e);
        }
        return path;
    }


    private bool Check(EdgeWeightedDigraph G, int s)
    {

        // check that edge weights are nonnegative
        foreach (DirectedEdge e in G.edges())
        {
            if (e.Weight() < 0)
            {
                throw new System.Exception("negative edge weight detected");
                return false;
            }
        }

        // check that distTo[v] and edgeTo[v] are consistent
        if (distTo[s] != 0.0 || edgeTo[s] != null)
        {
            throw new System.Exception("distTo[s] and edgeTo[s] inconsistent");
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
