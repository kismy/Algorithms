using UnityEngine;
using System.Collections;

public class BreadthFirstPaths : MonoBehaviour {
    public TextAsset txt;
	void Start () {
        Graph G = new Graph(txt);

        int s = 0;
        BreadthFirstPaths bfs = new BreadthFirstPaths(G, s);

        for (int v = 0; v < G.V(); v++)
        {
            if (bfs.hasPathTo(v))
            {
                //print("%d to %d (%d):  ", s, v, bfs.DistTo(v));
                string str = s + " to " + v +"\tdistance:"+ bfs.DistTo(v) + "\t.\t";
                foreach (int x in bfs.pathTo(v))
                {
                    if (x == s) str += x;
                    else str += ("-" + x);
                }
                print(str);
            }

            else
            {
                print(s + " to " + v + ":  not connected\n");
            }

        }
    }

    private static  int INFINITY = int.MaxValue;
    private bool[] marked;  // marked[v] = is there an s-v path
    private int[] edgeTo;      // edgeTo[v] = previous edge on shortest s-v path
    private int[] distTo;      // distTo[v] = number of edges shortest s-v path

    public BreadthFirstPaths(Graph G, int s)
    {
        marked = new bool[G.V()];
        distTo = new int[G.V()];
        edgeTo = new int[G.V()];
        validateVertex(s);
        bfs(G, s);
    }  

    public BreadthFirstPaths(Graph G, Queue<int> sources)
    {
        marked = new bool[G.V()];
        distTo = new int[G.V()];
        edgeTo = new int[G.V()];
        for (int v = 0; v < G.V(); v++)
            distTo[v] = INFINITY;
        validateVertices(sources);
        bfs(G, sources);
    }
    
    // breadth-first search from a single source
    private void bfs(Graph G, int s)
    {
        Queue<int> q = new Queue<int>();
        for (int v = 0; v < G.V(); v++)
            distTo[v] = INFINITY;
        distTo[s] = 0;
        marked[s] = true;
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

    // breadth-first search from multiple sources
    private void bfs(Graph G, Queue<int> sources)
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

    // check optimality conditions for single source
    private bool check(Graph G, int s)
    {

        // check that the distance of s = 0
        if (distTo[s] != 0)
        {
            print("distance of source " + s + " to itself = " + distTo[s]);
            return false;
        }

        // check that for each edge v-w dist[w] <= dist[v] + 1
        // provided v is reachable from s
        for (int v = 0; v < G.V(); v++)
        {
            foreach (int w in G.Adj(v))
            {
                if (hasPathTo(v) != hasPathTo(w))
                {
                    print("edge " + v + "-" + w);
                    print("hasPathTo(" + v + ") = " + hasPathTo(v));
                    print("hasPathTo(" + w + ") = " + hasPathTo(w));
                    return false;
                }
                if (hasPathTo(v) && (distTo[w] > distTo[v] + 1))
                {
                    print("edge " + v + "-" + w);
                    print("distTo[" + v + "] = " + distTo[v]);
                    print("distTo[" + w + "] = " + distTo[w]);
                    return false;
                }
            }
        }

        // check that v = edgeTo[w] satisfies distTo[w] = distTo[v] + 1
        // provided v is reachable from s
        for (int w = 0; w < G.V(); w++)
        {
            if (!hasPathTo(w) || w == s) continue;
            int v = edgeTo[w];
            if (distTo[w] != distTo[v] + 1)
            {
                print("shortest path edge " + v + "-" + w);
                print("distTo[" + v + "] = " + distTo[v]);
                print("distTo[" + w + "] = " + distTo[w]);
                return false;
            }
        }

        return true;
    }

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

    // throw an IllegalArgumentException unless {@code 0 <= v < V}
    private void validateVertices(Queue<int> vertices)
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
