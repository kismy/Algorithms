using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System;

public class LSD : MonoBehaviour {

    ////private string path=Application.dataPath+ "/Resources/algs4-data/words3.txt";
    public TextAsset textAsset;
    private const int BITS_PER_BYTE = 8;
    void Start()
    {
        //string  txt = File.ReadAllText(path);
        string txt = textAsset.text;
        string[] a = txt.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int n = a.Length;

        // check that strings have fixed length
        int w = a[0].Length;
        for (int i = 0; i < n; i++)
            Trace.Assert(a[i].Length == w, "Strings must have fixed length");

        // sort the strings
        sort(a, w);

        // print results
        for (int i = 0; i < n; i++)
            print(a[i]);
    }



    //// do not instantiate
    ////public LSD() { }

    /**  
      * Rearranges the array of W-character strings in ascending order.
      *
      * @param a the array to be sorted
      * @param w the number of characters per string
      */
    public static void sort(string[] a, int w)
    {

        int n = a.Length;
        int R = 256;   // extend ASCII alphabet size
        string[] aux = new string[n];

        for (int d = w - 1; d >= 0; d--)
        {
            // sort by key-indexed counting on dth character

            // compute frequency counts
            int[] count = new int[R + 1];
            for (int i = 0; i < n; i++)
            {
                char myChar = a[i].Substring(d, 1)[0];
                int index = (short)(myChar);
                count[index + 1]++;
            }


            // compute cumulates
            for (int r = 0; r < R; r++)
                count[r + 1] += count[r];

            // move data
            for (int i = 0; i < n; i++)
            {
                char myChar = a[i].Substring(d, 1)[0];
                int index =(short)(myChar);
                aux[count[index]++] = a[i];
            }


            // copy back
            for (int i = 0; i < n; i++)
                a[i] = aux[i];
        }
    }

    ///**
    //  * Rearranges the array of 32-bit integers in ascending order.
    //  * This is about 2-3x faster than Arrays.sort().
    //  *
    //  * @param a the array to be sorted
    //  */
    //public static void sort(int[] a)
    //{
    //    const int BITS = 32;                 // each int is 32 bits 
    //    const int R = 1 << BITS_PER_BYTE;    // each bytes is between 0 and 255
    //    const int MASK = R - 1;              // 0xFF
    //    const int w = BITS / BITS_PER_BYTE;  // each int is 4 bytes

    //    int n = a.Length;
    //    int[] aux = new int[n];

    //    for (int d = 0; d < w; d++)
    //    {

    //        // compute frequency counts
    //        int[] count = new int[R + 1];
    //        for (int i = 0; i < n; i++)
    //        {
    //            int c = (a[i] >> BITS_PER_BYTE * d) & MASK;
    //            count[c + 1]++;
    //        }

    //        // compute cumulates
    //        for (int r = 0; r < R; r++)
    //            count[r + 1] += count[r];

    //        // for most significant byte, 0x80-0xFF comes before 0x00-0x7F
    //        if (d == w - 1)
    //        {
    //            int shift1 = count[R] - count[R / 2];
    //            int shift2 = count[R / 2];
    //            for (int r = 0; r < R / 2; r++)
    //                count[r] += shift1;
    //            for (int r = R / 2; r < R; r++)
    //                count[r] -= shift2;
    //        }

    //        // move data
    //        for (int i = 0; i < n; i++)
    //        {
    //            int c = (a[i] >> BITS_PER_BYTE * d) & MASK;
    //            aux[count[c]++] = a[i];
    //        }

    //        // copy back
    //        for (int i = 0; i < n; i++)
    //            a[i] = aux[i];
    //    }
    //}



}
