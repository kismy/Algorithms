using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{

    
    //<愤怒的小鸟>给予初速度以后,怎么让小鸟受到 [重力 和 空气阻力]的影响而绘制抛物线轨迹,说出具体的计算方法.
    //Vector3 v代表初速度v'代表现在的速度，假设小鸟是沿的z轴也就是transform.forward方向运动的质量为1，那么v‘=v-new Vector3(0,g*t,f*t)，transform.Translate(v')做的就是抛物线运动（g为重力加速度不要用现实中的需要自己调试，f为阻力也要自己调试设置，t为时间）
    public  Vector3 Speed=new Vector3(5,5,0);
    public float dampFactor = 1;
    public float Gravity = -9.8f;

    


    void Start()
    {
        transform.GetComponent<Rigidbody>().velocity = Speed;
    }


    void FixedUpdate()
    {
      

    }

    private void MyCast()
    {
        transform.GetComponent<Rigidbody>().velocity = Speed;
    }

    private void OnCollisionEnter(Collision collider)
    {

        if (collider.gameObject != null)
        {
            MyCast();
        }
    }


}
