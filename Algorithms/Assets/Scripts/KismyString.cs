using UnityEngine;
using System;
using System.Collections;

public class KismyString : MonoBehaviour {
    
	
	void Start () {
        /*
        //测试string实例方法Split（）......................................................返回一个字符串中，某字符为“分隔符”的数组
        /*那么执行结果就是字符串数组floatArray={"abc","d","efg"}
         * string a = 123,456,789;
           string[] str = a.Split('，');
         * 这样运算以后，str数组里有3个元素
          str[0] == "123";
          str[1] == "456";
          str[2] == "789";
          这样就明白这个用法了吧……希望采纳
       
        string str = "abc/d@efg";
        char[] separator = { '/', '@' };
        string[] strArray = str.Split(separator);
        foreach (string sINstrArray in strArray) print(sINstrArray);
        */


        /*
        //测试string实例方法Trim()，删除一个字符串中，任何你不想要的首位字符；在括号内,可以加上任何你想要去掉的前尾字符.......................................................
        string s = " *abc# ";
        s = s.Trim();           print(s);       //s = "*abc#";  删除字符串首位空格； 如果想要去掉字符串中所有空格,请用s =s.Replace(" ","");
        s = s.Trim( '#','*');   print(s);      //s = "abc";     s.Trim( '#','*');中'#','*'位置可以互换


        s = "123 456 789";
        print(GetWord(s, 0));
        print(GetWord(s, 1));
        print(GetWord(s, 2));
        */


        /*

        //注意string类型变量的存储是没有上限的，想存多少内容就可以存多少
        string mystring1 = "这是一句中文";
        string mystring2 = "123456789";
        string mystring3 = "abcdefg";


        
        //字符串的大小写转换
        string myName = "jack";
        myName=myName.ToUpper();
        if (myName == "JACK") print("该用户已存在");
        else print("你的名字很有个性!");


        string mystr = "this is an example of conversation on string.我是中国人。";
        mystr = mystr.ToUpper();
        print(mystr);
        */


        /*
        //字符串格式化

        string s1= "2100.33";
        
        s1 = string.Format("默认格式:{0}\n"+"使用C格式:{1:C}\n" + "使用D格式:{0:D}\n" + "使用E格式:{1:E}\n" + "使用F格式:{1:F}\n" + "使用G格式:{1:G}\n" + "使用N格式:{1:N}\n" + "使用P格式:{1:P}\n" + "使用X格式:{0:}\n", 9998, 9998.99);
        print(s1);

        for (int i = 1; i<= 50; i++)
        {
            s1 = string.Format("{0:D3}",i);
            print(s1);

        }
        */


        /*
        string[] str = new string[] { "这是一句中文", "123456789", "abcdefg" };

        string mystring1 = "这是一句中文";
        string mystring2 = "123456789";
        string mystring3 = "abcdefg";

        string stri= string.Join("-",str);
        print(stri);
        */




        //StringArraySparateString();
        //CharArraySparateString();
        f();

    }

    //参数可以为多个int型，例如GetWord("This is a dag.", 2);返回"a"
    //而GetWord("This is a dag.", 1, 2);则返回"is a"
    //GetWord("This is a dag.", 0, 2);返回"This a"
    public static string GetWord(string s, int n)
    {
        string[] str = s.Split(' '); //返回一个字符串中，某字符为“分隔符”的数组
        return str[n];  //删除字符串首位空格；
    }


    /// <summary>
    /// 1）用字符串分割字符串
    /// 2）StringSplitOptions.RemoveEmptyEntries)
    /// </summary>
    void StringArraySparateString()
    {
        string str = "12   34   56   ||   78   910   1011  -   1213   1415   1617";

        //StringSplitOptions.RemoveEmptyEntries 的命名空间 using System;
        string[] separator = new string[] { " ", "||", "-" };
        string[] sArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);  

        foreach (string e in sArray)
        {
            print(e);
        }
    }

    /// <summary>
    /// 1）用字符分割字符串
    /// 2）StringSplitOptions.RemoveEmptyEntries)
    /// </summary>
    void CharArraySparateString()
    {
        string str = "12   34   56   ||   78   910   1011  -   1213   1415   1617";
        char[] separator = new char[] { ' ', '|', '-' };
        //StringSplitOptions.RemoveEmptyEntries 的命名空间 using System;
        string[] sArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        foreach (string e in sArray)
        {
            print(e);
        }
    }


    //提取字符串中的第i个字符开始的长度为j的字符串；
    void f()
    {
        string str = " 3 42";
        string substr01 = str.Substring(0, 2);

        string substr02 = str.Substring(3, 2);

        print(substr01);
        print(substr02);


    }

}

   




   






