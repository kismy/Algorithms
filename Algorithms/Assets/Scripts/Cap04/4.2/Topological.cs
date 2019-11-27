using UnityEngine;
using System.Collections;

public class Topological : MonoBehaviour {

    public TextAsset txt;
	void Start () {
       
        SymbolDigraph sg = new SymbolDigraph(txt,'/');
        Topological topological = new Topological(sg.digraph());
        foreach (int v in topological.Order())
        {
            print(sg.nameOf(v));
        }
    }
    private Stack<int> order;  // topological order
    private int[] rank;               // rank[v] = position of vertex v in topological order

   
    public Topological(Digraph G)
    {
        DirectedCycle finder = new DirectedCycle(G);
        if (!finder.hasCycle())
        {
            DepthFirstOrder dfs = new DepthFirstOrder(G);
            order = dfs.reversePost();
            rank = new int[G.V()];
            int i = 0;
            foreach (int v in order)
                rank[v] = i++;
        }
    }

   
    public Topological(EdgeWeightedDigraph G)
    {
        EdgeWeightedDirectedCycle finder = new EdgeWeightedDirectedCycle(G);
        if (!finder.hasCycle())
        {
            DepthFirstOrder dfs = new DepthFirstOrder(G);
            order = dfs.reversePost();
        }
    }

  
    public Stack<int> Order()
    {
        return order;
    }

  
    public bool hasOrder()
    {
        return order != null;
    }

   
    public bool isDAG()
    {
        return hasOrder();
    }

    
    public int Rank(int v)
    {
        validateVertex(v);
        if (hasOrder()) return rank[v];
        else return -1;
    }

   
    private void validateVertex(int v)
    {
        int V = rank.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }


}
