using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Model
{
    //represent a scheduling of the talk
    public class Scheduling
    {
        public Talk Talk { get; set; }//what the talk is related
        public Session Session { get; set; }//what the session is related
        public DateTime StartHour { get; set; }//hour to start the scheduling
        public DateTime EndHour { get; set; }//hour to end the scheduling
    }
}
