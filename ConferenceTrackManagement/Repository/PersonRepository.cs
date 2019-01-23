using ConferenceTrackManagement.Model;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement.Repository
{
    public class PersonRepository : IRepository.IPersonRepository<Person>
    {
        public bool Delete(Person obj)
        {
            return !DataBaseInMemory.DataBasePerson.Remove(obj);
        }

        public Person Get(Person obj)
        {
            return (Person)DataBaseInMemory.DataBasePerson.Where(a=>a.Name.Contains(obj.Name));
        }

        public IList<Person> List()
        {
            return DataBaseInMemory.DataBasePerson;
        }

        public bool Save(Person obj)
        {
            DataBaseInMemory.DataBasePerson.Add(obj);
            return DataBaseInMemory.DataBasePerson.Contains(obj);
        }
    }
}
