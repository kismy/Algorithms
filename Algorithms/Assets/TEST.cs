using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour {


    public delegate void DoHurt();

    protected DoHurt hurtCall;

    // Use this for initialization
    void Start ()
    {
        test();

    }

    public void aaa(DoHurt call)
    {
        this.hurtCall = (DoHurt)Delegate.Combine(this.hurtCall, call);

    }

    public void myHurt001()
    {
        Debug.Log("调用1");
    }

    public void myHurt002()
    {

        Debug.Log("调用2");
    }

    public void test()
    {
        this.aaa(myHurt001);
        this.aaa(myHurt002);
        this.hurtCall();
    }
}
