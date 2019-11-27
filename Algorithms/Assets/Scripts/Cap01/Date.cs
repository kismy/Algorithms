using UnityEngine;
using System.Collections;

public class Date : MonoBehaviour {

	
	void Start () {
        main();
	}
    private static  int[] DAYS = { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    private  int month;   // month (between 1 and 12)
    private  int day;     // day   (between 1 and DAYS[month]
    private  int year;    // year


    public Date()
    {       
        this.month =-1;
        this.day =-1;
        this.year =-1;
    }

    public Date(int month, int day, int year)
    {
        if (!isValid(month, day, year)) Debug.LogError("Invalid date");
        this.month = month;
        this.day = day;
        this.year = year;
    }


    public Date(string date)
    {
        string[] fields = date.Split('/');
        if (fields.Length != 3)
        {
            Debug.LogError("Invalid date");
        }
        month = int.Parse(fields[0]);
        day = int.Parse(fields[1]);
        year = int.Parse(fields[2]);
        if (!isValid(month, day, year)) Debug.LogError("Invalid date");
    }

    public static Date ReadDates( string agrs)
    {
        //勘误：怎么检测日期输入错误，并返回
        Date date = new Date();
        string[] str = agrs.Split('|','.');
        if (str.Length != 3)
        {
            throw new System.Exception("日期输入格式错误！");
        }
        else {
            date.year = int.Parse(str[0]);
            date.month = int.Parse(str[1]);
            date.day = int.Parse(str[2]);
        }
        foreach (string item in str)
        {
            print("Str item:" + item);
        }

        return date;
    }

    /**
     * Return the month.
     * @return the month (an integer between 1 and 12)
     */
    public int Month()
    {
        return month;
    }

    /**
     * Returns the day.
     * @return the day (an integer between 1 and 31)
     */
    public int Day()
    {
        return day;
    }

   
    public int Year()
    {
        return year;
    }



    private static bool isValid(int m, int d, int y)
    {
        if (m < 1 || m > 12) return false;
        if (d < 1 || d > DAYS[m]) return false;
        if (m == 2 && d == 29 && !isLeapYear(y)) return false;
        return true;
    }


    private static bool isLeapYear(int y)
    {
        if (y % 400 == 0) return true;
        if (y % 100 == 0) return false;
        return y % 4 == 0;
    }



    public Date Next()
    {
        if (isValid(month, day + 1, year)) return new Date(month, day + 1, year);
        else if (isValid(month + 1, 1, year)) return new Date(month + 1, 1, year);
        else return new Date(1, 1, year + 1);
    }

   

    public bool IsAfter(Date that)
    {
        return compareTo(that) > 0;
    }

   

    public bool isBefore(Date that)
    {
        return compareTo(that) < 0;
    }

  

    public int compareTo(Date that)
    {
        if (this.year < that.year) return -1;
        if (this.year > that.year) return +1;
        if (this.month < that.month) return -1;
        if (this.month > that.month) return +1;
        if (this.day < that.day) return -1;
        if (this.day > that.day) return +1;
        return 0;
    }

   

    public string toString()
    {
        return month + "/" + day + "/" + year;
    }

   

    public bool equals(Object other)
    {
        if (other == this) return true;
        if (other == null) return false;
        if (other.GetType() != this.GetType()) return false;
        Date that = (Date)other;
        return (this.month == that.month) && (this.day == that.day) && (this.year == that.year);
    }

    

    public int hashCode()
    {
        int hash = 17;
        hash = 31 * hash + month;
        hash = 31 * hash + day;
        hash = 31 * hash + year;
        return hash;
    }

   

    public static void main()
    {
        Date today = new Date(2, 25, 2004);
       print(today);
        for (int i = 0; i < 10; i++)
        {
            today = today.Next();
            print(today);
        }

       print(today.IsAfter(today.Next()));
       print(today.IsAfter(today));
        print(today.Next().IsAfter(today));


        Date birthday = new Date(10, 16, 1971);
        print(birthday);
        for (int i = 0; i < 10; i++)
        {
            birthday = birthday.Next();
            print(birthday);
        }
    }
}
