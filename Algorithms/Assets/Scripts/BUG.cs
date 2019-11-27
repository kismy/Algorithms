using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BUG : MonoBehaviour {

	List<int> list=new List<int>(new int[] {1,2,3,4,5});
	void Start ()
	{


        foreach (int item in list)
        {
            print(item * item);
            list.Remove(item);
        }

        //for (int i=0;i<4; i++)
        //{
        //    print(list[i]);
        //    list.Remove(i);
        //}

    }
	
	
}
