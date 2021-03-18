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
    class IRacingEventProvider : IEventProvider
    {
        public event Action<SessionInfo> NewSessionInfo;
        private readonly iRacingConnection _iracingConnection;
        private readonly Subject<CompetitorState> _competitorSubject = new Subject<CompetitorState>();
        private readonly Subject<MyCarState> _myCarSubject = new Subject<MyCarState>();
        private IDisposable _subscription;
        private bool _hasSentSessionData;

        public IRacingEventProvider()
        {
            _iracingConnection = new iRacingConnection();
            _iracingConnection.Connected += _iracingConnection_Connected;
            _iracingConnection.Disconnected += _iracingConnection_Disconnected;

            _subscription = Observable.FromEvent<DataSample>(x => _iracingConnection.NewSessionData += x, 
                x => _iracingConnection.NewSessionData -= x)
                .Where(d => d.IsConnected).Subscribe(HandleSample);
        }

        private void _iracingConnection_Disconnected()
        {
            //todo: log
        }

        private void _iracingConnection_Connected()
        {
            //todo: log
        }

        public IDisposable Subscribe(IObserver<CompetitorState> observer)
        {
            return _competitorSubject.Subscribe(observer);
        }
        
        public IDisposable Subscribe(IObserver<MyCarState> observer)
        {
            return _myCarSubject.Subscribe(observer);
        }

        private void HandleSample(DataSample sample)
        {
            if (!_hasSentSessionData)
            {
                _hasSentSessionData = true;
                NewSessionInfo?.Invoke(SessionInfo.FromSessionData(sample.SessionData));
            }

            _myCarSubject.OnNext(MyCarState.FromDataSample(sample));

            var competitorStates = CompetitorState.FromSample(sample);
            foreach (var competitorState in competitorStates)
                _competitorSubject.OnNext(competitorState);

        }

        public void Dispose()
        {
            _iracingConnection.Connected -= _iracingConnection_Connected;
            _iracingConnection.Disconnected -= _iracingConnection_Disconnected;
            _subscription.Dispose();
        }

    }
}
