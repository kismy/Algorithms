using UnityEngine;
using System.Collections;

public class Multiway : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string[] args =new string[] { "A", "C", "D", "B" }; 
        int n = args.Length;
        int[] streams = new int[n];
        for (int i = 0; i < n; i++)
            streams[i] = int.Parse(args[i]);
        merge(streams);
    }

    private Multiway() { }

    private static void merge(int[] streams)
    {
        
        int n = streams.Length;
       
        IndexMinPQ<string> pq = new IndexMinPQ<string>(n);
        for (int i = 0; i < n; i++)
            if (!streams[i].Equals(null))
                pq.insert(i, streams[i].ToString());

        // Extract and print min and read next from its stream. 
        while (!pq.isEmpty())
        {
            print(pq.minKey() + " ");
            int i = pq.delMin();
            if (!streams[i].Equals(null))
                pq.insert(i, streams[i].ToString());
        }
      
    }


    void test()
    {
     
    }




}

