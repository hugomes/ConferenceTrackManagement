using ConferenceTrackManagement.Controller;
using ConferenceTrackManagement.Library;
using ConferenceTrackManagement.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Tests.Controller.Tests
{
    [TestFixture]
    public class TalkControllerTests
    {
        [Test]
        public void AddTalk_LightlingTalkHaveNeedFiveMinutes()
        {
            TalkController talkController = new TalkController();
            Assert.That(() => talkController.AddTalk("Talk Test", 30, true),
                Throws.TypeOf<Exception>().With.Message.EqualTo(ExceptionsMessages.EXCEPTION_LIGHTNING_NOT_EQUAL_FIVE_MINUTES));
        }

        [Test]
        public void AddTalk_TitleDontHaveNumbers_WithParameters()
        {
            TalkController talkController = new TalkController();
            Assert.That(() => talkController.AddTalk("Talk1 Test", 30, false),
                Throws.TypeOf<Exception>().With.Message.EqualTo(ExceptionsMessages.EXCEPTION_TITLE_NO_NUMBERS));
        }

        [Test]
        public void AddTalk_TitleDontHaveNumbers_WithObject()
        {
            TalkController talkController = new TalkController();
            Assert.That(() => talkController.AddTalk(new Talk() { Title = "Talk1 Test", Duration = 30, IsLightning = false }),
                Throws.TypeOf<Exception>().With.Message.EqualTo(ExceptionsMessages.EXCEPTION_TITLE_NO_NUMBERS));
        }

        [Test]
        public void AddTalk_NeedAddTalkObjectInRepositoryTalk()
        {
            Talk talk = new Talk() { Title = "Talk test", Duration = 30, IsLightning = false };
            TalkController talkController = new TalkController();
            talkController.AddTalk(talk);
            Assert.Contains(talk, new Collection<Talk>(talkController.ListAllTalks()));
        }

        [Test]
        public void AddTalk_AfterDefaultTalkAdded_NeedAddTalkObjectInRepositoryTalk()
        {
            Talk talk = new Talk() { Title = "Talk test", Duration = 30, IsLightning = false };
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            talkController.AddTalk(talk);
            Assert.Contains(talk, new Collection<Talk>(talkController.ListAllTalks()));
        }

        [Test]
        public void CreateDefaultTalks_NeedSaveNineteenTalksInRepository()
        {
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            Assert.AreEqual(talkController.ListAllTalks().Count, 19);
        }

        [Test]
        public void ListAllTalks_NeedListMoreThenOneTalks()
        {
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            Assert.Greater(talkController.ListAllTalks().Count, 0);
        }

        [Test]
        public void InsertAudienceTalk_NeedInsertUpTo3PersonsAtTalk()
        {
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            IList<Talk> talkList = talkController.ListAllTalks();
            talkController.AddAudience(new Person() {Name = "Hugo"}, talkList[0]);
            talkController.AddAudience(new Person() {Name = "Camila"}, talkList[0]);
            talkController.AddAudience(new Person() {Name = "Diana"}, talkList[0]);
            Assert.That(() => talkController.AddAudience(new Person() { Name = "Arthur" }, talkList[0]),
                Throws.TypeOf<IndexOutOfRangeException>().With.Message.EqualTo(ExceptionsMessages.MESSAGE_PERSONS_AT_TALK));
        }

        [Test]
        public void InsertAudienceTalk_InsertPersonAtTalk()
        {
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            IList<Talk> talkList = talkController.ListAllTalks();
            Person person = new Person() {Name = "Hugo"};
            talkList[0].Audience.Add(person);
            Assert.Contains(person, new Collection<Person>(talkList[0].Audience));
        }

        [Test]
        public void GetTalkByIndex_SelectOneTalkByIndexArray()
        {
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            Assert.IsInstanceOf(typeof(Talk), talkController.GetTalkByIndex(18));
        }

        [Test]
        public void GetTalkByIndex_SelectOneTalkOutOfIndexArray()
        {
            TalkController talkController = new TalkController();
            talkController.CreateDefaultTalks();
            Assert.That(() => talkController.GetTalkByIndex(talkController.ListAllTalks().Count),
                Throws.TypeOf<IndexOutOfRangeException>().With.Message.EqualTo(ExceptionsMessages.MESSAGE_INVALID_TALK));
        }
    }
}
