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


        internal static SessionInfo FromSessionData(SessionData data)
        {
            return new SessionInfo();
        }
    }
}
