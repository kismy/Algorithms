using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LazyPrimMST : MonoBehaviour {
    public TextAsset txt;
    void Start()
    {
       
        EdgeWeightedGraph G = new EdgeWeightedGraph(txt, ' ');
        LazyPrimMST mst = new LazyPrimMST(G);
        foreach (Edge e in mst.edges())
        {
            print(e.ToString());
        }
        print(mst.Weight());
    }

    private static  double FLOATING_POINT_EPSILON = 1E-12;

    private double weight;       // total weight of MST
    private Queue<Edge> mst;     // edges in the MST
    private bool[] marked;    // marked[v] = true if v on tree
    private MinPQ<Edge> pq;      // edges with one endpoint in tree

    public LazyPrimMST(EdgeWeightedGraph G)
    {
        mst = new Queue<Edge>();
        EdgeComparer ec = new EdgeComparer();
        Comparer<Edge> edgeComparer = (Comparer<Edge>)ec;

        pq = new MinPQ<Edge>(edgeComparer);
        marked = new bool[G.V()];
        for (int v = 0; v < G.V(); v++)     // run Prim from all vertices to
            if (!marked[v]) prim(G, v);     // get a minimum spanning forest

      
    }

    
    private void prim(EdgeWeightedGraph G, int s)
    {
       
        scan(G, s);
        while (!pq.isEmpty())
        {                        // better to stop when mst has V-1 edges
            Edge e = pq.delMin();                      // smallest edge on pq
            int v = e.either(), w = e.other(v);        // two endpoints

            if (marked[v] && marked[w]) continue;      // lazy, both v and w already scanned
            mst.Enqueue(e);                            // add e to MST
            weight += e.Weight();
            if (!marked[v]) scan(G, v);               // v becomes part of tree
            if (!marked[w]) scan(G, w);               // w becomes part of tree
        }
    }

    
    private void scan(EdgeWeightedGraph G, int v)
    {
        marked[v] = true;
        foreach (Edge e in G.Adj(v))
        {

            if (!marked[e.other(v)]) pq.insert(e);
        }
    }

    
    public Queue<Edge> edges()
    {
        return mst;
    }

  
    public double Weight()
    {
        return weight;
    }

   
    private bool check(EdgeWeightedGraph G)
    {

        // check weight
        double totalWeight = 0.0;
        foreach (Edge e in edges())
        {
            totalWeight += e.Weight();
        }
        if (Mathf.Abs((float)(totalWeight - (double)Weight())) > FLOATING_POINT_EPSILON)
        {
            throw new System.Exception("Weight of edges does not equal weight(): "+ totalWeight+" vs. "+ Weight()+"\n" );
            return false;
        }

        // check that it is acyclic
        UF uf = new UF(G.V());
        foreach (Edge e in edges())
        {
            int v = e.either(), w = e.other(v);
            if (uf.connected(v, w))
            {
                throw new System.Exception("Not a forest");
                return false;
            }
            uf.union(v, w);
        }

        // check that it is a spanning forest
        foreach (Edge e in G.edges())
        {
            int v = e.either(), w = e.other(v);
            if (!uf.connected(v, w))
            {
                throw new System.Exception("Not a spanning forest");
                return false;
            }
        }

        // check that it is a minimal spanning forest (cut optimality conditions)
        foreach (Edge e in edges())
        {

            // all edges in MST except e
            uf = new UF(G.V());
            foreach (Edge f in mst)
            {
                int x = f.either(), y = f.other(x);
                if (f != e) uf.union(x, y);
            }

            // check that e is min weight edge in crossing cut
            foreach (Edge f in G.edges())
            {
                int x = f.either(), y = f.other(x);
                if (!uf.connected(x, y))
                {
                    if (f.Weight() < e.Weight())
                    {
                        throw new System.Exception("Edge " + f + " violates cut optimality conditions");
                        return false;
                    }
                }
            }

        }

        return true;
    }

   
}
