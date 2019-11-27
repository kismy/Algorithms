using UnityEngine;
using System.Collections;

public class BoruvkaMST : MonoBehaviour {
    public TextAsset txt;
	void Start () {
       
        EdgeWeightedGraph G = new EdgeWeightedGraph(txt,' ');
        BoruvkaMST mst = new BoruvkaMST(G);
        foreach (Edge e in mst.edges())
        {
            print(e);
        }
       print(mst.Weight());
    }

    private static  double FLOATING_POINT_EPSILON = 1E-12;

    private Bag<Edge> mst = new Bag<Edge>();    // edges in MST
    private double weight;                      // weight of MST

  


    public BoruvkaMST(EdgeWeightedGraph G)
    {
        UF uf = new UF(G.V());

        // repeat at most log V times or until we have V-1 edges
        for (int t = 1; t < G.V() && mst.size() < G.V() - 1; t = t + t)
        {

            // foreach tree in forest, find closest edge
            // if edge weights are equal, ties are broken in favor of first edge in G.edges()
            Edge[] closest = new Edge[G.V()];
            foreach (Edge e in G.edges())
            {
                int v = e.either(), w = e.other(v);
                int i = uf.find(v), j = uf.find(w);
                if (i == j) continue;   // same tree
                if (closest[i] == null || less(e, closest[i])) closest[i] = e;
                if (closest[j] == null || less(e, closest[j])) closest[j] = e;
            }

            // add newly discovered edges to MST
            for (int i = 0; i < G.V(); i++)
            {
                Edge e = closest[i];
                if (e != null)
                {
                    int v = e.either(), w = e.other(v);
                    // don't add the same edge twice
                    if (!uf.connected(v, w))
                    {
                        mst.Add(e);
                        weight += e.Weight();
                        uf.union(v, w);
                    }
                }
            }
        }

    }

   

    public Bag<Edge> edges()
    {
        return mst;
    }


   
    public double Weight()
    {
        return weight;
    }

    // is the weight of edge e strictly less than that of edge f?
    private static bool less(Edge e, Edge f)
    {
        return e.Weight() < f.Weight();
    }

    // check optimality conditions (takes time proportional to E V lg* V)
    private bool check(EdgeWeightedGraph G)
    {

        // check weight
        double totalWeight = 0.0;
        foreach (Edge e in edges())
        {
            totalWeight += e.Weight();
        }
        if (Mathf.Abs((float)(totalWeight - Weight())) > FLOATING_POINT_EPSILON)
        {
           throw new System.Exception("Weight of edges does not equal weight(): "+ totalWeight+" vs. "+Weight()+"\n");
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
