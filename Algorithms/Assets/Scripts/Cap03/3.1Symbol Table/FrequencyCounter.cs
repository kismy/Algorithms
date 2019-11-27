using UnityEngine;
using System.Collections;

public class FrequencyCounter : MonoBehaviour {

    public TextAsset text;
    void Start () {
	
	}
    private FrequencyCounter() { }


    public  void main(string[] args)
    {
        int distinct = 0, words = 0;
        int minlen =2;
        ST<string, int> st = new ST<string, int>();
        string[] str =text.text.Split('\n', ' ');
        // compute frequency counts
        foreach (string item in str)
        {
            string key = item;
            if (key.Length < minlen) continue;
            words++;
            if (st.contains(key))
            {
                st.put(key, st.GetValue(key) + 1);
            }
            else
            {
                st.put(key, 1);
                distinct++;
            }
        }

        // find a key with the highest frequency count
        string max = "";
        st.put(max, 0);
        foreach (string word in st.keys())
        {
            if (st.GetValue(word) > st.GetValue(max))
                max = word;
        }

        print(max + " " + st.GetValue(max));
        print("distinct = " + distinct);
        print("words    = " + words);
    }


    public static string[] ReadAllWords(TextAsset txt, out int n) //读取txt文件每一行
    {
        string[] str = txt.text.Split('\n',' ');
        n = str.Length;
        return str;
    }
}

