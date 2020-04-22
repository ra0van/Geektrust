using geektrust.Family.DTO;
using geektrust.Family.Enums;
using System.Collections.Generic;

namespace geektrust.Family.Interfaces
{
    public interface IPersonStorage
    {
        PersonDTO AddPerson(string personName, Gender gender);
        bool ContainsPerson(string personName);
        PersonDTO GetPeople(string personName);
        IEnumerable<PersonDTO> GetPeople(IEnumerable<string> people);
    }
}
