using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSort : SortBase {

	// Use this for initialization
	void Start () {
        UnSort(array);
        Show(array);
        shellSort(array);
        Show(array);


    }
    //public void shellSort(int[] list)
    //{
    //    int gap = list.Length / 2;
    //    while (1 <= gap)
    //    {
    //        // 把距离为 gap 的元素编为一个组，扫描所有组
    //        for (int i = gap; i < list.Length; i++)
    //        {
    //            int j = 0;
    //            int temp = list[i];
    //            // 对距离为 gap 的元素组进行排序
    //            for (j = i - gap; j >= 0 && temp < list[j]; j = j - gap)
    //            {
    //                list[j + gap] = list[j];
    //            }
    //            list[j + gap] = temp;
    //        }
    //        gap = gap / 2; // 减小增量
    //    }

    //}

    public void shellSort(int[] list)
    {
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

    }
}
