using UnityEngine;
using System.Collections;

public class Quick : SortBase
{
    public int[] myArray;

    
    void Start()
    {
        myArray = new int[] { 99,98, 7, 5, 2, 11, 98, 81, 17, 23, 98, 36, 256, 98, 13, 1024, 110, 98, 120, 49, 512, 3 };
        Show(myArray);
        QuickSort(myArray, 0, myArray.Length - 1);
        //PartSort(myArray, 0, j[0]);
        //PartSort(myArray, j[1], myArray.Length - 1);
        Show(myArray);

    }


    /// <summary>
    /// 3取样切分快速排序——提高切分元素在尽量在数组中间的概率，因为快速排序中如果切分元素越靠近数组最小值和最大值，效率越低
    /// </summary>
    /// <param name="array"></param>
    /// <param name="lo"></param>
    /// <param name="hi"></param>
    /// <returns></returns>
    private int Partition3elements(int[] array,int lo,int hi)
    {
        int i = lo; int j =hi+1;

        //三取样：对前三个元素排序然后取中值。
        PartSort(array, 0, 2);
        int compareValue = array[1];
        Exch(array, 0, 1);

        while (true)
        {
            while (Less(array[++i], compareValue)) if (i ==hi) break; //从左边遍历，找到第一个大于compareValue的array[i]
            while (Less(compareValue, array[--j])) if (j ==lo) break;//从右边遍历，找到第一个小于compareValue的array[j]
            if (i >= j) break; //退出循环
            Exch(array, i, j);  //把大于compareValue的array[i]和小于compareValue的array[j]交换位置
        }
            Exch(array, lo, j);  //把切分元素放在左半部分的最右边

        print("切分元素值：" + compareValue + "   切分ID：" + j);
        return j;
    }

    /// <summary>
    /// 3向切分快速排序——对于数组中重复元素多的情况下效率高
    /// </summary>
    /// <param name="array"></param>
    /// <param name="lo"></param>
    /// <param name="hi"></param>
    /// <returns></returns>
    private int[] Partition3Way(int[] array, int lo, int hi)
    {
        if (hi <= lo) return new int[2] { 0,0};
        int lt = lo;  int i = lo + 1; int gt = hi;

        int compareValue = array[lo];

        while (i<=gt)
        {
            //不能用Less(array[i], compareValue)，因为这只能返回返回bool array[i]<compareValue  ,无法判断等于的情况
            int cmp = array[i].CompareTo(compareValue);  //array[i]>compareValue 返回1     array[i]=compareValue 返回0   array[i]<compareValue 返回-1

            if (cmp<0) Exch(array, lt++, i++);  //
            else if (cmp>0) Exch(array, i, gt--); //
            else i++;

        }// 现在 array[lo...lt-1]  <  compareValue =array[lt...gt]  <  array[gt+1...hi] 

        print("切分元素值：" + compareValue + "   左界切分ID：" + (lt - 1) + "   右界切分ID：" + (gt + 1));
        return new int[2] { lt-1, gt+1 }; 
    }



    /// <summary>
    /// 集合3切分和3取样的快速排序
    /// </summary>
    /// <param name="array"></param>
    /// <param name="lo"></param>
    /// <param name="hi"></param>
    /// <returns></returns>
    private int[] Partition3Way3element(int[] array, int lo, int hi)
    {
        if (hi <= lo) return new int[2] { 0, 0 };
        int lt = lo; int i = lo + 1; int gt = hi;


        //三取样：对前三个元素排序然后取中值。
        PartSort(array, 0, 2);
        int compareValue = array[1];
        Exch(array, 0, 1);

        while (i <= gt)
        {
            int cmp = array[i].CompareTo(compareValue);  //array[i]>compareValue 返回1     array[i]=compareValue 返回0   array[i]<compareValue 返回-1

            if (Less(array[i], compareValue)) Exch(array, lt++, i++);  //
            else if (Less(compareValue, array[i])) Exch(array, i, gt--); //
            else i++;

        }// 现在 array[lo...lt-1]  <  compareValue =array[lt...gt]  <  array[gt+1...hi] 

        print("切分元素值：" + compareValue + "   左界切分ID：" + (lt - 1) + "   右界切分ID：" + (gt + 1));
        return new int[2] { lt - 1, gt + 1 };
    }

    public void QuickSort(int[] a,int start,int end)
    {
        if (start >= end) return;
        int partitionValue = a[start];
        int left = start;  int right = end;

        int i = start+1;        
        while (i <=right)
        {            
            if (partitionValue>a[i])
            {
                Exch(a, left, i);
                left++;
                i++;
            }
            else if (partitionValue<a[i])
            {
                Exch(a, i, right);
                right--;               
            }
            else i++;
        } // a[start..left-1]  <  partitionValue=a[left..right]  <  a[right+1...end]


        QuickSort(a, start, left - 1);
        QuickSort(a, right+1, end);         
    }

}
