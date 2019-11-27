using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(Find("abcdefabcdefabc"));
	}

    /// <summary>
    /// 时间复杂度O(n)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    char Find(string s)
    {
        if (string.IsNullOrEmpty(s))
            return ' ';

        Dictionary<char, int> dic = new Dictionary<char, int>();
        for (int i = 0; i < s.Length; i++)
        {
            if (dic.ContainsKey(s[i]))
            {
                int count = dic[s[i]];
                dic[s[i]] = count+1;
            }
            else
            {
                dic.Add(s[i],1);
            }
        }
        foreach (var key in dic.Keys)
        {
            if (dic[key] == 2) return key;
        }
        return 'A';
    }


    int[] result;
    int Steps(int n)
    {
        if (n <= 2)
            return n;
        if (result[n] != 0)
           return result[n];
        return Steps(n - 1) + Steps(n-2);
    }
}
