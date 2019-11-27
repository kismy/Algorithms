using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

    private  string name;     // counter name
    private int count = 0;         // current value
    public Counter(string id)
    {
        name = id;
    }
    
    public void increment()
    {
        count++;
    }

    public int tally()
    {
        return count;
    }

    public string toString()
    {
        return name+":"+count ;
    }


    public int compareTo(Counter that)
    {
        if (this.count < that.count) return -1;
        else if (this.count > that.count) return +1;
        else return 0;
    }

    public static void main(int n, int trials)
    {
      

        // create n counters
        Counter[] hits = new Counter[n];
        for (int i = 0; i < n; i++)
        {
            hits[i] = new Counter("counter" + i);
        }

        // increment trials counters at random
        for (int t = 0; t < trials; t++)
        {
            hits[Random.Range(0,n)].increment();
        }

        // print results
        for (int i = 0; i < n; i++)
        {
            print(hits[i].toString());
        }
    }

    void Start()
    {
        main(5,8);
    }
}
