using geektrust.Family.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace geektrust.Family.Interfaces
{
    public interface IRelationships
    {
        IEnumerable<Person> PaternalUncle(string person);
        IEnumerable<Person> MaternalUncle(string person);
        IEnumerable<Person> PaternalAunt(string person);
        IEnumerable<Person> MaternalAunt(string person);
        IEnumerable<Person> SisterInLaw(string person);
        IEnumerable<Person> BrotherInLaw(string person);
        IEnumerable<Person> Son(string person);
        IEnumerable<Person> Daughter(string person);
        IEnumerable<Person> Siblings(string person);
    }
}
