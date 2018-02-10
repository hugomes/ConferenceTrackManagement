using ConferenceTrackManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.IRepository
{
    public interface IRepository<T>
    {
        bool Save(T obj);//add an object to list
        T Get(T obj);//get an object from list
        List<T> List();//list all objects
        bool Delete(T obj);//delete an object from list
    }
}
