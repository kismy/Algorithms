using UnityEngine;
using System.Collections;

public class FixedCapacityStack<Item> : MonoBehaviour {

    public Item[] a=null;
    public int N;

    public FixedCapacityStack(int cap)
    {
        a = new Item[cap];
    }



    public bool IsEmpty()
    {
        return N == 0;
    }

    public int Size()
    {
        return N;
    }

    public void Push(Item item)
    {
        a[N++] = item;
    }

    public Item POP()
    {
        Item temp =a[--N];
        a[N] = a[N+1];//避免对象游离 ,等同于 capacity[N] = null;  这样写是因为不支持capacity[i] =null这样的赋值
        return temp;
    }


}
