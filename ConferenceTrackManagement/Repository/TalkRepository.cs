using ConferenceTrackManagement.Model;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement.Repository
{
    public class TalkRepository : IRepository.IRepository<Talk>
    {
        //delete an talk from list
        public bool Delete(Talk obj)
        {
            return DataBaseInMemory.DataBaseTalk.Remove(obj);
        }

        //delete all talks from list
        public void DeleteAll()
        {
            DataBaseInMemory.DataBaseTalk.Clear();
        }

        //get an talk from list
        public Talk Get(Talk obj)
        {
            return (Talk)DataBaseInMemory.DataBaseTalk.Where(a => a.Title.Contains(obj.Title));
        }

        //list all talks
        public List<Talk> List()
        {
            return DataBaseInMemory.DataBaseTalk;
        }

        //add a talk to list
        public bool Save(Talk obj)
        {
            DataBaseInMemory.DataBaseTalk.Add(obj);
            return DataBaseInMemory.DataBaseTalk.Contains(obj);
        }
    }
}
