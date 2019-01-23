using System.Collections.Generic;

namespace ConferenceTrackManagement.IRepository
{
    public interface ITalkRepository<T> : IRepository<T>
    {
        void DeleteAll();//delete all objects
    }
}
