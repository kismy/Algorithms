using UnityEngine;
using System.Collections;

public class Quick3way : SortBase {
    public int[] a = new int[] { 98, 81, 1024, 81, 5, 7, 11, 81, 7, 23, 81, 36, 256, 13, 2, 81, 81, 2, 110, 120, 2, 49, 512, 3 };

    void Start () {

        Sort(a, 0, a.Length - 1);
        Show(a);
    }

    //private static void Sort(int[] a,int lo,int hi) {

    //    if (hi <= lo) return;
    //    int lt = lo,     i = lo + 1,     gt = hi;
    //    int v = a[lo];


    //    while (i <= gt) {
    //        if (a[i] < v) Exch(a, lt++, i++);
    //        else if (a[i] > v) Exch(a, i, gt--);
    //        else i++;
    //    }  //现在                     a[lo...lt-1]      <       v=a[lt...gt]          <           a[gt+1...hi]


    //    Sort(a,lo,lt-1);
    //    Sort(a,gt+1,hi);

    //}

    private static void Sort(int[] a, int lo, int hi)
    {

        if (hi <= lo) return;
        int lt = lo, i = lo + 1, gt = hi;
        int v = a[lo];


        while (i <= gt)
        {
            if (a[i] < v)
            {
                Exch(a, lt, i);
                lt++;
                i++;
            }
            else if (a[i] > v)
            {
                Exch(a, i, gt);
                gt--;
            }
            else i++;
        }  //现在                     a[lo...lt-1]      <       v=a[lt...gt]          <           a[gt+1...hi]


        Sort(a, lo, lt - 1);
        Sort(a, gt + 1, hi);

    }
}
