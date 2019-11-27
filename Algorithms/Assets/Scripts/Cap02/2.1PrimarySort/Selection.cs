using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : SortBase {

	void Start () {

        //foreach (int item in arrayList) print(item);
        UnSort(array);
        Show(array);

        Sort(array);
        Show(array);

        Inverse(array);
        Show(array);
    }


    /// <summary>
    /// 选择排序：从数组的第一个数开始，遍历其右边的数，如果比第一个数小就交换二者位置; 接着到数组的第二个元素，遍历其右边的数，如果比第一个数小就交换二者位置....一直到倒数第2个数结束
    /// </summary>
    /// <param name="array"></param>
    public override void Sort(int[] array)
    {
        base.Sort(array);
        for (int i = 0; i < array.Length-1; i++)
            for (int j = i+1; j < array.Length; j++)
            {
                if (Less(array[j], array[i]))Exch(array,i,j);
            }

    }

}
