using geektrust.Family.DTO;
using System.Collections.Generic;

namespace geektrust.Family.Interfaces
{
    public interface IRelationships
    {
        IEnumerable<PersonDTO> PaternalUncle(string person);
        IEnumerable<PersonDTO> MaternalUncle(string person);
        IEnumerable<PersonDTO> PaternalAunt(string person);
        IEnumerable<PersonDTO> MaternalAunt(string person);
        IEnumerable<PersonDTO> SisterInLaw(string person);
        IEnumerable<PersonDTO> BrotherInLaw(string person);
        IEnumerable<PersonDTO> Son(string person);
        IEnumerable<PersonDTO> Daughter(string person);
        IEnumerable<PersonDTO> Siblings(string person);
    }
}
