using UnityEngine;
using System.Collections;

public class DijkstraAllPairsSP : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    private DijkstraSP[] all;

    public DijkstraAllPairsSP(EdgeWeightedDigraph G)
    {
        all = new DijkstraSP[G.V()];
        for (int v = 0; v < G.V(); v++)
            all[v] = new DijkstraSP(G, v);
    }

   
    public Stack<DirectedEdge> path(int s, int t)
    {
        validateVertex(s);
        validateVertex(t);
        return all[s].PathTo(t);
    }

   
    public bool hasPath(int s, int t)
    {
        validateVertex(s);
        validateVertex(t);
        return Dist(s, t) < double.PositiveInfinity;
    }

    
    public double Dist(int s, int t)
    {
        validateVertex(s);
        validateVertex(t);
        return all[s].DistTo(t);
    }

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertex(int v)
    {
        int V = all.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }
}
