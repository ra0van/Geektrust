using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace geektrust.Family.Implementation
{
    public class PersonStorage : IPersonStorage
    {
        private Dictionary<string, Person> peopleStore;
        private int Id;
        private static object Lock = new object();

        public PersonStorage()
        {
            peopleStore = new Dictionary<string, Person>();
            Id = 1;
        }

        public Person AddPerson(string personName, Gender gender)
        {
            Person person;
            if (ContainsPerson(personName))
            {
                throw new ArgumentException($"{personName} is already present");
            }
            lock (Lock)
            {
                person = new Person(personName, gender, Id);
                peopleStore.Add(personName, person);
                Interlocked.Increment(ref Id);
            }
            return person;
        }

        public bool ContainsPerson(string personName)
        {
            return peopleStore.ContainsKey(personName);
        }

        public IEnumerable<Person> GetPeople(IEnumerable<string> people)
        {
            List<Person> result = new List<Person>();
            foreach (var person in people)
            {
                try
                {
                    Person personObject = GetPeople(person);
                    result.Add(personObject);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        public Person GetPeople(string personName)
        {
            bool result = peopleStore.TryGetValue(personName, out Person person);
            if (!result)
            {
                throw new ArgumentException($"{personName} isn't found");
            }
            return person;
        }
    }
}
