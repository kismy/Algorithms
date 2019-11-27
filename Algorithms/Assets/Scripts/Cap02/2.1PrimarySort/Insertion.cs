using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insertion : SortBase {


    void Start()
    {
       
        Sort(array);
        Show(array);

    }



    public override  void Sort(int[] array)
    {
       
        int insertID = -1;

        int lo = 0;
        int mid = 0;
        int lt =array.Length - 1;

        for (int i=1;i<array.Length;i++)
        {
           

            //二分查找insertID
            while (lo <= lt)
            {
                mid =(lo + lt) / 2;
                if (array[mid] < array[i] && array[i] <= array[mid + 1]) insertID= mid + 1;

                if (array[i] < array[mid]) lt = mid - 1;
                else if (array[i] > array[mid]) lo = mid + 1;
                else insertID= mid;
            }

            //把arra[i]插入到insertID的位置，并使之有序
            for (int j = insertID; j < i; j++)
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }


        }


    }


  

  

}
