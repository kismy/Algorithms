using UnityEngine;
using System.Collections.Generic;

public class EdgeComparer : Comparer<Edge>
{
    public EdgeComparer()
    {

    }
    public override int Compare(Edge x, Edge y)
    {
        return x.Weight().CompareTo(y.Weight());
    }
}
