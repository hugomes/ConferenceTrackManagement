using System.Collections.Generic;

namespace ConferenceTrackManagement.IRepository
{
    public interface IRepository<T>
    {
        bool Save(T obj);//add an object to list
        T Get(T obj);//get an object from list
        IList<T> List();//list all objects
        bool Delete(T obj);//delete an object from list
    }
}
