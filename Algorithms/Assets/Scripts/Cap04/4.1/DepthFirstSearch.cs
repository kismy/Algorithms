using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class DepthFirstSearch : MonoBehaviour {

    public TextAsset txt;
	// Use this for initialization
	void Start () {
        Graph G = new Graph(txt);
        //int s = Random.Range(0, int.Parse(txt.text.Split('\n')[0]));
        int s = 0;
        DepthFirstSearch search = new DepthFirstSearch(G, s);

        string str = "与顶点s连接的所有顶点如下：\n";
        for (int v = 0; v < G.V(); v++)
        {
            if (search.Marked(v)) str += (v+"\t");
            if (v % 10 == 9) str += "\n";
        }
        print(str);
        

        if (search.Count() != G.V()) print("非连通图");
        else print("连通图");


        foreach (int item in search.pathOfSearch)
        {
            print(item);
        }

    }
  

    private bool[] marked;    // marked[v] = is there an s-v path?
    private int count;           // number of vertices connected to s
    public List<int> pathOfSearch = new List<int>();  //用于记录路过的顶点



    public DepthFirstSearch(Graph G, int s)
    {
        marked= new bool[G.V()];
        validateVertex(s);
        dfs(G, s);
    }

    // depth first search from v
    private void dfs(Graph G, int v)
    {
        count++;
        marked[v] = true;
        pathOfSearch.Add(v);  //虽然不一定是最短路径
        foreach (int w in G.Adj(v))
        {
            if (!marked[w])
            {
                dfs(G, w);   //深度优先递归搜索
            }
        }
    }



    public bool Marked(int v)
    {
        validateVertex(v);
        return marked[v];
    }

 

    public int Count()
    {
        return count;
    }

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }



   
}
