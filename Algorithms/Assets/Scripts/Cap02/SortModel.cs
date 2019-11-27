using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SortModel
{
 
 void Sort(int[] array);
 void Sort(List<int> list);
 void Sort(List<int> list,int key);
    
 void Inverse(int[] array);
 void UnSort(int[] array);
 bool Less(int a, int b);
 void Exch( int[] array, int i, int j);
 void Show(int[] array);
 void Show(List<int> list);

    bool IsUpSorted(int[] array);
  
}
