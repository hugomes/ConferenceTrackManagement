using System.Collections.Generic;
using ConferenceTrackManagement.Model;

namespace ConferenceTrackManagement.IRepository
{
    public interface ITalkRepository<T> : IRepository<T>
    {
        void DeleteAll();//delete all objects
        Talk GetTalkByIndex(int index);
    }
}
