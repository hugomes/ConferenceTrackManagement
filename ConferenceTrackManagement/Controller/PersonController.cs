using ConferenceTrackManagement.Library;
using ConferenceTrackManagement.Model;
using ConferenceTrackManagement.Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConferenceTrackManagement.IRepository;

namespace ConferenceTrackManagement.Controller
{
    public class PersonController
    {
        private IPersonRepository<Person> _personRepository;
        public PersonController()
        {
            _personRepository = new PersonRepository();
        }

        public Person AddPerson(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception(ExceptionsMessages.MESSAGE_INVALID_NAME);
            else
            {
                Person person = new Person() {Name = name};
                if (_personRepository.Save(person))
                    return person;
                else
                    throw new Exception("An error has occurred to add the Person.");
            }
        }

        public IList<Person> ListAllPersons()
        {
            return _personRepository.List();
        }

    }
}
