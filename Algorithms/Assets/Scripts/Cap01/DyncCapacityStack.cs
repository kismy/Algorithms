using UnityEngine;
using System.Collections;

//动态规划内存空间，使之既可以保存所有元素，有不至于浪费过多空间——实现方法是把旧的数组栈保存到新的数组中，销毁旧的数组
public class DyncCapacityStack<Item> : MonoBehaviour {

    public Item[] capacity=null;  //数组大小
    public int N; //栈的大小

    public DyncCapacityStack(int cap)
    {
        capacity = new Item[cap];
    }

    public bool IsEmpty()
    {
        return N == 0;
    }

    public int Size()
    {
        return N;
    }

    private void Resize(int newSize)
    {
        Item[] temp = new Item[newSize];
        for (int i = 0; i < N; i++)
        {
            temp[i] = capacity[i];
        }
        capacity = temp;       
    }

    public void Push(Item item) //检查数组是否太小，如果栈的大小N==数组大小capacity.length
    {
        if (N == capacity.Length) Resize(capacity.Length * 2);
        capacity[N++] = item;      
    }

    public Item POP()
    {
        Item temp = capacity[--N];
        capacity[N] = capacity[N+1];//避免对象游离 ,等同于 capacity[N] = null;  这样写是因为不支持capacity[i] =null这样的赋值
        if (N > 0 && N == capacity.Length / 4) Resize(capacity.Length / 2);  //栈永远不会溢出，使用率不会低于1/4        
        return temp;
    }
}
