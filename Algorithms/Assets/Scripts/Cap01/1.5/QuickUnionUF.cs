using UnityEngine;
using System.Collections;

public class QuickUnionUF : MonoBehaviour {


	void Start () {
        //int n = StdIn.readInt();
        //QuickUnionUF uf = new QuickUnionUF(n);
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
    private int[] parent;  // parent[i] = parent of i
    private int count;     // number of components

  

    public QuickUnionUF(int n)
    {
        parent = new int[n];
        count = n;
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }
    }

    

    public int Count()
    {
        return count;
    }



    /// <summary>
    /// 找出根结点，即根连通分量
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public int find(int p)
    {
        validate(p);
        while (p != parent[p])
            p = parent[p];
        return p;
    }

   

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



    /// <summary>
    /// 把根节点相连，性能搜限制因素：树的深度太深， find(int p)访问次数太多
    /// </summary>
    /// <param name="p"></param>
    /// <param name="q"></param>
    public void union(int p, int q)
    {
        int rootP = find(p);
        int rootQ = find(q);
        if (rootP == rootQ) return;
        parent[rootP] = rootQ;
        count--;
    }

   

   

}