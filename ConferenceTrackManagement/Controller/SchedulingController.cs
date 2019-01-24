using ConferenceTrackManagement.Library;
using ConferenceTrackManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceTrackManagement.IRepository;
using ConferenceTrackManagement.Repository;

namespace ConferenceTrackManagement.Controller
{
    public class SchedulingController
    {
        private readonly ITalkRepository<Talk> _talkRepository;
        public SchedulingController()
        {
            _talkRepository = new TalkRepository();
        }

        List<List<Talk>> listTalkMother = new List<List<Talk>>();
        public List<Track> ScheduleTalks(DateTime dayOfTheConference)
        {
            listTalkMother.Clear();
            //get all talks
            IList<Talk> talkList = _talkRepository.List();

            List<Task> tasksList = new List<Task>();
            int numberLoops = 1000;
            for (int i = 0; i < numberLoops; i++)
            {
                //shuffle talks to try a new order of talks
                talkList.Shuffle();
                bool talkListExist = false;
                foreach(List<Talk> talkMother in listTalkMother)
                {
                    //verify if exist a talk list with the same order
                    if (talkList.SequenceEqual(talkMother))
                    {
                        talkListExist = true;
                        break;
                    }
                }

                if (talkListExist == false)
                {
                    listTalkMother.Add(new List<Talk>(talkList));
                    //calculate fitness in parallel to improve performance
                    Task task = new Task(() => Fitness(talkList, dayOfTheConference));
                    task.Start();
                    tasksList.Add(task);
                }
            }
            Task.WaitAll(tasksList.ToArray());
            smallerFitness = 100000;
            return bestTrackList;
        }

        private static List<Track> bestTrackList = new List<Track>(); //used to get the best
        private static int smallerFitness = 100000;//used to find the best schedule for the event
        //calculate de fitness number to find the better hours
        private static void Fitness(IList<Talk> talkList, DateTime dayOfTheConference) {
            int fitnessScore = 0;
            List<Track> trackList = new List<Track>();
            int? indexNextTrack = 0;
            int startIndexNextScheduling = 0;
            int indexScheduling = 0;
            int minutesToNextSession = 0;
            while (indexNextTrack != null)
            {
                //punishment for needing one more day to schedule the talks
                fitnessScore += (60 * 8);
                //create a day track
                Track track = new Track(dayOfTheConference) { Title = "Track Day "+(indexNextTrack+1), Date = dayOfTheConference };

                DateTime lastEndHourScheduleOfTheSession = new DateTime();

                //get only the session with index 0(Morning) and 2(Afternoon)
                for (int indexSession = 0; indexSession < 3; indexSession += 2)
                {
                    //the first scheduling start at same hour of the first session
                    DateTime startHourNextScheduling = track.SessionList[indexSession].StartHour;
                    for (indexScheduling = startIndexNextScheduling; indexScheduling < talkList.Count; indexScheduling++)
                    {
                        fitnessScore += talkList[indexScheduling].Duration;
                        //schedule end hour is equal the start hour more the talk duration
                        DateTime endHourScheduling = startHourNextScheduling.AddMinutes(talkList[indexScheduling].Duration);
                        //if scheduling end hour is small or equal than session end hour, add schedduling on session
                        if (endHourScheduling <= track.SessionList[indexSession].EndHour)
                        {
                            //create a new scheduling for talk
                            Scheduling scheduling = new Scheduling()
                            {
                                Talk = talkList[indexScheduling],
                                StartHour = startHourNextScheduling,
                                Session = track.SessionList[indexSession],
                                EndHour = endHourScheduling
                            };
                            //the next schedule start hour begin exactly on the same hour of the end hour of the current scheduling
                            startHourNextScheduling = endHourScheduling;
                            lastEndHourScheduleOfTheSession = endHourScheduling;
                            track.SessionList[indexSession].SchedulingList.Add(scheduling);
                            //will get the minutes remaining from the last scheduling until the next session
                            minutesToNextSession = endHourScheduling.Subtract(track.SessionList[indexSession + 1].StartHour).Minutes;
                        }
                        else
                        {
                            //if scheduling end hour is greather than session end hour, go to next session and get the current scheduling to start
                            startIndexNextScheduling = indexScheduling;
                            break;
                        }
                    }

                    fitnessScore += minutesToNextSession;
                    //get out of sessions loop if the index of scheduling list is bigger than number of talks
                    if (indexScheduling >= talkList.Count)
                        break;
                }

                if (indexScheduling >= talkList.Count)
                    indexNextTrack = null;
                else
                    indexNextTrack++;

                DateTime networkStartHour = new DateTime(lastEndHourScheduleOfTheSession.Year, lastEndHourScheduleOfTheSession.Month, lastEndHourScheduleOfTheSession.Day, 16, 00, 0);
                if (lastEndHourScheduleOfTheSession < networkStartHour)
                {
                    track.SessionList[3].StartHour = track.SessionList[3].SchedulingList[0].StartHour = networkStartHour;
                    trackList.Add(track);
                }
                else
                {
                    //get last scheduling end hour to put on network start hour
                    track.SessionList[3].StartHour = track.SessionList[3].SchedulingList[0].StartHour = lastEndHourScheduleOfTheSession;
                    trackList.Add(track);
                }
            }

            if (fitnessScore < smallerFitness)
            {
                smallerFitness = fitnessScore;
                bestTrackList = trackList;
            }
        }
    }
}
