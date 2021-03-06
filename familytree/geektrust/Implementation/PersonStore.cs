﻿using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace geektrust.Family.Implementation
{
    public class PersonStorage : IPersonStorage
    {
        private Dictionary<string, PersonDTO> storage;
        private int Id;
        private static object sharedStorage = new object();

        public PersonStorage()
        {
            storage = new Dictionary<string, PersonDTO>();
            Id = 1;
        }

        public PersonDTO AddPerson(string personName, Gender gender)
        {
            PersonDTO person;
            if (ContainsPerson(personName))
            {
                throw new ArgumentException($"{personName} is already present");
            }
            lock (sharedStorage)
            {
                person = new PersonDTO(personName, gender, Id);
                storage.Add(personName, person);
                Interlocked.Increment(ref Id);
            }
            return person;
        }

        public bool ContainsPerson(string personName)
        {
            return storage.ContainsKey(personName);
        }

        public IEnumerable<PersonDTO> GetPeople(IEnumerable<string> people)
        {
            List<PersonDTO> response = new List<PersonDTO>();
            foreach (var person in people)
            {
                try
                {
                    PersonDTO personObject = GetPeople(person);
                    response.Add(personObject);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }

        public PersonDTO GetPeople(string name)
        {
            bool result = storage.TryGetValue(name, out PersonDTO response);
            if (!result)
            {
                throw new ArgumentException($"{name} isn't found");
            }
            return response;
        }
    }
}
