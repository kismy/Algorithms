using UnityEngine;
using System;
using System.Collections;

public class Arbitrage : MonoBehaviour {

    public TextAsset txt;
	void Start () {
        string[] lines = txt.text.Split(new char[] {'\n'});

        // V currencies
        int V = int.Parse(lines[0]);
        string[] name = new string[V];

        // create complete network
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(V);
        for (int v = 0; v < V; v++)
        {
            string[] lineStrs = lines[v+1].Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
            name[v] = lineStrs[0];
            for (int w = 0; w < V; w++)
            {
                double rate =double.Parse(lineStrs[w+1]);
                DirectedEdge e = new DirectedEdge(v, w, -Math.Log(rate));
                G.addEdge(e);
            }
        }

        // find negative cycle
        BellmanFordSP spt = new BellmanFordSP(G, 0);
        if (spt.hasNegativeCycle())
        {
            double stake = 1000.0;
            foreach (DirectedEdge e in spt.negativeCycle())
            {
               string str=(stake+" "+name[e.from()]+" ");
                stake *= Math.Exp(-e.Weight());
                str+=("= "+ stake+" "+name[e.to()]+"\n");
                print(str);
            }
            
            print("以1000元为本金，该负权重环一圈套利" + (stake - 1000));
        }
        else
        {
            print("No arbitrage opportunity");
        }
    }
    private Arbitrage() { }

}
