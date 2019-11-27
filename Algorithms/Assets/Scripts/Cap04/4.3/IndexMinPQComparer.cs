
using System;

public class IndexMinPQComparer : System.Collections.Generic.Comparer<double>
{
    public override int Compare(double x, double y)
    {
        if (x > y) return 1;
        else if (y > x) return -1;
        else return 0;
    }
}
