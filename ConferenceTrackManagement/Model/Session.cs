using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Model
{
    //represent a session of the conference
    public class Session
    {
        public string Title { get; set; }//title of the session
        public DateTime StartHour { get; set; }//hour to start session
        public DateTime EndHour { get; set; }//hour to end session
        public List<Scheduling> SchedulingList { get; set; }//list of Scheduling of the Session
        public int Order { get; set; }//order to session happen
    }
}
