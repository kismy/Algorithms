using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StudentComparer : MonoBehaviour,IComparer<Student> {

	
    public enum CompareType
    {
        Name, Age, Grade
    }
    private CompareType type;
    // 构造函数，根据type的值，判断按哪个字段排序  
    public StudentComparer(CompareType type)
    {
        this.type = type;
    }
    public int Compare(Student x, Student y)
    {
        switch (this.type)
        {
            case CompareType.Name:
                return x.Name.CompareTo(y.Name);
            case CompareType.Age:
                return x.Age.CompareTo(y.Age);
            default:
                //case CompareType.Grade:  
                return x.Grade.CompareTo(y.Grade);
        }
    }
}  

