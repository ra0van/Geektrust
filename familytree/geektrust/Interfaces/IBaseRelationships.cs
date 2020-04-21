using geektrust.Family.DTO;
using geektrust.Family.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace geektrust.Family.Interfaces
{
    public interface IBaseRelationships
    {
        IEnumerable<Person> Children(IEnumerable<Person> people, Gender? gender = null);
        IEnumerable<Person> Children(IEnumerable<string> people, Gender? gender = null);
        IEnumerable<Person> Children(Person person, Gender? gender = null);
        IEnumerable<Person> Children(string person, Gender? gender = null);
        IEnumerable<Person> Parents(IEnumerable<Person> people, Gender? gender = null);
        IEnumerable<Person> Parents(IEnumerable<string> people, Gender? gender = null);
        IEnumerable<Person> Parents(Person person, Gender? gender = null);
        IEnumerable<Person> Parents(string person, Gender? gender = null);
        IEnumerable<Person> Siblings(IEnumerable<Person> people, Gender? gender = null);
        IEnumerable<Person> Siblings(IEnumerable<string> people, Gender? gender = null);
        IEnumerable<Person> Siblings(Person person, Gender? gender = null);
        IEnumerable<Person> Siblings(string person, Gender? gender = null);
        IEnumerable<Person> Spouse(IEnumerable<Person> people);
        IEnumerable<Person> Spouse(IEnumerable<string> people);
        IEnumerable<Person> Spouse(Person person);
        IEnumerable<Person> Spouse(string person);
    }
}

