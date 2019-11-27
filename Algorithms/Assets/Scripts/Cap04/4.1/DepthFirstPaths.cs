using UnityEngine;
using System.Collections;

public class DepthFirstPaths : MonoBehaviour {

    public TextAsset txt;
	void Start () {
        Test(txt);
	}
    public static void Test(TextAsset txt)
    {
        Graph G = new Graph(txt);
        int s = 0;
        //int s = Random.Range(0, int.Parse(txt.text.Split('\n')[0]));
        DepthFirstPaths dfs = new DepthFirstPaths(G, s);

        for (int v = 0; v < G.V(); v++)
        {
            if (dfs.hasPathTo(v))
            {
                string str = s + " to " + v + ":  ";

                foreach (int x in dfs.pathTo(v))
                {
                    if (x == s) str += x;
                    else str += ("-" + x);
                }
                print(str);

            }
            else print(s + " to " + v + ":  not connected\n");
        }
    }

    private bool[] marked;    // marked[v] = is there an s-v path?
    private int[] edgeTo;        // edgeTo[v] = last edge on s-v path  记录每个顶点到起点s的路径
    private  int s;         // source vertex


    public DepthFirstPaths(Graph G, int s)
    {
        this.s = s;
        edgeTo = new int[G.V()];
        marked = new bool[G.V()];
        validateVertex(s);
        dfs(G, s);
    }

    // depth first search from v
    private void dfs(Graph G, int v)
    {
        marked[v] = true;
        foreach (int w in G.Adj(v))
        {
            if (!marked[w])
            {
                edgeTo[w] = v;
                dfs(G, w);
            }
        }
    }

   

    public bool hasPathTo(int v)
    {
        validateVertex(v);
        return marked[v];
    }


    public Stack<int> pathTo(int v)
    {
        validateVertex(v);
        if (!hasPathTo(v)) return null;
        Stack<int> path = new Stack<int>();
        for (int x = v; x != s; x = edgeTo[x])
        {
            path.push(x);
        }
          

        path.push(s);
        return path;
    }

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

   

    

}
