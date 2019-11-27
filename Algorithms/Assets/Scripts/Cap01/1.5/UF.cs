using UnityEngine;
using System.Collections;


/// <summary>
/// 压缩路径的WeightedQuickUnionUF
/// </summary>
public class UF: MonoBehaviour {

    public TextAsset text;    

    void Start () {
        int n=0; //触点数量
        ConnectedPQ[] pq = ReadAllIntPairs(text,out n);

        UF uf = new UF(n);
        for (int i=0;i< pq.Length; i++)
        {
            int p = pq[i].p;
            int q = pq[i].q;
            if (uf.connected(p, q)) continue;
            uf.union(p, q);          
        }
        print("连通分量components个数："+uf.Count());
    }


    private int[] parent;  // parent[i] = parent of i ，表示联通分量
    private int count;     // number of components
    private byte[] rank;   // rank[i] = rank of subtree rooted at i (never more than 31)


    /// <summary>
    /// 初始化：1）创建n个联通分量parent[n]，分量的某个触点i为连通分量的标识符parent[i]
    /// </summary>
    /// <param name="n"></param>
    public UF(int n)
    {
        if (n < 0) throw new System.Exception("输入参数不合法，联通分量个数必须大于0 ！");
        count = n;
        parent = new int[n];
        rank = new byte[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
    }

   
    //while循环，在路径上遇到的所有节点元素，把元素直接连接到连通分量根节点——————扁平化树
    public int find(int p)
    {
        validate(p);
        while (p != parent[p])  
        {
            parent[p] = parent[parent[p]];    // path compression by halving
            p = parent[p];
        }
        return p;
    }

  
    //联通分量的数量
    public int Count()
    {
        return count;
    }

   
    //判断p，q是否相连——判断p,q是否在同一个连通分量中
    public bool connected(int p, int q)
    {
        return find(p) == find(q);
    }

   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="q"></param>
    public void union(int p, int q)
    {
        int rootP = find(p);
        int rootQ = find(q);
        if (rootP == rootQ) return;

        // make root of smaller rank point to root of larger rank
        if (rank[rootP] < rank[rootQ]) parent[rootP] = rootQ;
        else if (rank[rootP] > rank[rootQ]) parent[rootQ] = rootP;
        else
        {
            parent[rootQ] = rootP;
            rank[rootP]++;
        }
        count--;
    }


    /// <summary>
    /// 确认p 属于 parent[0] 到 parent[n-1] 的,否则报错
    /// </summary>
    /// <param name="p"></param>
    private void validate(int p) //证实; 使合法化，使有法律效力; 使生效; 批准，确认;
    {
        int n = parent.Length;
        if (p < 0 || p >= n)
        {
            throw new System.Exception("index " + p + " is not between 0 and " + (n - 1));
        }
    }


    public static ConnectedPQ[] ReadAllIntPairs(TextAsset txt,out int n) //读取txt文件每一行
    {
        string[] str = txt.text.Split('\n');
        n = int.Parse(str[0]); //第一行表示触电数量n

        ConnectedPQ[] pq = new ConnectedPQ[str.Length-1];
        for (int i = 1; i < str.Length-1; i++) //除去第一行 ，第一行表示触电数量n
        {
            string[]str2 = str[i].Split(' ');
            if (str2.Length == 2)
            {

                pq[i].p = int.Parse(str2[0]);

                pq[i].q = int.Parse(str2[1]);

            } else
            {
                throw new System.Exception("所提供的TextAsset txt 内容不规范，第" + i+"/"+str.Length + "行整数个数不等于2");
            }

          
           
        }

     
        return pq;
    }

   public  struct ConnectedPQ
    {
      public  int p;
      public  int q;
        private ConnectedPQ(int p,int q)
        {
            this.p = p;
            this.q = q;
        }
    }

}
