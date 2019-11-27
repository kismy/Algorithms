using UnityEngine;
using System.Collections;

public class TestDelegate : MonoBehaviour
{

    //定义一个委托（格式是不是很类很像），用来指向我们某个函数。（c++里面的指针函数）
    //param参数是名字
    private delegate void DebugString(string param);

    /// <summary>
    /// 输出中文名字
    /// </summary>
    public void DebugNameOfChina(string str)
    {
        Debug.Log("中文名字:" + str);
    }

    /// <summary>
    /// 输出英文名字
    /// </summary>
    public void DebugNameOfEnglish(string str)
    {
        Debug.Log("English Name:" + str);
    }

    //定义一个委托的变量事件
    private DebugString handlerDebugString;


    void Start()
    {
        //给委托对象赋值
        handlerDebugString = DebugNameOfChina;
        handlerDebugString("丁小未");
        Debug.Log("................................");

        //给委托对象再加一个事件，叫多路广播
        handlerDebugString += DebugNameOfEnglish;
        handlerDebugString("DingXiaowei");
        Debug.Log("................................");

        //输出中文试试
        handlerDebugString("丁小未");
        Debug.Log("................................");

        //给委托对象减去一个事件
        handlerDebugString -= DebugNameOfChina;
        handlerDebugString("DingXiaowei");

    }

}