using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class MergeBU : SortBase {

	void Start () {
        Sort(array);
        Show(array);
    }



    public override void Sort(int[] a)
    {
        int n = a.Length;
        int[] aux = new int[n];
        for (int len = 1; len < n; len *= 2)
        {
            for (int lo = 0; lo < n - len; lo += len + len)
            {
                int mid = lo + len - 1;
                int hi = System.Math.Min(lo + len + len - 1, n - 1);
                merge(a, aux, lo, mid, hi);
            }
        }
        Trace.Assert(Sorted(a) == false, "数组a无序");

    }

    private void merge(int[] a, int[] aux, int lo, int mid, int hi)
    {

        // copy to aux[]
        for (int k = lo; k <= hi; k++)
        {
            aux[k] = a[k];
        }

        // merge back to a[]
        int i = lo, j = mid + 1;
        for (int k = lo; k <= hi; k++)
        {
            if (i > mid) a[k] = aux[j++];  // this copying is unneccessary
            else if (j > hi) a[k] = aux[i++];
            else if (Less(aux[j], aux[i])) a[k] = aux[j++];
            else a[k] = aux[i++];
        }

    }
}
