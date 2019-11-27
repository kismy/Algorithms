using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class Merge : SortBase {

    
	void Start () {
        Sort(array);
        Show(array);


    }
    public override void Sort(int[] a)
    {
        int[] aux = new int[a.Length];
        sort(a, aux, 0, a.Length - 1);
        Trace.Assert(Sorted(a)==false,"数组a无序");
    }

    // stably merge a[lo .. mid] with a[mid+1 ..hi] using aux[lo .. hi]
    private  void merge(int[] a, int[] aux, int lo, int mid, int hi)
    {
        Trace.Assert(Sorted(a, lo, mid)==false,"lo..mid无序"); 
        Trace.Assert(Sorted(a, mid + 1, hi)==false,"mid+1...hi无序");

        // copy to aux[]
        for (int k = lo; k <= hi; k++)
        {
            aux[k] = a[k];
        }

        // merge back to a[]
        int i = lo, j = mid + 1;
        for (int k = lo; k <= hi; k++)
        {
            if (i > mid) a[k] = aux[j++];
            else if (j > hi) a[k] = aux[i++];
            else if (Less(aux[j], aux[i])) a[k] = aux[j++];
            else a[k] = aux[i++];
        }


        Trace.Assert(Sorted(a, lo, hi)==false, "lo..hi无序");
    }

    // mergesort a[lo..hi] using auxiliary array aux[lo..hi]
    private  void sort(int[] a, int[] aux, int lo, int hi)
    {
        if (hi <= lo) return;
        int mid = lo + (hi - lo) / 2;
        sort(a, aux, lo, mid);
        sort(a, aux, mid + 1, hi);
        merge(a, aux, lo, mid, hi);
    }


    private  void merge(int[] a, int[] index, int[] aux, int lo, int mid, int hi)
    {

        // copy to aux[]
        for (int k = lo; k <= hi; k++)
        {
            aux[k] = index[k];
        }

        // merge back to a[]
        int i = lo, j = mid + 1;
        for (int k = lo; k <= hi; k++)
        {
            if (i > mid) index[k] = aux[j++];
            else if (j > hi) index[k] = aux[i++];
            else if (Less(a[aux[j]], a[aux[i]])) index[k] = aux[j++];
            else index[k] = aux[i++];
        }
    }


    public  int[] indexSort(int[] a)
    {
        int n = a.Length;
        int[] index = new int[n];
        for (int i = 0; i < n; i++)
            index[i] = i;

        int[] aux = new int[n];
        sort(a, index, aux, 0, n - 1);
        return index;
    }

    // mergesort a[lo..hi] using auxiliary array aux[lo..hi]
    private  void sort(int[] a, int[] index, int[] aux, int lo, int hi)
    {
        if (hi <= lo) return;
        int mid = lo + (hi - lo) / 2;
        sort(a, index, aux, lo, mid);
        sort(a, index, aux, mid + 1, hi);
        merge(a, index, aux, lo, mid, hi);
    }

}
