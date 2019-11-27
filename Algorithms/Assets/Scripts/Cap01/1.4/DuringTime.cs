using UnityEngine;
using System;
using System.Diagnostics;

public class DuringTime : MonoBehaviour {
    System.Diagnostics.Stopwatch watch = new Stopwatch();
    void Start () {

        watch.Start(); //  开始监视代码运行时间

                           //  需要测试的代码 ....

        watch.Stop(); //  停止监视


        TimeSpan timespan = watch.Elapsed; //  获取当前实例测量得出的总时间   //span=跨度，Elapsed=消逝，过去，间隔
        double hours = timespan.TotalHours; // 总小时
        double minutes = timespan.TotalMinutes;  // 总分钟
        double seconds = timespan.TotalSeconds;  //  总秒数
        double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数

    }

 
   
}
