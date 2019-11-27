using UnityEngine;
using System.Collections;

public class AcyclicLP : MonoBehaviour {
    public TextAsset txt;
	void Start () {

        int s =5;
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(txt);

        AcyclicLP lp = new AcyclicLP(G, s);

        for (int v = 0; v < G.V(); v++)
        {
            if (lp.hasPathTo(v))
            {
                string str= (s+" to "+v+"  distance= "+lp.DistTo(v));
                foreach (DirectedEdge e in lp.PathTo(v))
                {
                    str+=("     "+ e);
                }
              print(str);
            }
            else
            {
              print(s+" to "+ v+"         no path\n");
            }
        }
    }
    private double[] distTo;          // distTo[v] = distance  of longest s->v path
    private DirectedEdge[] edgeTo;    // edgeTo[v] = last edge on longest s->v path

   
    public AcyclicLP(EdgeWeightedDigraph G, int s)
    {
        distTo = new double[G.V()];
        edgeTo = new DirectedEdge[G.V()];

        validateVertex(s);

        for (int v = 0; v < G.V(); v++)
            distTo[v] = double.NegativeInfinity;
        distTo[s] = 0.0;

        // relax vertices in toplogical order
        Topological topological = new Topological(G);
        if (!topological.hasOrder())
            throw new System.Exception("Digraph is not acyclic.");
        foreach (int v in topological.Order())
        {
            foreach (DirectedEdge e in G.Adj(v))
                relax(e);
        }
    }

    // relax edge e, but update if you find a *longer* path
    private void relax(DirectedEdge e)
    {
        int v = e.from(), w = e.to();
        if (distTo[w] < distTo[v] + e.Weight())
        {
            distTo[w] = distTo[v] + e.Weight();
            edgeTo[w] = e;
        }
    }

   
    public double DistTo(int v)
    {
        validateVertex(v);
        return distTo[v];
    }

   
    public bool hasPathTo(int v)
    {
        validateVertex(v);
        return distTo[v] > double.NegativeInfinity;
    }

  
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

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertex(int v)
    {
        int V = distTo.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }


}
