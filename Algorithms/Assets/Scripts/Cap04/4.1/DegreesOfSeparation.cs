using UnityEngine;
using System.Collections;


public class DegreesOfSeparation : MonoBehaviour {
    public TextAsset txt;
    void Start() {
      
        string source = "JFK";
        SymbolGraph sg = new SymbolGraph(txt);
        Graph G = sg.Graph();
        if (!sg.Contains(source))
        {
            print(source + " not in database.");
            return;
        }

        int s = sg.IndexOf(source);
        BreadthFirstPaths bfs = new BreadthFirstPaths(G, s);

        string sink = "DFW";
        if (sg.Contains(sink))
        {
            int t = sg.IndexOf(sink);
            if (bfs.hasPathTo(t))
            { 
                foreach (int v in bfs.pathTo(t))
                {
                    print("   " + sg.NameOf(v));
                }
            }
            else
            {
               print("Not connected");
            }
        }
        else
        {
            print("   Not in database.");
        }
    }
    private DegreesOfSeparation() { }

   
   
}