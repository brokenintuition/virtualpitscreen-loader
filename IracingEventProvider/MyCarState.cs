using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSDK;

namespace IracingEventProvider
{
    public sealed class MyCarState : CarState
    {
        public float FuelLevel { get; set; }
        public float FuelLevelPercent { get; set; }
        public float FuelUsePerHour { get; set; }
        public float PitRepairLeft { get; set; } //todo: is this required or total?
        public float PitOptionalRepairLeft { get; set; }
        public float CurrentLapTime { get; set; }
        public float LastLapTime { get; set; }

        internal static MyCarState FromDataSample(DataSample data)
        {
            var t = data.Telemetry;
            var carIdx = (int)data.SessionData.DriverInfo.DriverCarIdx;

            var driver = data.SessionData.DriverInfo.Drivers[carIdx];

            var result = new MyCarState
            {
                CarIdx = carIdx,
                Position = t.PlayerCarPosition,
                PositionInClass = t.PlayerCarClassPosition,
                Lap = t.Lap,
                LapCompleted = t.LapCompleted,
                IsInPits = t.OnPitRoad,
                DriverName = driver.UserName,
                DriverID = driver.UserID,
                TeamID = driver.TeamID,
                FuelLevel = t.FuelLevel,
                FuelLevelPercent = t.FuelLevelPct,
                FuelUsePerHour = t.FuelUsePerHour,
                PitRepairLeft = t.PitRepairLeft,
                PitOptionalRepairLeft = t.PitOptRepairLeft,
                CurrentLapTime = t.LapCurrentLapTime,
                LastLapTime = t.LapLastLapTime
            };

            if (t.CarIdxPitStopCount != null)
                result.PitStopCount = t.CarIdxPitStopCount[result.CarIdx];

            if (t.HasRetired != null)
                result.HasRetired = t.HasRetired[result.CarIdx];

            return result;
        }

    }
}
