using UnityEngine;
using System.Collections.Generic;

public class CompareTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<Student> arr = new List<Student>();
        arr.Add(new Student("张三", 7, 1));
        arr.Add(new Student("李四", 11,2));
        arr.Add(new Student("王五", 21, 1));
        arr.Add(new Student("陈六", 8, 3));
        arr.Add(new Student("刘七", 15, 2));

        print("调用Sort方法，实现按年级排序 ");
        arr.Sort(new StudentComparer(StudentComparer.CompareType.Grade));
        foreach (Student item in arr)
           print(item.ToString());

        print("调用Sort方法，实现按姓名排序  ");
        arr.Sort(new StudentComparer(StudentComparer.CompareType.Name));
        foreach (Student item in arr)
            print(item.ToString());

        print("调用Sort方法，实现按年龄排序  ");
        arr.Sort(new StudentComparer(StudentComparer.CompareType.Age));
        foreach (Student item in arr)
            print(item.ToString());
    }
}
