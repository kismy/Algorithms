using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PointType
{
    PointType_Home,
    PointType_Hole,
    PointType_Farm
}
public struct PointInfo
{
    public int id;
    public PointType pointType;
    public Vector2 position;
}
public class Point : MonoBehaviour {

    public PointInfo pointInfo;

    void OnClick()
    {
        Player.instance.OnClickPoint(this);
   
    }
}
