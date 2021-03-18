using System;

namespace IracingEventProvider
{
    interface IEventProvider : IObservable<CompetitorState>, IDisposable
    {
        event Action<SessionInfo> NewSessionInfo;
    }
}
