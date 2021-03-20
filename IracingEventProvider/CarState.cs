using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSDK;
using static iRacingSDK.SessionData;

namespace IracingEventProvider
{
    public class CarState
    {
        public int CarIdx { get; set; }
        public int Position { get; set; }
        public int PositionInClass { get; set; }
        public int Lap { get; set; }
        public int LapCompleted { get; set; }
        public int PitStopCount { get; set; }
        public bool IsInPits { get; set; }
        public bool HasRetired { get; set; }
        public string DriverName { get; set; }
        public long DriverID { get; set; }
        public long TeamID { get; set; }


        internal static IEnumerable<CarState> FromSample(DataSample data)
        {
            var result = new List<CarState>();
            var t = data.Telemetry;

            for (var i = 0; i < t.CarDetails.Length; i++)
            {
                if (!t.HasData(i) || t.CarDetails[i].IsPaceCar)
                    continue;

                var driverInfo = t.CarDetails[i].Driver;
                var competitorState = new CarState
                {
                    CarIdx = i,
                    Position = t.CarIdxPosition[i],
                    PositionInClass = t.CarIdxClassPosition[i],
                    Lap = t.CarIdxLap[i],
                    LapCompleted = t.CarIdxLapCompleted[i],
                    IsInPits = t.CarIdxOnPitRoad[i],
                    DriverName = driverInfo.UserName,
                    DriverID = driverInfo.UserID,
                    TeamID = driverInfo.TeamID
                };

                if (t.CarIdxPitStopCount != null)
                    competitorState.PitStopCount = t.CarIdxPitStopCount[i];
                if (t.HasRetired != null)
                    competitorState.HasRetired = t.HasRetired[i];

                result.Add(competitorState);
            }

            return result;
        }

    }
}
