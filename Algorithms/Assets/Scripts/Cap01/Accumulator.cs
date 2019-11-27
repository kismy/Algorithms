using UnityEngine;
using System.Collections;

public class Accumulator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        main();

    }

    private int n = 0;          // number of data values
    private double sum = 0.0;   // sample variance * (n-1)
    private double mu = 0.0;    // sample mean

   
    public Accumulator()
    {
    }

  
    public void addDataValue(double x)
    {
        n++;
        double delta = x - mu;
        mu += delta / n;
        sum += (double)(n - 1) / n * delta * delta;
    }

   
    public double mean()
    {
        return mu;
    }

   
    public double var()
    {
        if (n <= 1) return double.NaN;
        return sum / (n - 1);
    }

  

    public double stddev()
    {
        return Mathf.Sqrt((float)this.var());
    }

   
    public int count()
    {
        return n;
    }

   
    public static void main()
    {
        Accumulator stats = new Accumulator();
        double x = 12.0f;
        stats.addDataValue(x);

        Debug.LogFormat("n      = " +stats.count());
        Debug.LogFormat("mean   =  "+ stats.mean());
        Debug.LogFormat("stddev = "+stats.stddev());
        Debug.LogFormat("var    =  "+stats.var());
    }
}
