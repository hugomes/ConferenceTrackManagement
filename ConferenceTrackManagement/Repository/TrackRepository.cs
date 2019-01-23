using ConferenceTrackManagement.IRepository;
using ConferenceTrackManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Repository
{
    public class TrackRepository : IRepository.ITrackRepository<Track>
    {
        //delete an track from list
        public bool Delete(Track obj)
        {
            return !DataBaseInMemory.DataBaseTrack.Remove(obj);
        }

        //get an track from list
        public Track Get(Track obj)
        {
            return (Track)DataBaseInMemory.DataBaseTrack.Where(a=>a.Title.Contains(obj.Title));
        }

        //list all tracks
        public List<Track> List()
        {
            return DataBaseInMemory.DataBaseTrack;
        }

        //add a track to list
        public bool Save(Track obj)
        {
            DataBaseInMemory.DataBaseTrack.Add(obj);
            return DataBaseInMemory.DataBaseTrack.Contains(obj);
        }
    }
}
