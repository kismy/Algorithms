using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


//prim 普里姆最小生成树算法
//最小生成树，也称最小权重生成树
//例如：要在n个城市之间铺设光缆，主要目标是要使这 n 个城市的任意两个之间都可以通信，
//但铺设光缆的费用很高，且各个城市之间铺设光缆的费用不同，因此另一个目标是要使铺设光缆的总费用最低。这就需要找到带权的最小生成树
public class PrimMST : MonoBehaviour {

    public TextAsset txt;
	void Start () {
       
        EdgeWeightedGraph G = new EdgeWeightedGraph(txt,' ');
        PrimMST mst = new PrimMST(G);
        foreach (Edge e in mst.Edges())
        {
           print(e);
        }
        print(mst.weight());
    }

    private static  double FLOATING_POINT_EPSILON = 1E-12;

    private Edge[] edgeTo;        // edgeTo[v] = shortest edge from tree vertex to non-tree vertex
    private double[] distTo;      // distTo[v] = weight of shortest such edge
    private bool[] marked;     // marked[v] = true if v on tree, false otherwise
    private IndexMinPQ<double> pq;

   

    public PrimMST(EdgeWeightedGraph G)
    {
        edgeTo = new Edge[G.V()];
        distTo = new double[G.V()];
        marked = new bool[G.V()];

        IndexMinPQComparer comparer = new IndexMinPQComparer();
        pq = new IndexMinPQ<double>(G.V(), (Comparer<double>)comparer);
        for (int v = 0; v < G.V(); v++)
            distTo[v] = double.PositiveInfinity;

        for (int v = 0; v < G.V(); v++)      // run from each vertex to find
            if (!marked[v]) prim(G, v);      // minimum spanning forest


    }

   

    private void prim(EdgeWeightedGraph G, int s)
    {
        distTo[s] = 0.0;
        pq.insert(s, distTo[s]);
        while (!pq.isEmpty())
        {
            int v = pq.delMin();
            scan(G, v);
        }
    }

   

    private void scan(EdgeWeightedGraph G, int v)
    {
        marked[v] = true;
        foreach (Edge e in G.Adj(v))
        {
            int w = e.other(v);
            if (marked[w]) continue;         // v-w is obsolete edge
            if (e.Weight() < distTo[w])
            {
                distTo[w] = e.Weight();
                edgeTo[w] = e;
                if (pq.contains(w)) pq.decreaseKey(w, distTo[w]);
                else pq.insert(w, distTo[w]);
            }
        }
    }

    

    public Queue<Edge> Edges()
    {
        Queue<Edge> mst = new Queue<Edge>();
        for (int v = 0; v < edgeTo.Length; v++)
        {
            Edge e = edgeTo[v];
            if (e != null)
            {
                mst.Enqueue(e);
            }
        }
        return mst;
    }

  

    public double weight()
    {
        double weight = 0.0;
        foreach (Edge e in Edges())
            weight += e.Weight();
        return weight;
    }


    // check optimality conditions (takes time proportional to E V lg* V)
    private bool check(EdgeWeightedGraph G)
    {

        // check weight
        double totalWeight = 0.0;
        foreach (Edge e in Edges())
        {
            totalWeight += e.Weight();
        }
        if (Mathf.Abs((float)(totalWeight - weight())) > FLOATING_POINT_EPSILON)
        {
            throw  new System.Exception("Weight of edges does not equal weight(): "+ totalWeight+" vs. "+ weight()+"\n");
            return false;
        }

        // check that it is acyclic
        UF uf = new UF(G.V());
        foreach (Edge e in Edges())
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
        foreach (Edge e in Edges())
        {

            // all edges in MST except e
            uf = new UF(G.V());
            foreach (Edge f in Edges())
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
