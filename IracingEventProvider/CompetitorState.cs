using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSDK;

namespace IracingEventProvider
{
    class CompetitorState
    {


        internal static IEnumerable<CompetitorState> FromSample(DataSample data)
        {
            return Enumerable.Empty<CompetitorState>();
        }
    }
}
