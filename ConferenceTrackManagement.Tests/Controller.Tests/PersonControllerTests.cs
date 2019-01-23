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
    public class PersonControllerTests
    {
        [Test]
        public void AddPerson_NeedWriteName()
        {
            PersonController personController = new PersonController();
            Assert.That(() => personController.AddPerson(""),
                Throws.TypeOf<Exception>().With.Message.EqualTo(ExceptionsMessages.MESSAGE_INVALID_NAME));
        }

        [Test]
        public void AddPerson_NeedAddPersonObjectInRepositoryPerson()
        {
            PersonController personController = new PersonController();
            Person person = personController.AddPerson("Person test");
            Assert.Contains(person, new Collection<Person>(personController.ListAllPersons()));
        }

    }
}
