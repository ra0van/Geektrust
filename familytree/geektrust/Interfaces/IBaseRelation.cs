using geektrust.Family.DTO;
using geektrust.Family.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace geektrust.Family.Interfaces
{
    public interface IBaseRelation
    {
        IEnumerable<PersonDTO> Children(IEnumerable<PersonDTO> people, Gender? gender = null);
        IEnumerable<PersonDTO> Children(PersonDTO name, Gender? gender = null);
        IEnumerable<PersonDTO> Children(string name, Gender? gender = null);
        IEnumerable<PersonDTO> Parents(PersonDTO name, Gender? gender = null);
        IEnumerable<PersonDTO> Parents(string name, Gender? gender = null);
        IEnumerable<PersonDTO> Siblings(IEnumerable<PersonDTO> people, Gender? gender = null);
        IEnumerable<PersonDTO> Siblings(PersonDTO person, Gender? gender = null);
        IEnumerable<PersonDTO> Siblings(string name, Gender? gender = null);
        IEnumerable<PersonDTO> Spouse(IEnumerable<PersonDTO> people);
        IEnumerable<PersonDTO> Spouse(PersonDTO person);
        IEnumerable<PersonDTO> Spouse(string name);
    }
}

