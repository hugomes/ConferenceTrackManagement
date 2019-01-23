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
        public static IList<Talk> DataBaseTalk = new List<Talk>();
        public static IList<Scheduling> DataBaseScheduling = new List<Scheduling>();
        public static IList<Track> DataBaseTrack = new List<Track>();
        public static IList<Session> DataBaseSession = new List<Session>();
        public static IList<Person> DataBasePerson = new List<Person>();
    }
}
