using UnityEngine;
using System.Collections.Generic;
using System;
using System.Diagnostics;


public class ThreeSum : MonoBehaviour {

    public TextAsset text;
    //List<int> arrayList=new List<int>();
   
    System.Diagnostics.Stopwatch watch = new Stopwatch();

    void Start () {
        watch.Start(); //  开始监视代码运行时间

        //  需要测试的代码 ....
        int[] a= ReadAllInts(text);
        int count = Count(a);
        watch.Stop(); //  停止监视      
        print("elapsed time = " + watch.Elapsed);
        //print(count);
    }

    public static int[] ReadAllInts(TextAsset txt) //读取txt文件每一行
    {        
        string[] str= txt.text.Split('\n');
        int[] a = new int[str.Length];
        for (int i=0;i<str.Length;i++ )
        {
            a[i] = int.Parse(str[i]);
        }

        return a;
    }



    /**
     * Prints to standard output the (i, j, k) with {@code i < j < k}
     * such that {@code a[i] + a[j] + a[k] == 0}.
     *
     * @param a the array of integers
     */
    public static void printAll(int[] a)
    {
        int n = a.Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    if (a[i] + a[j] + a[k] == 0)
                    {
                        print(a[i] + " " + a[j] + " " + a[k]);
                    }
                }
            }
        }
    }


    /**
      * Returns the number of triples (i, j, k) with {@code i < j < k}
      * such that {@code a[i] + a[j] + a[k] == 0}.
      *
      * @param  a the array of integers
      * @return the number of triples (i, j, k) with {@code i < j < k}
      *         such that {@code a[i] + a[j] + a[k] == 0}
      */
    public static int Count(int[] a)
    {
        int n = a.Length;
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    if (a[i] + a[j] + a[k] == 0)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

}
