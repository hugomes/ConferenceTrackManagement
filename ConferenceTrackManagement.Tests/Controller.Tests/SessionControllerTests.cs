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
    public class SessionControllerTests
    {
        [Test]
        public void CreateNetworkSession_NeedReturnSessionInstanceWithTitleContainsNetwork()
        {
            SessionController sessionController = new SessionController();
            Session session = sessionController.CreateNetworkSession(new DateTime(2018, 1, 1, 16, 30, 0));
            Assert.IsTrue(session.Title.Contains("Network"));
        }

        [Test]
        public void CreateLunchSession_NeedReturnSessionInstanceWithTitleContainsLunch()
        {
            SessionController sessionController = new SessionController();
            Session session = sessionController.CreateLunchSession(new DateTime(2018, 1, 1, 16, 30, 0));
            Assert.IsTrue(session.Title.Contains("Lunch"));
        }

        [Test]
        public void CreateMorningSession_NeedReturnSessionInstanceWithTitleContainsMorning()
        {
            SessionController sessionController = new SessionController();
            Session session = sessionController.CreateMorningSession(new DateTime(2018, 1, 1, 16, 30, 0));
            Assert.IsTrue(session.Title.Contains("Morning"));
        }

        [Test]
        public void CreateAfternoonSession_NeedReturnSessionInstanceWithTitleContainsAfternoon()
        {
            SessionController sessionController = new SessionController();
            Session session = sessionController.CreateAfternoonSession(new DateTime(2018, 1, 1, 16, 30, 0));
            Assert.IsTrue(session.Title.Contains("Afternoon"));
        }
    }
}
