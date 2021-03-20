using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iRacingSDK;

namespace IracingEventProvider
{
    public sealed class SessionInfo
    {
        public string TrackName { get; private set; }
        public string TrackDisplayName { get; private set; }
        public string EventType { get; private set; }
        public long SessionID { get; private set; }
        public long SubSessionId { get; private set; }
        public IReadOnlyList<CarInfo> Cars { get; private set; }


        internal static SessionInfo FromSessionData(SessionData data)
        {
            var cars = data.DriverInfo.CompetingDrivers.Select(x => CarInfo.FromDriverInfo(x)).ToList();

            return new SessionInfo()
            {
                TrackName = data.WeekendInfo.TrackName,
                TrackDisplayName = data.WeekendInfo.TrackDisplayName,
                EventType = data.WeekendInfo.EventType,
                SessionID = data.WeekendInfo.SessionID,
                SubSessionId = data.WeekendInfo.SubSessionID,
                Cars = cars
            };
        }
    }
}
