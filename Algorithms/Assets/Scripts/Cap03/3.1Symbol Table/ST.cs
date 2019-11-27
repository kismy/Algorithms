using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ST<Key,Value> : MonoBehaviour,IEnumerable {

	// Use this for initialization
	void Start () {
        //string[] args = { };
        //ST<string, int> st = new ST<string, int>();
        //for (int i = 0; !StdIn.isEmpty(); i++)
        //{
        //    string key = StdIn.readString();
        //    st.put(key, i);
        //}
        //foreach (string s in st.keys())
        //    print(s + " " + st.get(s));
    }

    private Dictionary<Key, Value> st;

   

    public ST()
    {
        st = new Dictionary<Key, Value>();
    }



    public Value GetValue(Key key)
    {
        if (key == null) throw new System.Exception("called get() with null key");
        Value v ;
        st.TryGetValue(key,out v);
        return v;
    }

    

    public void put(Key key, Value val)
    {
        if (key == null) throw new System.Exception("called put() with null key");
        if (val == null) st.Remove(key);
        else st.Add(key, val);
    }

    

    public void delete(Key key)
    {
        if (key == null) throw new System.Exception("called delete() with null key");
        st.Remove(key);
    }

   

    public bool contains(Key key)
    {
        if (key == null) throw new System.Exception("called contains() with null key");
        return st.ContainsKey(key);
    }


    public int size()
    {
        return st.Count;
    }

  

    public bool isEmpty()
    {
        return size() == 0;
    }


    //public Key min()
    //{
    //    if (isEmpty()) throw new System.Exception("called min() with empty symbol table");
    //    return st.firstKey();
    //}



    //public Key max()
    //{
    //    if (isEmpty()) throw new System.Exception("called max() with empty symbol table");
    //    return st.lastKey();
    //}



    //public Key ceiling(Key key)
    //{
    //    if (key == null) throw new System.Exception("called ceiling() with null key");
    //    Key k = st.ceilingKey(key);
    //    if (k == null) throw new System.Exception("all keys are less than " + key);
    //    return k;
    //}



    //public Key floor(Key key)
    //{
    //    if (key == null) throw new System.Exception("called floor() with null key");
    //    Key k = st.floorKey(key);
    //    if (k == null) throw new System.Exception("all keys are greater than " + key);
    //    return k;
    //}



    public IEnumerable keys()
    {      
        foreach (Key key in st.Keys) yield return key;        
    }



    #region 迭代器...................................
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Key> GetEnumerator()  //todo
    {
        Dictionary<Key, Value> stCopy = st;

        while (stCopy != null)
        {
            yield return (Key)(object)null;
        }
    }



    #endregion
}
