using UnityEngine;
using System.Collections;

public class TransitiveClosure : MonoBehaviour {

    public TextAsset txt;
    void Start()
    {
       
        Digraph G = new Digraph(txt);

        TransitiveClosure tc = new TransitiveClosure(G);

        // print header
       string str=("     ");
        for (int v = 0; v < G.V(); v++)
            str+=v;
        print(str);

        // print transitive closure
        for (int v = 0; v < G.V(); v++)
        {
            str=(v+": ");
            for (int w = 0; w < G.V(); w++)
            {
                if (tc.reachable(v, w)) str+=("  T");
                else str+=("   ");
            }
           print(str);
        }
    }

    private DirectedDFS[] tc;  // tc[v] = reachable from v

    
    public TransitiveClosure(Digraph G)
    {
        tc = new DirectedDFS[G.V()];
        for (int v = 0; v < G.V(); v++)
            tc[v] = new DirectedDFS(G, v);
    }

    
    public bool reachable(int v, int w)
    {
        validateVertex(v);
        validateVertex(w);
        return tc[v].Marked(w);
    }

    
    private void validateVertex(int v)
    {
        int V = tc.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

   
}
