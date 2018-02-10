using ConferenceTrackManagement.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Model
{
    //represent a day of the conference
    public class Track
    {
        public Track(DateTime dayOfTheTrack)
        {
            //always when create a Track, need create 4 sessions: morning session, lunch session, afternoon session and network session
            SessionController sessionController = new SessionController();
            this.SessionList = new List<Session>();
            this.SessionList.Add(sessionController.CreateMorningSession(dayOfTheTrack));
            this.SessionList.Add(sessionController.CreateLunchSession(dayOfTheTrack));
            this.SessionList.Add(sessionController.CreateAfternoonSession(dayOfTheTrack));
            //new date at 5PM to create the network session
            DateTime networkDay = new DateTime(dayOfTheTrack.Year, dayOfTheTrack.Month, dayOfTheTrack.Day, 17, 0, 0);
            this.SessionList.Add(sessionController.CreateNetworkSession(networkDay));
        }

        public string Title { get; set; }//title/name of the track
        public List<Session> SessionList { get; set; }//list of Session of the Track
        public DateTime Date { get; set; }//date of the track
    }
}
