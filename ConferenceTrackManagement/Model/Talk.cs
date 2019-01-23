using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Model
{
    //represent a talk submited to the conference
    public class Talk
    {
        public Talk()
        {
            this.Audience = new List<Person>();
        }

        public string Title { get; set; } //title of talk
        public int Duration { get; set; } //duration of talk in minutes
        public bool IsLightning { get; set; }//if is a lightning(5 minutes) scheduling
        public IList<Person> Audience { get; set; }
    }
}
