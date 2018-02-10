using ConferenceTrackManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Repository
{
    static class DataBaseInMemory
    {
        public static List<Talk> DataBaseTalk = new List<Talk>();
        public static List<Scheduling> DataBaseScheduling = new List<Scheduling>();
        public static List<Track> DataBaseTrack = new List<Track>();
        public static List<Session> DataBaseSession = new List<Session>();
    }
}
