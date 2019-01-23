using ConferenceTrackManagement.Library;
using ConferenceTrackManagement.Model;
using ConferenceTrackManagement.Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConferenceTrackManagement.IRepository;

namespace ConferenceTrackManagement.Controller
{
    public class TalkController
    {
        private ITalkRepository<Talk> _talkRepository;
        public TalkController()
        {
            _talkRepository = new TalkRepository();
        }

        public bool AddTalk(Talk talk)
        {
            if (talk.IsLightning && talk.Duration != 5)
                throw new Exception(ExceptionsMessages.EXCEPTION_LIGHTNING_NOT_EQUAL_FIVE_MINUTES);
            else if (!Regex.Match(talk.Title, "^([^0-9]*)$").Success)
                throw new Exception(ExceptionsMessages.EXCEPTION_TITLE_NO_NUMBERS);
            else
            {
                return _talkRepository.Save(talk);
            }
        }

        public bool AddTalk(string title, int duration, bool isLightning)
        {
            if (isLightning && duration != 5)
                throw new Exception(ExceptionsMessages.EXCEPTION_LIGHTNING_NOT_EQUAL_FIVE_MINUTES);
            else if (!Regex.Match(title, "^([^0-9]*)$").Success)
                throw new Exception(ExceptionsMessages.EXCEPTION_TITLE_NO_NUMBERS);
            else
            {
                return _talkRepository.Save(new Talk() { Title = title, Duration = duration, IsLightning = isLightning });
            }
        }

        public void CreateDefaultTalks()
        {
            _talkRepository.DeleteAll();
            AddTalk("Writing Fast Tests Against Enterprise Rails", 60, false);
            AddTalk("Overdoing it in Python", 45, false);
            AddTalk("Lua for the Masses", 30, false);
            AddTalk("Ruby Errors from Mismatched Gem Versions", 45, false);
            AddTalk("Common Ruby Errors", 45, false);
            AddTalk("Rails for Python Developers", 5, true);
            AddTalk("Communicating Over Distance", 60, false);
            AddTalk("Accounting-Driven Development", 45, false);
            AddTalk("Woah", 30, false);
            AddTalk("Sit Down and Write", 30, false);
            AddTalk("Pair Programming vs Noise", 45, false);
            AddTalk("Rails Magic", 60, false);
            AddTalk("Ruby on Rails: Why We Should Move On", 60, false);
            AddTalk("Clojure Ate Scala (on my project)", 45, false);
            AddTalk("Programming in the Boondocks of Seattle", 30, false);
            AddTalk("Ruby vs. Clojure for Back-End Development", 30, false);
            AddTalk("Ruby on Rails Legacy App Maintenance", 60, false);
            AddTalk("A World Without HackerNews", 30, false);
            AddTalk("User Interface CSS in Rails Apps", 30, false);
        }

        public IList<Talk> ListAllTalks()
        {
            return _talkRepository.List();
        }

        public bool AddAudience(Person person, Talk talk)
        {
            if (talk.Audience.Count >= 3)
                throw new IndexOutOfRangeException(ExceptionsMessages.MESSAGE_PERSONS_AT_TALK);
            else
            {
                if (talk.Audience == null)
                    talk.Audience = new List<Person>();
                talk.Audience.Add(person);
            }

            return true;
        }
    }
}
