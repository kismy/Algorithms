using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//领接表数组
public class MyTest335 : MonoBehaviour {
    List<List<int>> listofList=new List<List<int>>();
    List<Bag<int>> bagList=new List<Bag<int>>();
    Bag<int>[] myBags = new Bag<int>[99];

    Dictionary<string, Bag<string>> dic = new Dictionary<string, Bag<string>>();
    Hashtable hashtableADJ = new Hashtable();
    HashSet<Bag<int>> hashSet = new HashSet<Bag<int>>();



    void Start () {

        listofList.Add(new List<int>());
        listofList[0].Add(1001);
        listofList[0].Add(1002);

        bagList.Add(new Bag<int>());
        bagList[0].Add(21);
        bagList[0].Add(23);
        bagList[0].Add(567);

        myBags = new Bag<int>[99];
        myBags[0].Add(1000);
        myBags[98].Add(1001);
        myBags[98].Add(1002);
        myBags[98].Add(1003);



    }

}
