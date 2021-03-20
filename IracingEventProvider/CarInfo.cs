using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iRacingSDK.SessionData._DriverInfo;

namespace IracingEventProvider
{
    // Todo: might rename this driver info if iRacing actually tells me 
    // who's in the car during team events
    public sealed class CarInfo
    {
        public long UserID { get; private set; }
        public long CarIdx { get; private set; }
        public long TeamID { get; private set; }
        public long CarID { get; private set; }
        public long CarClassID { get; private set; }
        public string UserName { get; private set; }
        public string TeamName { get; private set; }
        public string CarNumberString { get; private set; }
        public long CarNumber { get; private set; }

        public static CarInfo FromDriverInfo(_Drivers driver)
        {
            return new CarInfo
            {
                UserID = driver.UserID,
                CarIdx = driver.CarIdx,
                TeamID = driver.TeamID,
                CarID = driver.CarID,
                CarClassID = driver.CarClassID,
                UserName = driver.UserName,
                TeamName = driver.TeamName,
                CarNumberString = driver.CarNumber,
                CarNumber = driver.CarNumberRaw
            };

        }
    }
}
