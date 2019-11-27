using UnityEngine;
using System.Collections;

public class Student : MonoBehaviour {

	
    private string name;
    // 姓名  
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    private int age;
    // 年龄  
    public int Age
    {
        get
        {
            return age;
        }
        set
        {
            age = value;
        }
    }
    private int grade;
    // 年级  
    public int Grade
    {
        get
        {
            return grade;
        }
        set
        {
            grade = value;
        }
    }
    //构造函数  
    public Student(string name, int age, int grade)
    {
        this.name = name;
        this.age = age;
        this.grade = grade;
    }
    public override string ToString()
    {
        string gradeStr;
        if (Grade == 1) gradeStr = "一年级";
        else if (Grade == 2) gradeStr = "二年级";
        else if (Grade == 3) gradeStr = "三年级";
        else gradeStr = "未知年级";

        return this.name + "," + this.age.ToString() + "," + gradeStr;
    }
}  

