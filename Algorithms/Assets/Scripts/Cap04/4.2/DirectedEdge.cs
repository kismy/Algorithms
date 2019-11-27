using UnityEngine;
using System.Collections;

public class DirectedEdge {

	// Use this for initialization
	void Start () {
        //DirectedEdge e = new DirectedEdge(12, 34, 5.67);
        //print(e);
    }

    private  int v;
    private  int w;
    private  double weight;

    
    public DirectedEdge(int v, int w, double weight)
    {
        if (v < 0) throw new System.Exception("Vertex names must be nonnegative integers");
        if (w < 0) throw new System.Exception("Vertex names must be nonnegative integers");
        if (double.IsNaN(weight)) throw new System.Exception("Weight is NaN");
        this.v = v;
        this.w = w;
        this.weight = weight;
    }

    public int from()
    {
        return v;
    }

    
    public int to()
    {
        return w;
    }

   
    public double Weight()
    {
        return weight;
    }

   
    public override  string ToString()
    {
        return( v + "->" + w + " " + weight.ToString());
    }

  
   
}
