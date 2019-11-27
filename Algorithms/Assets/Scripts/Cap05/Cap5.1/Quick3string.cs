using UnityEngine;
using System.Diagnostics;
using System;
using System.Collections;


public class Quick3string : MonoBehaviour {

    public TextAsset textAsset;

    void Start () {
        string txt = textAsset.text;
        string[] a = txt.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int n = a.Length;

        // sort the strings
        sort(a);

        // print the results
        for (int i = 0; i < n; i++)
            print(a[i]);
    }

    public  const int CUTOFF = 15;   // cutoff to insertion sort

    // do not instantiate
    private Quick3string() { }

    /**  
     * Rearranges the array of strings in ascending order.
     *
     * @param a the array to be sorted
     */
    public static void sort(string[] a)
    {
        //a= KismySTD.RandomShuffle.Shuffle(a);
        sort(a, 0, a.Length - 1, 0);
        Trace.Assert( isSorted(a));
    }

    // return the dth character of s, -1 if d = length of s
    private static int charAt(string s, int d)
    {
        Trace.Assert( d >= 0 && d <= s.Length);
        if (d == s.Length) return -1;
        return s[d];
    }


    // 3-way string quicksort a[lo..hi] starting at dth character
    private static void sort(string[] a, int lo, int hi, int d)
    {

        // cutoff to insertion sort for small subarrays
        if (hi <= lo + CUTOFF)
        {
            insertion(a, lo, hi, d);
            return;
        }

        int lt = lo, gt = hi;
        int v = charAt(a[lo], d);
        int i = lo + 1;
        while (i <= gt)
        {
            int t = charAt(a[i], d);
            if (t < v) exch(a, lt++, i++);
            else if (t > v) exch(a, i, gt--);
            else i++;
        }

        // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi]. 
        sort(a, lo, lt - 1, d);
        if (v >= 0) sort(a, lt, gt, d + 1);
        sort(a, gt + 1, hi, d);
    }

    // sort from a[lo] to a[hi], starting at the dth character
    private static void insertion(string[] a, int lo, int hi, int d)
    {
        for (int i = lo; i <= hi; i++)
            for (int j = i; j > lo && less(a[j], a[j - 1], d); j--)
                exch(a, j, j - 1);
    }

    // exchange a[i] and a[j]
    private static void exch(string[] a, int i, int j)
    {
        string temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }

    // is v less than w, starting at character d
    // DEPRECATED BECAUSE OF SLOW SUBSTRING EXTRACTION IN JAVA 7
    // private static boolean less(String v, String w, int d) {
    //    assert v.substring(0, d).equals(w.substring(0, d));
    //    return v.substring(d).compareTo(w.substring(d)) < 0; 
    // }

    // is v less than w, starting at character d
    private static bool less(string v, string w, int d)
    {
        Trace.Assert( v.Substring(0, d).Equals(w.Substring(0, d)));
        for (int i = d; i < Mathf.Min(v.Length, w.Length); i++)
        {
            if (v[i] < w[i]) return true;
            if (v[i] > w[i]) return false;
        }
        return v.Length < w.Length;
    }

    // is the array sorted
    private static bool isSorted(string[] a)
    {
        for (int i = 1; i < a.Length; i++)
            if (a[i].CompareTo(a[i - 1]) < 0) return false;
        return true;
    }

}
