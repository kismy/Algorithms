using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortBase : MonoBehaviour
{
    public int[] array = new int[] { 98, 81, 1024, 81, 5, 7, 11, 81,7, 23, 81,36, 256, 13, 2,81, 81, 2, 110, 120, 2, 49, 512, 3 };


    public static void Exch(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    public void Inverse(int[] array)
    {
        //倒序排序:
        for (int i = 0; i < array.Length - 1; i++)
            for (int j = i + 1; j < array.Length; j++)
            {
                if (Less(array[i], array[j])) Exch(array, i, j);
            }
        print("倒序排序开始...");
    }

    public bool Sorted(int[] array)
    {
        bool upsort = true;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < array[i - 1])
            {
                upsort = false;
                break;
            }
        }


        bool reverseSort = true;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > array[i - 1])
            {
                reverseSort = false;
                break;
            }
        }


        return upsort|| reverseSort;
    }
    public bool Sorted(int[] array,int start,int end)
    {
        bool upsort = true;
        for (int i = start+1; i <= end; i++)
        {
            if (array[i] < array[i - 1])
            {
                upsort = false;
                break;
            }
        }


        bool reverseSort = true;
        for (int i = start+1; i <= end; i++)
        {
            if (array[i] > array[i - 1])
            {
                reverseSort = false;
                break;
            }
        }


        return upsort || reverseSort;
    }

    public bool Less(int a, int b)
    {
        return a < b;
    }

    public void Show(int[] array)
    {
        string mystring = null;
        foreach (int item in array) mystring += "   " + item;

        print(mystring + "    该数组是升序排序:" + Sorted(array));

    }



    public virtual void Sort(int[] array)
    {
        print("开始对数组排序...");
    }



    public void PartSort(int[] array, int lo, int hi)
    {

        for (int i = lo; i < hi; i++)
        {

            for (int j = i + 1; j <= hi; j++)
            {
                if (array[j] < array[i])
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;

                }

            }
        }
        print("对该输入的数组的元素从array[" + lo + "]到array[" + hi + "]排序...");
    }

    public  void UnSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int temp = UnityEngine.Random.Range(i, array.Length);
            Exch(array, i, temp);
        }
        print("打乱排序...");
    }


    /// <summary>
    /// 返回不大于以2为底的logN的最大整数,不能使用mathf库
    /// </summary>
    /// <param name="N"></param>
    /// <returns></returns>
    public static int Lg(int N)
    {
        int m = 1;
        int i = 0;
        while (m <= N)
        {
            m = m * 2;
            i++;
        }
        return i - 1;



        ////使用mathf库
        //int i = 0;
        //while (Mathf.Pow(2,i) <= N) 
        //{          
        //    i++;
        //}
        //return i - 1;
    }

    /// <summary>
    /// 计算非负整数x,y的最大公约数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int CommomDivisor(int x, int y)
    {
        if (x == 1 || y == 1) return 1;
        if (x < y) { int temp = x; x = y; y = temp; }

        if (x % y == 0) { return y; }
        else
        {
            x = x % y;
            return CommomDivisor(x, y);

        }
    }

   
}
