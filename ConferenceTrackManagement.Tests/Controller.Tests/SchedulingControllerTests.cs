using ConferenceTrackManagement.Controller;
using ConferenceTrackManagement.Library;
using ConferenceTrackManagement.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Tests.Controller.Tests
{
    [TestFixture]
    public class SchedulingControllerTests
    {
        [Test]
        public void SchedulingTalks_NeedReturnOneTrackListWithAtLeastOneTrack()
        {
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            SchedulingController schedulingController = new SchedulingController();
            List<Track> trackList = schedulingController.ScheduleTalks(new DateTime().Date);

            Assert.Greater(trackList.Count, 0);
        }

        [Test]
        public void SchedulingTalks_TheSessionsMorningAndAfternoonOfTheBestSchedulingNeedTheSameNumberOfTalkThanTalkList()
        {
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            Talk talk = new Talk() { Title = "Talk test", Duration = 30, IsLightning = false };
            talkController.AddTalk(talk);
            int totalTalk = talkController.ListAllTalks().Count;

            SchedulingController schedulingController = new SchedulingController();
            List<Track> trackList = schedulingController.ScheduleTalks(new DateTime().Date);
            int qtdTalkInDefaultSchedule = 0;
            foreach (Track track in trackList)
            {
                foreach (Session session in track.SessionList)
                {
                    if (session.Title.Contains("Morning") || session.Title.Contains("Afternoon"))
                    {
                        foreach (Scheduling scheduling in session.SchedulingList)
                        {
                            if (scheduling.Talk != null)
                                qtdTalkInDefaultSchedule++;
                        }

                    }
                }
            }

            Assert.AreEqual(qtdTalkInDefaultSchedule, totalTalk);
        }

    }
}
