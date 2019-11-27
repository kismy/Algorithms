using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExChange : SortBase {

	// Use this for initialization
	void Start () {
        UnSort(array);
        Show(array);
        ExChangeSort(array);
        Show(array);

        //TimeCaculate(3);


    }

    public void ExChangeSort(int[] list)
    {
        bool stop = false;
        int gap = list.Length / 2;
        while (1 <= gap)
        {

            for (int i = gap; i < list.Length; i++)
            {

                if (list[i - gap] > list[i])
                {
                    int temp = list[i];
                    list[i] = list[i - gap];
                    list[i - gap] = temp;
                }

            }
            gap = gap / 2;
        }


        while (stop == false)
        {
            if (Sorted(list)) stop = true;
            //相邻交换排序
            for(int i=0;i<list.Length-1;i++)
            if (list[i] > list[i+1])
            {
                int temp = list[i];
                list[i] = list[i + 1];
                list[i +1] = temp;
            }

        }

    }


    void TimeCaculate(int n)
    {
        int s = 0;
        int i;
        int j;
        //for ( i = 1; i <= n; i++)   //循环了n*n次，当然是O(n^2)
        //    for ( j = 1; j <= n; j++)
        //        s++;

        for (i = 1; i <= n; i++)//循环了(n+n-1+n-2+...+1)≈(n^2)/2，因为时间复杂度是不考虑系数的，所以也是O(n^2)
            for (j = i; j <= n; j++)
                s++;
        print("时间复杂度："+s);
    }
}
