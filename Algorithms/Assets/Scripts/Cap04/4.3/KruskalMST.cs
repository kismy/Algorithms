using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Kruskal 克鲁斯卡尔最小生成树算法
public class KruskalMST : MonoBehaviour {

    public TextAsset txt;
	void Start () {
       
        EdgeWeightedGraph G = new EdgeWeightedGraph(txt,' ');
        KruskalMST mst = new KruskalMST(G);
        foreach (Edge e in mst.edges())
        {
            print(e);
        }
        print(mst.Weight());
    }

    private static  double FLOATING_POINT_EPSILON = 1E-12;

    private double weight;                        // weight of MST
    private Queue<Edge> mst = new Queue<Edge>();  // edges in MST

   

    public KruskalMST(EdgeWeightedGraph G)
    {
        // more efficient to build heap by passing array of edges

        EdgeComparer comparer = new EdgeComparer();
        MinPQ<Edge> pq = new MinPQ<Edge>((Comparer<Edge>)comparer);
        foreach (Edge e in G.edges())
        {
            pq.insert(e);
        }

        // run greedy algorithm
        UF uf = new UF(G.V());
        while (!pq.isEmpty() && mst.size() < G.V() - 1)
        {
            Edge e = pq.delMin();
            int v = e.either();
            int w = e.other(v);
            if (!uf.connected(v, w))
            { // v-w does not create a cycle
                uf.union(v, w);  // merge v and w components
                mst.Enqueue(e);  // add edge e to mst
                weight += e.Weight();
            }
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

    // check optimality conditions (takes time proportional to E V lg* V)
    private bool check(EdgeWeightedGraph G)
    {

        // check total weight
        double total = 0.0;
        foreach (Edge e in edges())
        {
            total += e.Weight();
        }
        if (Mathf.Abs((float)(total - Weight())) > FLOATING_POINT_EPSILON)
        {
            throw new System.Exception("Weight of edges does not equal weight(): "+ total+" vs. "+ Weight()+"\n");
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
