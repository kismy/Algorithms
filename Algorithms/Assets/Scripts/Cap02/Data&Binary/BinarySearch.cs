using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinarySearch : SortBase {
    int[] arraysort = new int[] { 0, 3, 5, 7, 8, 9, 11, 13, 35, 56, 67, 78, 89, 110, 234, 4556 };

    float f1 = 1;
    float f2 = 1.00000001f;  //1)f2为8位小数   2)十进制单精度浮点型的效数字位：7    3)运算结果的不确定量有效字位12      
    double d1 = 1;
    double d2 = 1.0000000000000001; //d2为16位小数  十进制双精度浮点型的效数字位：15    
    //浮点数的比较不能直接用==  ;而是引用 eps
    //eps缩写自epsilon，表示一个小量，但这个小量又要确保远大于浮点运算结果的不确定量。eps最常见的取值是1e-8左右。

    void Start() {
        // print(Search(arraysort,2));
        // Sort(arraysort);

        //int key = 5;
        //if (BinarySearch.indexOf(arraysort, key) == -1) print(key);

        
        print(f1==f2);  //true
        print(d1 == d2);   //true
        print(1e-8);
    }

    /// <summary>
    /// 二分查找：在有序的数组上查找某元素，有该值返回其在数组中索引ID，没有返回-1
    /// </summary>
    /// <param name="value"></param>
    /// <param name="array"></param>
    /// <returns></returns>
    public static int Search(int[] SortArray,int value)
    {
        int Lo = 0;
        int Ln = SortArray.Length;
        while (Lo <= Ln)
        {
            int mid = (int)(Lo + Ln) / 2;
            if (value < SortArray[mid]) Ln = mid - 1;
            else if (value > SortArray[mid]) Lo = mid + 1;
            else return mid;
        }
        return -1;
    }



   

    public static int indexOf(int[] a, int key)
    {
        int lo = 0;
        int hi = a.Length - 1;
        while (lo <= hi)
        {
            // Key is in a[lo..hi] or not present.
            int mid = lo + (hi - lo) / 2;
            if (key < a[mid]) hi = mid - 1;
            else if (key > a[mid]) lo = mid + 1;
            else return mid;
        }
        return -1;
    }
    public static int rank(int key, int[] a)
    {
        return indexOf(a, key);
    }


    //二分查找的递归实现

    public static int Rank(int[] a, int value)
    {

        return Rank(a,  value, 0,a.Length-1);
    }

    public static int Rank(int[] a, int value, int lo, int hi)
    {
        if(lo>hi)return -1;
        int mid = lo + (hi - lo) / 2;

        if (value < a[mid]) return Rank(a, value, lo, mid - 1);
        else if (value > a[mid]) return Rank(a, value, mid + 1, hi);
        else return mid;
    }


    /// <summary>
    /// 返回N！的自然对数
    /// </summary>
    /// <param name="N"></param>
    /// <returns></returns>
    public static float Ln(int N)
    {
        int value = 1;
        for (int i = 2; i <= N; i++)
        {
            value = value * i;
        }

        return Mathf.Log(value);

    }
   

}
