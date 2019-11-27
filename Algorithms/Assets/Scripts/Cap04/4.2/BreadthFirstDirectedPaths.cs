using UnityEngine;
using System.Collections;

public class BreadthFirstDirectedPaths : MonoBehaviour {

    public TextAsset txt;
    void Start()
    {
       
        Digraph G = new Digraph(txt);

        int s = 2; //1  2  6
        BreadthFirstDirectedPaths bfs = new BreadthFirstDirectedPaths(G, s);

        for (int v = 0; v < G.V(); v++)
        {
            if (bfs.hasPathTo(v))
            {
                string str = (s+" to "+v+"\t(distance="+ bfs.DistTo(v)+"):\t");
               
                foreach (int x in bfs.pathTo(v))
                {
                    if (x == s) str+=x;
                    else str+=("->" + x);
                }
                print(str);
            }

            else
            {
               print(s+" to "+ v+":  not connected\n");
            }

        }
    }

    private static  int INFINITY = int.MaxValue;
    private bool[] marked;  // marked[v] = is there an s->v path?
    private int[] edgeTo;      // edgeTo[v] = last edge on shortest s->v path
    private int[] distTo;      // distTo[v] = length of shortest s->v path

  
    public BreadthFirstDirectedPaths(Digraph G, int s)
    {
        marked = new bool[G.V()];
        distTo = new int[G.V()];
        edgeTo = new int[G.V()];
        for (int v = 0; v < G.V(); v++)
            distTo[v] = INFINITY;
        validateVertex(s);
        bfs(G, s);
    }

    public BreadthFirstDirectedPaths(Digraph G, Stack<int> sources)
    {
        marked = new bool[G.V()];
        distTo = new int[G.V()];
        edgeTo = new int[G.V()];
        for (int v = 0; v < G.V(); v++)
            distTo[v] = INFINITY;
        validateVertices(sources);
        bfs(G, sources);
    }

   
    private void bfs(Digraph G, int s)
    {
        Queue<int> q = new Queue<int>();
        marked[s] = true;
        distTo[s] = 0;
        q.Enqueue(s);
        while (!q.isEmpty())
        {
            int v = q.Dequeue();
            foreach (int w in G.Adj(v))
            {
                if (!marked[w])
                {
                    edgeTo[w] = v;
                    distTo[w] = distTo[v] + 1;
                    marked[w] = true;
                    q.Enqueue(w);
                }
            }
        }
    }

  
    private void bfs(Digraph G, Stack<int> sources)
    {
        Queue<int> q = new Queue<int>();
        foreach (int s in sources)
        {
            marked[s] = true;
            distTo[s] = 0;
            q.Enqueue(s);
        }
        while (!q.isEmpty())
        {
            int v = q.Dequeue();
            foreach (int w in G.Adj(v))
            {
                if (!marked[w])
                {
                    edgeTo[w] = v;
                    distTo[w] = distTo[v] + 1;
                    marked[w] = true;
                    q.Enqueue(w);
                }
            }
        }
    }

   
    public bool hasPathTo(int v)
    {
        validateVertex(v);
        return marked[v];
    }

  
    public int DistTo(int v)
    {
        validateVertex(v);
        return distTo[v];
    }

    
    public Stack<int> pathTo(int v)
    {
        validateVertex(v);

        if (!hasPathTo(v)) return null;
        Stack<int> path = new Stack<int>();
        int x;
        for (x = v; distTo[x] != 0; x = edgeTo[x])
            path.push(x);
        path.push(x);
        return path;
    }

    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

   
    private void validateVertices(Stack<int> vertices)
    {
        if (vertices == null)
        {
            throw new System.Exception("argument is null");
        }
        int V = marked.Length;
        foreach (int v in vertices)
        {
            if (v < 0 || v >= V)
            {
                throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
            }
        }
    }


   

}
