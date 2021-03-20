using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IracingEventProvider;

namespace ConsoleDataDump
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventProvider = new IRacingEventProvider();
            eventProvider.NewSessionInfo += NewSessionInfo;
            eventProvider.Subscribe(HandleCompetitorState);
            Console.ReadLine();
        }


        private static void NewSessionInfo(SessionInfo info)
        {

        }

        private static void HandleCompetitorState(CompetitorState state)
        {

        }
    }
}
