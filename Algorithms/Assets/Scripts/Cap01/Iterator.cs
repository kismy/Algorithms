using System.Collections;
using System.Collections.Generic;

public class Iterator<T> : IEnumerable<T>
{
    private T[] values = new T[100];
    private int top = 0;

    public void Push(T t) { values[top++] = t; }
    public T Pop() {
        T temp = values[--top];
        values[top] = values[top+1];
        return temp; }

    // These make Stack<T> implement IEnumerable<T> allowing
    // a stack to be used in a foreach statement.
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = top; --i >= 0;)
        {
            yield return values[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Iterate from top to bottom.
    public IEnumerable<T> TopToBottom
    {
        get
        {
            // Since we implement IEnumerable<T>
            // and the default iteration is top to bottom,
            // just return the object.
            return this;
        }
    }

    // Iterate from bottom to top.
    public IEnumerable<T> BottomToTop
    {
        get
        {
            for (int i = 0; i < top; i++)
            {
                yield return values[i];
            }
        }
    }

    //A parameterized iterator that return n items from the top
    public IEnumerable<T> TopN(int n)
    {
        // in this example we return less than N if necessary 
        int j = n >= top ? 0 : top - n;

        for (int i = top; --i >= j;)
        {
            yield return values[i];
        }

    }
}