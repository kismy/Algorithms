using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public int second = 57;
    public int minute = 59;
    public int hour = 23;

    private Text timerShow;
    void Start ()
    {
        print("Time.fixedDeltaTime:"+ Time.fixedDeltaTime);
        timerShow = transform.Find("Panel/Panel/Text").GetComponent<Text>();

        InvokeRepeating("SecondAdd", 0, 1);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		//CaculateOneMinuteTimer();
	}

    private void SecondAdd()
    {
        second++;
        if (second == 60)
        {
            minute++;
            second = 0;
        }

        if (minute == 60)
        {
            hour++;
            minute = 0;
        }
        if (hour==24)
        {
            hour = 0;
        }
        timerShow.text = TimerShowFormat();
    }

    private string TimerShowFormat()
    {
        string timeString = "";
        string secondString = "00";
        string minuteString = "00";
        string hourString = "00";

        if (second < 10) secondString = "0" + second.ToString();
        else secondString =second.ToString();

        if (minute < 10) minuteString = "0" + minute.ToString();
        else minuteString = minute.ToString();

        if (hour < 10) hourString = "0" + hour.ToString();
        else hourString = hour.ToString();

        timeString = hourString  + " : " + minuteString + " : " + secondString;
        return timeString;
    }

    float timer=1.0f;
    private void CaculateOneMinuteTimer()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
        else
        {
            timer = 1.0f;
            second++;
        }
    }
}
