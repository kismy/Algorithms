using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour {
    private class SortPointInfo
    {
        public float SqrtDistance;
        public PointInfo pointInfo;
        public SortPointInfo(PointInfo pointInfo, Vector2 PlayerPosition)
        {

            this.pointInfo = pointInfo;
            SqrtDistance = Vector2.SqrMagnitude(pointInfo.position - PlayerPosition);
        }
    }

    [SerializeField]
    private GameObject PointPreFab;
    List<PointInfo> pointList = new List<PointInfo>();


    public void SortPointList(List<PointInfo> list, Vector2 PlayerPosition)
    {
        if (list == null || list.Count < 0)
            return;
        List<SortPointInfo> tempPointList = new List<SortPointInfo>();
        foreach (var pointInfo in list)
            tempPointList.Add(new SortPointInfo(pointInfo, PlayerPosition));

        tempPointList.Sort((point1, point2) => { return point1.SqrtDistance.CompareTo(point2.SqrtDistance); });

        for (int i = 0; i < list.Count; i++)
            list[i] = tempPointList[i].pointInfo;

    }

    public void CreateMap(List<PointInfo> list)
    {
        if (list == null || list.Count < 0)
            return;
        foreach (PointInfo pointInfo in list)
        {
            GameObject go = GameObject.Instantiate(PointPreFab);
            Point Point = go.GetComponent<Point>();
            Point.pointInfo = pointInfo;           
        }
    }
}
