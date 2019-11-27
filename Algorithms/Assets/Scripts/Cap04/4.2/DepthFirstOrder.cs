using UnityEngine;
using System.Collections;

public class DepthFirstOrder : MonoBehaviour {


    public TextAsset txt;
	void Start () {
       
        Digraph G = new Digraph(txt);

        DepthFirstOrder dfs = new DepthFirstOrder(G);
        print("   v  pre post");
        print("--------------");
        string str = null;
        for (int v = 0; v < G.V(); v++)
        {
            str += (v+" "+ dfs.Pre(v)+" "+ dfs.Post(v)+"\n");
            
        }
        print(str);

        str = "Preorder:  ";
       
        foreach (int v in dfs.Pre())
        {
            str+=(v + " ");
        }
       print(str);

        str = "Postorder: ";
       
        foreach (int v in dfs.Post())
        {
            str+=(v + " ");
        }
       print(str);

        str = "Reverse postorder: ";
       
        foreach (int v in dfs.reversePost())
        {
            str+=(v + " ");
        }
       print(str);
    }

    private bool[] marked;          // marked[v] = has v been marked in dfs?
    private int[] pre;                 // pre[v]    = preorder  number of v
    private int[] post;                // post[v]   = postorder number of v
    private Queue<int> preorder;   // vertices in preorder
    private Queue<int> postorder;  // vertices in postorder
    private int preCounter;            // counter or preorder numbering
    private int postCounter;           // counter for postorder numbering

 
    public DepthFirstOrder(Digraph G)
    {
        pre = new int[G.V()];
        post = new int[G.V()];
        postorder = new Queue<int>();
        preorder = new Queue<int>();
        marked = new bool[G.V()];
        for (int v = 0; v < G.V(); v++)
            if (!marked[v]) dfs(G, v);
    }

   
    public DepthFirstOrder(EdgeWeightedDigraph G)
    {
        pre = new int[G.V()];
        post = new int[G.V()];
        postorder = new Queue<int>();
        preorder = new Queue<int>();
        marked = new bool[G.V()];
        for (int v = 0; v < G.V(); v++)
            if (!marked[v]) dfs(G, v);
    }

   
    private void dfs(Digraph G, int v)
    {
        marked[v] = true;
        pre[v] = preCounter++;
        preorder.Enqueue(v);
        foreach (int w in G.Adj(v))
        {
            if (!marked[w])
            {
                dfs(G, w);
            }
        }
        postorder.Enqueue(v);
        post[v] = postCounter++;
    }

    
    private void dfs(EdgeWeightedDigraph G, int v)
    {
        marked[v] = true;
        pre[v] = preCounter++;
        preorder.Enqueue(v);
        foreach (DirectedEdge e in G.Adj(v))
        {
            int w = e.to();
            if (!marked[w])
            {
                dfs(G, w);
            }
        }
        postorder.Enqueue(v);
        post[v] = postCounter++;
    }

   
    public int Pre(int v)
    {
        validateVertex(v);
        return pre[v];
    }

   
    public int Post(int v)
    {
        validateVertex(v);
        return post[v];
    }

    
    public Queue<int> Post()
    {
        return postorder;
    }

   
    public Queue<int> Pre()
    {
        return preorder;
    }

  
    public Stack<int> reversePost()
    {
        Stack<int> reverse = new Stack<int>();
        foreach (int v in postorder)
            reverse.push(v);
        return reverse;
    }

    
    private bool check()
    {

        // check that post(v) is consistent with post()
        int r = 0;
        foreach (int v in Post())
        {
            if (Post(v) != r)
            {
                print("post(v) and post() inconsistent");
                return false;
            }
            r++;
        }

        // check that pre(v) is consistent with pre()
        r = 0;
        foreach (int v in Pre())
        {
            if (Pre(v) != r)
            {
               print("pre(v) and pre() inconsistent");
                return false;
            }
            r++;
        }

        return true;
    }

    private void validateVertex(int v)
    {
        int V = marked.Length;
        if (v < 0 || v >= V)
            throw new System.Exception("vertex " + v + " is not between 0 and " + (V - 1));
    }

  
  
}
