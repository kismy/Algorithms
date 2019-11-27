using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Heap : SortBase {
    public static Comparer<int> comparator;  // optional comparator

    void Start () {
        int[] a = { 6,3,8,1, 2, 7, 4, 9 };
        Heap.sort(a);
        Show(a);
    }

    private Heap() {
        comparator = Comparer<int>.Default;
    }
    public static void sort(int[] pq)
    {
        int n = pq.Length;
        for (int k = n / 2; k >= 1; k--) //构造堆,其中k为二叉树有子树的节点ID，注意，ID从1开始
            sink(pq, k, n);


        while (n > 1)//构造堆有序
        {
            exch(pq, 1, n--);
            sink(pq, 1, n);
        }
    }

    /***************************************************************************
     * Helper functions to restore the heap invariant.
     ***************************************************************************/
    private static void sink(int[] pq, int k, int n)
    {
        while (2 * k <= n)
        {
            int j = 2 * k;

            if (j < n && less(pq, j, j + 1)) j++;  //比较子节点哪一个更大，获取最大索引
            if (less(pq, k, j) == false) break; //父节点k >子节点j，return.
            else
            {
                exch(pq, k, j); //k 下沉，j上浮
                k = j;
            }
        }
    }

    /***************************************************************************
     * Helper functions for comparisons and swaps.
     * Indices are "off-by-one" to support 1-based indexing.
     ***************************************************************************/
    private static bool less(int[] pq, int i, int j)
    {
        return Heap.comparator.Compare(pq[i - 1], pq[j - 1]) < 0;
    }

    private static void exch(int[] pq, int i, int j)
    {
        int swap = pq[i - 1];
        pq[i - 1] = pq[j - 1];
        pq[j - 1] = swap;
    }

  
}
