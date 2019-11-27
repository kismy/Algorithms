using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Edge
{

    private  int v;
    private  int w;
    private  double weight;

    public Edge()
    {

    }

    public Edge(int v, int w, double weight)
    {
        if (v < 0) throw new System.Exception("vertex index must be a nonnegative integer");
        if (w < 0) throw new System.Exception("vertex index must be a nonnegative integer");
        if (double.IsNaN(weight)) throw new System.Exception("Weight is NaN");
        this.v = v;
        this.w = w;
        this.weight = weight;
       
    }

   

    public double Weight()
    {
        return weight;
    }

  

    public int either()
    {
        return v;
    }

  

    public int other(int vertex)
    {
        if (vertex == v) return w;
        else if (vertex == w) return v;
        else throw new System.Exception("Illegal endpoint");
    }

   

    public int compareTo(Edge that)
    {
        return this.weight.CompareTo(that.weight);
    }

    

    public override string ToString()
    {
        string str = (v+" - "+w+" - "+weight);
        return str;
    }

    public double ToInt()
    {        
        return weight;
    }

   
}
