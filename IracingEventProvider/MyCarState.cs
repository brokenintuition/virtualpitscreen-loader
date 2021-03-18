using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSDK;

namespace IracingEventProvider
{
    public sealed class MyCarState
    {

        
        internal static MyCarState FromDataSample(DataSample data)
        {
            return new MyCarState();
        }

    }
}
