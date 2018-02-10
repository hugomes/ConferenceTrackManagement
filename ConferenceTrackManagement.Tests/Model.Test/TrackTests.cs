using ConferenceTrackManagement.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Tests.Model.Test
{
    [TestFixture]
    public class TrackTests
    {
        [Test]
        public void Track_NeedCreateFourSessionsObjectsOnInstanceClass()
        {
            Track track = new Track(new DateTime());
            Assert.AreEqual(track.SessionList.Count, 4);
        }
    }
}
