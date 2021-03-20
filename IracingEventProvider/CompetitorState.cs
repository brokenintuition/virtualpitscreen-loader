using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSDK;

namespace IracingEventProvider
{
    public sealed class CompetitorState
    {
        public int CarIdx { get; private set; }
        public int Position { get; private set; }
        public int PositionInClass { get; private set; }
        public int Lap { get; private set; }
        public int LapCompleted { get; private set; }
        public int PitStopCount { get; private set; }
        public bool IsInPits { get; private set; }
        public bool HasRetired { get; private set; }
        public string DriverName { get; private set; }
        public long DriverID { get; private set; }
        public long TeamID { get; private set; }


        internal static IEnumerable<CompetitorState> FromSample(DataSample data)
        {
            var result = new List<CompetitorState>();
            var t = data.Telemetry;

            for (var i = 0; i < t.CarDetails.Length; i++)
            {
                if (!t.HasData(i))
                    continue;

                var driverInfo = t.CarDetails[i].Driver;
                result.Add(new CompetitorState
                {
                    CarIdx = i,
                    Position = t.CarIdxPosition[i],
                    PositionInClass = t.CarIdxClassPosition[i],
                    Lap = t.CarIdxLap[i],
                    LapCompleted = t.CarIdxLapCompleted[i],
                    PitStopCount = t.CarIdxPitStopCount[i],
                    IsInPits = t.CarIdxOnPitRoad[i],
                    HasRetired = t.HasRetired[i],
                    DriverName = driverInfo.UserName,
                    DriverID = driverInfo.UserID,
                    TeamID = driverInfo.TeamID
                });
            }

            return result;
        }
    }
}
