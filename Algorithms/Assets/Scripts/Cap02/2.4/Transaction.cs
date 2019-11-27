using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//1）没有实现对账户的各种属性进行排序的接口
public class Transaction : MonoBehaviour {

	void Start () {

        int[] a = new int[3] { 1, 2, 3 };
        
	}
    public  string  who;      // customer
    public  Date    when;     // date
    public  double amount;   // amount


    
    public Transaction(string who, Date when, double amount)
    {
        if (double.IsNaN(amount) || double.IsInfinity(amount))
            Debug.LogError("Amount cannot be NaN or infinite");
        this.who = who;
        this.when = when;
        this.amount = amount;
    }

   

    public Transaction(string transaction)
    {
       
        string[] a = transaction.Split('|');
        who = a[0];
        when = new Date(a[1]);
        amount = double.Parse(a[2]);
        if(double.IsNaN(amount) || double.IsInfinity(amount))
            Debug.LogError("Amount cannot be NaN or infinite");
    }

  

    public string Who()
    {
        return who;
    }

    

    public Date When()
    {
        return when;
    }

   

    public double Amount()
    {
        return amount;
    }


    public string ToString()
    {
        return string.Format("%-10s %10s %8.2f", who, when, amount);
    }

   

    public  int compareTo(Transaction that)
    {
        return this.amount.CompareTo(that.amount);
    }

   

    public bool equals(Object other)
    {
        if (other == this) return true;
        if (other == null) return false;
        if (other.GetType() != this.GetType()) return false;
        Transaction that = (Transaction)other;
        return (this.amount == that.amount) && (this.who.Equals(that.who))
                                            && (this.when.equals(that.when));
    }


   
    public int hashCode()
    {
        int hash = 1;
        hash = 31 * hash + who.GetHashCode();
        hash = 31 * hash + when.hashCode();
        hash = 31 * hash + ((double)amount).GetHashCode();
        return hash;
        // return Objects.hash(who, when, amount);
    }




    public static  class  WhoOrder 
    {
        public static int compare(Transaction v, Transaction w)
        {
           
            return string.Compare(v.who, w.who);
        }
    }

    /**
     * Compares two transactions by date.
     */
    public  class WhenOrder
    {
        public static int compare(Transaction v, Transaction w)
        {
            return v.when.compareTo(w.when);
        }
    }

    /**
     * Compares two transactions by amount.
     */
    public  class HowMuchOrder
    {

        public static int compare(Transaction v, Transaction w)
        {

            return v.amount.CompareTo(w.amount);
        }
    }



    public static void main()
    {
        Transaction[] a = new Transaction[4];
        a[0] = new Transaction("Turing  | 6/17/1990 | 644.08");
        a[1] = new Transaction("Tarjan  | 3/26/2002 | 4121.85");
        a[2] = new Transaction("Knuth   | 6/14/1999 | 288.34");
        a[3] = new Transaction("Dijkstra| 8/22/2007 |  2678.40");

        print("Unsorted");
        for (int i = 0; i < a.Length; i++)
            print(a[i]);
      

        //print("Sort by date");
        //Arrays.sort(a, new Transaction.WhenOrder());
        //for (int i = 0; i < a.Length; i++)
        //    print(a[i]);



        //print("Sort by customer");
        //System.Array.Sort (a, new Transaction.WhoOrder());
        //for (int i = 0; i < a.Length; i++)
        //    print(a[i]);



        //print("Sort by amount");
        //Arrays.sort(a, new Transaction.HowMuchOrder());
        //for (int i = 0; i < a.Length; i++)
        //    print(a[i]);
       
    }

   
}




