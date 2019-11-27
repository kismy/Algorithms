using UnityEngine;
using System;
using System.Collections;

public class CPM : MonoBehaviour {

    public TextAsset txt;
	void Start () {
        string[] lines = txt.text.Split('\n');
        // number of jobs
        int n = int.Parse(lines[0]);

        // source and sink
        int source = 2 * n;
        int sink = 2 * n + 1;

        // build network
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(2 * n + 2);
        for (int i = 0; i < n; i++)
        {          
            string[] strs = lines[i + 1].Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double duration = double.Parse(strs[0]);
            G.addEdge(new DirectedEdge(source, i, 0.0));
            G.addEdge(new DirectedEdge(i + n, sink, 0.0));
            G.addEdge(new DirectedEdge(i, i + n, duration));

            // precedence constraints  
            int m = int.Parse(strs[1]);
            for (int j = 0; j < m; j++)
            {
                int precedent = int.Parse(strs[2 + j]);
                G.addEdge(new DirectedEdge(n + i, precedent, 0.0));
            }
        }

        // compute longest path
        AcyclicLP lp = new AcyclicLP(G, source);

        // print results
        print("jobID \t startTime \t finishTime");

        for (int i = 0; i < n; i++)
        {
            print(i + "  \t " + lp.DistTo(i) + " \t " + lp.DistTo(i + n));
        }
        print("Finish time:" + lp.DistTo(sink));

    }

    private CPM() { }

 
    void Main()
    {
       
    }
}
