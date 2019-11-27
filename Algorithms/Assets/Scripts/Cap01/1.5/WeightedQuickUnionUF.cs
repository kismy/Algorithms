using UnityEngine;
using System.Collections;

public class WeightedQuickUnionUF : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //int n = StdIn.readInt();
        //WeightedQuickUnionUF uf = new WeightedQuickUnionUF(n);
        //while (!StdIn.isEmpty())
        //{
        //    int p = StdIn.readInt();
        //    int q = StdIn.readInt();
        //    if (uf.connected(p, q)) continue;
        //    uf.union(p, q);
        //    StdOut.println(p + " " + q);
        //}
        //StdOut.println(uf.count() + " components");
    }

    private int[] parent;   // parent[i] = parent of i
    private int[] size;     // size[i] = number of sites in subtree rooted at i
    private int count;      // number of components

  

    public WeightedQuickUnionUF(int n)
    {
        count = n;
        parent = new int[n];
        size = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
            size[i] = 1;
        }
    }

   

    public int Count()
    {
        return count;
    }

  

    public int find(int p)
    {
        validate(p);
        while (p != parent[p])
            p = parent[p];
        return p;
    }

    // validate that p is a valid index
    private void validate(int p)
    {
        int n = parent.Length;
        if (p < 0 || p >= n)
        {
            throw new System.Exception("index " + p + " is not between 0 and " + (n - 1));
        }
    }

   

    public bool connected(int p, int q)
    {
        return find(p) == find(q);
    }


    public void union(int p, int q)
    {
        int rootP = find(p);
        int rootQ = find(q);
        if (rootP == rootQ) return;

        // make smaller root point to larger one  将小树的根结点连接到大树的根结点
        if (size[rootP] < size[rootQ])
        {
            parent[rootP] = rootQ;
            size[rootQ] += size[rootP];
        }
        else
        {
            parent[rootQ] = rootP;
            size[rootP] += size[rootQ];
        }
        count--;
    }



   
}
