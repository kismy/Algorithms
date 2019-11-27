using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{

    public  class Trace
    {

        public static void Assert(bool shoot,string meg)
        {

            if (shoot)
            {
                UnityEngine.Debug.LogError(meg);
            }

        }
    }
}
