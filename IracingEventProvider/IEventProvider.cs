using System;
using System.Collections.Generic;

namespace IracingEventProvider
{
    interface IEventProvider : IObservable<TelemetryUpdate>, IDisposable
    {
        event Action<SessionInfo> NewSessionInfo;
    }

    public class TelemetryUpdate
    {
        public MyCarState MyCar { get; set; }
        public IEnumerable<CarState> CompetitorStates { get; set; }
    }

}
