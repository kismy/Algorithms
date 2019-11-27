using UnityEngine;
using System.Collections;

//时间复杂度：平方级别——union（）需要遍历所有的id[i]导致性能下降
public class QuickFindUF : MonoBehaviour {

	
	void Start () {
        //int n = StdIn.readInt();
        //QuickFindUF uf = new QuickFindUF(n);
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

    private int[] id;    // id[i] = component identifier of i
    private int count;   // number of components

  

    public QuickFindUF(int n)
    {
        count = n;
        id = new int[n];
        for (int i = 0; i < n; i++)
            id[i] = i;
    }


    public int Count()
    {
        return count;
    }

   

    public int find(int p)
    {
        validate(p);
        return id[p];
    }

    

    private void validate(int p)
    {
        int n = id.Length;
        if (p < 0 || p >= n)
        {
            throw new System.Exception("index " + p + " is not between 0 and " + (n - 1));
        }
    }

   

    public bool connected(int p, int q)
    {
        validate(p);
        validate(q);
        return id[p] == id[q];
    }


    /// <summary>
    /// 性能平方级别：union（）需要遍历所有的Parent[i]导致性能下降
    /// </summary>
    /// <param name="p"></param>
    /// <param name="q"></param>
    public void union(int p, int q)
    {
        validate(p);
        validate(q);
        int pID = id[p];   // needed for correctness
        int qID = id[q];   // to reduce the number of array accesses

        // p and q are already in the same component
        if (pID == qID) return;

        for (int i = 0; i < id.Length; i++)
            if (id[i] == pID) id[i] = qID;
        count--;
    }

  

  

}
