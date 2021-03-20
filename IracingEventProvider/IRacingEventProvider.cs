using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using iRacingSDK;

namespace IracingEventProvider
{
    public sealed class IRacingEventProvider : IEventProvider
    {
        public event Action<SessionInfo> NewSessionInfo;
        private readonly Subject<TelemetryUpdate> _subject = new Subject<TelemetryUpdate>();
        private IDisposable _subscription;
        private bool _hasSentSessionData;

        public IRacingEventProvider()
        {
            _subscription = Observable.FromEvent<DataSample>(x => iRacing.NewData += x, 
                x => iRacing.NewData -= x)
                .Where(x => x.IsConnected)
                .Subscribe(HandleSample);

            iRacing.StartListening();
        }


        public IDisposable Subscribe(IObserver<TelemetryUpdate> observer)
        {
            return _subject.Subscribe(observer);
        }

        private void HandleSample(DataSample sample)
        {
            if (!_hasSentSessionData)
            {
                _hasSentSessionData = true;
                NewSessionInfo?.Invoke(SessionInfo.FromSessionData(sample.SessionData));
            }

            _subject.OnNext(new TelemetryUpdate
            {
                MyCar = MyCarState.FromDataSample(sample),
                CompetitorStates = CarState.FromSample(sample)
            });
        }

        public void Dispose()
        {
            _subscription.Dispose();
            iRacing.StopListening();
        }

    }
}
