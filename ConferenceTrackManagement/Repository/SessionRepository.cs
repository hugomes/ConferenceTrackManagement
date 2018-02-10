using ConferenceTrackManagement.IRepository;
using ConferenceTrackManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Repository
{
    public class SessionRepository : IRepository.IRepository<Session>
    {
        //delete an session from list
        public bool Delete(Session obj)
        {
            return DataBaseInMemory.DataBaseSession.Remove(obj);
        }

        //get an session from list
        public Session Get(Session obj)
        {
            return (Session)DataBaseInMemory.DataBaseSession.Where(a=>a.Title.Contains(obj.Title));
        }

        //list all sessions
        public List<Session> List()
        {
            return DataBaseInMemory.DataBaseSession;
        }

        //add a session to list
        public bool Save(Session obj)
        {
            try
            {
                DataBaseInMemory.DataBaseSession.Add(obj);
                return DataBaseInMemory.DataBaseSession.Contains(obj);
            }
            catch (Exception exception)
            {
                throw new Exception("The "+obj.Title+" can't get save.");
            }
            return false;
        }
    }
}
