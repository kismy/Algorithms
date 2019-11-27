using System.Collections;
using System.Collections.Generic;
public class ResizingArrayStack<Item> : IEnumerable<Item>
{
    public Item[] values;         // array of items
    private int n;            // number of elements on stack



    //public ResizingArrayStack()
    //{
    //    values = new Item[2];
    //    n = 0;
    //}

    //public bool IsEmpty()
    //{
    //    return n == 0;
    //}

    //public int size()
    //{
    //    return n;
    //}

    //private void resize(int capacity)
    //{

    //    Item[] temp = new Item[capacity];
    //    for (int i = 0; i < n; i++)
    //    {
    //        temp[i] = values[i];
    //    }
    //    values = temp;

    //}



    //public void push(Item item)
    //{
    //    if (n == values.Length) resize(2 * values.Length);    // double size of array if necessary
    //    values[n++] = item;                            // add item
    //}



    //public Item POP()
    //{
    //    if (IsEmpty()) throw new KeyNotFoundException("Stack underflow");
    //    Item item = values[--n];
    //    values[n] = values[n+1];                              // to avoid loitering        
    //    // shrink size of array if necessary
    //    if (n > 0 && n == values.Length / 4) resize(values.Length / 2);
    //    return item;
    //}

    //public Item peek()  //查看堆栈顶部的对象，但不从堆栈中移除它。
    //{
    //    if (IsEmpty()) throw new KeyNotFoundException("Stack underflow");
    //    return values[n - 1];
    //}



    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Item> GetEnumerator()
    {
        for (int i = n; --i >= 0;)
        {
            yield return values[i];
        }
    }

    //// Iterate from top to bottom.
    //public IEnumerable<Item> TopToBottom
    //{
    //    get
    //    {
    //        // Since we implement IEnumerable<T>
    //        // and the default iteration is top to bottom,
    //        // just return the object.
    //        return this;
    //    }
    //}

    //// Iterate from bottom to top.
    //public IEnumerable<Item> BottomToTop
    //{
    //    get
    //    {
    //        for (int i = 0; i < n; i++)
    //        {
    //            yield return values[i];
    //        }
    //    }
    //}

    ////A parameterized iterator that return n items from the top
    //public IEnumerable<Item> TopN(int topN)
    //{
    //    // in this example we return less than N if necessary 
    //    int j = topN >= n ? 0 : n - topN;

    //    for (int i = n; --i >= j;)
    //    {
    //        yield return values[i];
    //    }

    //}
}
