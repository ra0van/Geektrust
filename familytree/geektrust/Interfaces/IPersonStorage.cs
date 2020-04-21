using geektrust.Family.DTO;
using geektrust.Family.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace geektrust.Family.Interfaces
{
    public interface IPersonStorage
    {
        Person AddPerson(string personName, Gender gender);
        bool ContainsPerson(string personName);
        Person GetPeople(string personName);
        IEnumerable<Person> GetPeople(IEnumerable<string> people);
    }
}
