using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


//引用类型
class RefData
{
    public Int32 a;
}
//值类型
struct ValData
{
    public Int32 a;
}
public class ValueVSRefrence : MonoBehaviour
{  


    void Start () {

        ////string虽然是引用类型，但却表现值类型特性，是因为每次在为string变量时都为变量分配新的内存地址
        //string s1 = "a";
        //string s2 = s1;
        //s1 = "b";//s2 is still "a"
        //print(s2);

        ////string和String是同一个类
        //String V1 = "a";
        //String V2 = V1;
        //V1 = "b";//V2 is still "a"
        //print(V2);

        //改变s1的值对s2没有影响。
        //这更使string看起来像值类型
        //实际上，这是运算符重载的结果，当s1被改变时，.NET在托管堆上为s1重新分配了内存。  s1 = "123"，即s1=new string("123")的简写，它的每一次赋值都会抛掉原来的对象而生成一个新的字符串对象，分配新的内存空间
        //这样的目的，是为了将做为引用类型的string实现为通常语义下的字符串。


        ////值类型按值传递
        //int myInt = 1;
        //int myInt2 = myInt;
        //myInt = 100;
        //print(myInt2);//myInt2 is still 1;


        RefData r1 = new RefData(); //在堆上分配
        r1.a = 1; //在托管堆上修改
        RefData r2 = new RefData();
        r2 = r1; //只复制引用（指针）
        r1.a = 2; //r1.a和r2.a都会更改
        Debug.Log("r1内存地址：" + getMemory(r1));
        //Debug.Log("r2内存地址：" + getMemory(r2));

        //ValData v1 = new ValData(); //在栈上分配
        //v1.a = 100; //在栈上修改
        //ValData v2 = new ValData(); //在栈上分配
        // v2 = v1; //值复制
        //v1.a = 200; //v1.a会更改，但v2.a不变

        //Debug.Log("V1内存地址：" + getMemory(v1));
        //Debug.Log("V2内存地址：" + getMemory(v2));


        //int[] a = new int[1];
        //int[] b = new int[1];
        //Debug.Log( string.Format("b={0,-2},未赋值前b的地址是:{1}", b[0], getMemory(b)));
        //a[0] = 3;
        ////b = a;// 引用类型等号赋值：此句赋值是b引用a的地址，此时a和b表示同一个内存空间地址  
        //b = RefFunReturn(a);// 引用类型通过函数的参数传递赋值：此句赋值是b引用a的地址，此时a和b表示同一个内存空间地址  
        //b[0] = 33;
        //Debug.Log(string.Format("b={0},赋值后b的地址是:{1}", b[0], getMemory(b)));
        //Debug.Log(string.Format("a={0},a的地址是:{1}", a[0], getMemory(a)));

        //int a=0;
        //int b=0;
        //Debug.Log(string.Format("b={0,-2},未用a给b赋值前b的地址是:{1}", b, getMemory(b)));
        //a = 3;
        ////b = a;// 引用类型等号赋值：此句赋值是b引用a的地址，此时a和b表示同一个内存空间地址  
        //b = ValFunReturn(a);// 引用类型通过函数的参数传递赋值：此句赋值是b引用a的地址，此时a和b表示同一个内存空间地址  
        //b= 33;
        //Debug.Log(string.Format("b={0},用a给b赋值后b的地址是:{1}", b, getMemory(b)));
        //Debug.Log(string.Format("a={0},a的地址是:{1}", a, getMemory(a)));



    }

    public static string getMemory(object o) // 获取引用类型的内存地址方法  
    {
        //if (o == null) return "";
        GCHandle h = GCHandle.Alloc(o, GCHandleType.Pinned);
        IntPtr addr = h.AddrOfPinnedObject();
        return "0x" + addr.ToString("X");
    }

    int[] RefFunReturn(int[] a)
    {
        return a;
    }

    int ValFunReturn(int a)
    {
        return a;
    }

}
