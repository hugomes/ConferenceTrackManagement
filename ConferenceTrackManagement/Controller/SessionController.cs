using ConferenceTrackManagement.Library;
using ConferenceTrackManagement.Model;
using ConferenceTrackManagement.IRepository;
using ConferenceTrackManagement.Repository;
using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement.Controller
{
    public class SessionController
    {
        private readonly ISessionRepository<Session> _sessionRepository;
        public SessionController()
        {
            _sessionRepository = new SessionRepository();
        }

        //create an instance of Session to the default session network
        public Session CreateNetworkSession(DateTime dayOfTheSession)
        {
            try
            {
                Session session = new Session() { Title = "Network", Order = 4 };
                //the network's hour has been between 4PM and 5PM
                if (dayOfTheSession.Hour < 16 || dayOfTheSession.Hour > 17)
                    throw new ArgumentOutOfRangeException(ExceptionsMessages.EXCEPTION_NETWORK_HOUR_BETWEEN_FOUR_FIVE_HOURS);
                else
                {
                    session.StartHour = dayOfTheSession;
                    //create a new scheduling 
                    Scheduling scheduling = new Scheduling()
                    {
                        StartHour = session.StartHour,
                        Session = session
                    };
                    //add a talk at a scheduling
                    scheduling.Talk = new Talk() { Title = "Network Talk", IsLightning = false };
                    //add a talk at a scheduling
                    session.SchedulingList = new List<Scheduling>();
                    session.SchedulingList.Add(scheduling);
                }
                _sessionRepository.Save(session);
                return session;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        //create an instance of Session to lunch session
        public Session CreateLunchSession(DateTime dayOfTheSession)
        {
            try
            {
                Session session = new Session() { Title = "Lunch Session", Order = 2 };
                //for definition the lunch start at 12AM
                session.StartHour = new DateTime(dayOfTheSession.Year, dayOfTheSession.Month, dayOfTheSession.Day, 12, 0, 0);
                //for definition the lunch end at 1PM
                session.EndHour = new DateTime(dayOfTheSession.Year, dayOfTheSession.Month, dayOfTheSession.Day, 13, 0, 0);

                //create a new scheduling 
                Scheduling scheduling = new Scheduling()
                {
                    StartHour = session.StartHour,
                    EndHour = session.EndHour,
                    Session = session
                };
                scheduling.Talk = new Talk() { Title = "Lunch", Duration = 60, IsLightning = false };
                session.SchedulingList = new List<Scheduling>();
                session.SchedulingList.Add(scheduling);

                _sessionRepository.Save(session);
                return session;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        //create an instance of Session to lunch session
        public Session CreateMorningSession(DateTime dayOfTheSession)
        {
            try
            {
                Session session = new Session() { Title = "Morning Session", Order = 1 };
                //for definition the morning session start at 9AM
                session.StartHour = new DateTime(dayOfTheSession.Year, dayOfTheSession.Month, dayOfTheSession.Day, 9, 0, 0);
                //for definition the morning session end at 12PM
                session.EndHour = new DateTime(dayOfTheSession.Year, dayOfTheSession.Month, dayOfTheSession.Day, 12, 0, 0);
                session.SchedulingList = new List<Scheduling>();
                _sessionRepository.Save(session);
                return session;

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        //create an instance of Session to lunch session
        public Session CreateAfternoonSession(DateTime dayOfTheSession)
        {
            try
            {
                Session session = new Session() { Title = "Afternoon Session", Order = 3 };
                //for definition the morning session start at 1PM
                session.StartHour = new DateTime(dayOfTheSession.Year, dayOfTheSession.Month, dayOfTheSession.Day, 13, 0, 0);
                session.EndHour = new DateTime(dayOfTheSession.Year, dayOfTheSession.Month, dayOfTheSession.Day, 17, 0, 0);
                session.SchedulingList = new List<Scheduling>();
                _sessionRepository.Save(session);
                return session;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
