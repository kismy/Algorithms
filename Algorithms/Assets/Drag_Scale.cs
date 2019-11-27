using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Scale : MonoBehaviour {

    void Start()
    {
        StartCoroutine(OnMouseDown());
    }

    void Update ()
	{
	    Scroller();

	}

    //1.缩放Cube
    private void Scroller()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
           transform.localScale+=new Vector3(-0.1f,-0.1f,-0.1f);
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

    }


    //二.鼠标实现在场景中拖动物体：
    //1）将世界坐标转换成屏幕坐标，然后计算物体与鼠标之间移动量
    //2）循环鼠标被按下操作，得到鼠标的当前位置，加上计算好的移动量，将新的坐标赋值给物理就行了。
    //注：主要是开启一个协同程序（Corountine）来处理
      IEnumerator OnMouseDown()
    {
        //将Cube由世界坐标系转换为屏幕坐标系
        Vector3 cubeScreenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
        print(cubeScreenSpace.z);//屏幕空间下，物体里相机的距离

        //一下完成两个步骤 
        //1.由于鼠标的坐标系是2维，需要转换成3维的世界坐标系 
        //2.只有3维坐标情况下才能来计算鼠标位置与物理的距离，offset即是距离
        //将鼠标屏幕坐标转为三维坐标，再算出物体位置与鼠标之间的距离

        Vector3 mouseWorldSpace =
              Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenSpace.z));
        Vector3 offset = transform.position - mouseWorldSpace;
        
        while (Input.GetMouseButton(0))
        {
            //得到现在鼠标的2维坐标系位置
            Vector3 curMouseWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenSpace.z)) ;
            //将当前鼠标的2维位置转换成3维位置，再加上鼠标的移动量
           
            //curPosition就是物体应该的移动向量赋给transform的position属性
            transform.position = curMouseWorldSpace + offset;
            yield return new WaitForFixedUpdate(); //这个很重要，循环执行
        }
    }
}
