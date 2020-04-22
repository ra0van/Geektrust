using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace geektrust.Family.Implementation
{
    public sealed class BaseRelationships : IRelationships
    {
        private IBaseRelation baseRelationships;
        public BaseRelationships(IBaseRelation baseRelationships)
        {
            this.baseRelationships = baseRelationships;
        }
        public IEnumerable<PersonDTO> BrotherInLaw(string person)
        {
            return InLaws(person, Gender.Male).OrderBy(m => m.Id);
        }

        public IEnumerable<PersonDTO> Daughter(string person)
        {
            return Children(person, Gender.Female).OrderBy(m => m.Id);
        }

        public IEnumerable<PersonDTO> MaternalAunt(string person)
        {
            return UncleAndAunt(person, "Maternal", "Aunt").OrderBy(m => m.Id);
        }

        public IEnumerable<PersonDTO> MaternalUncle(string person)
        {
            return UncleAndAunt(person, "Maternal", "Uncle").OrderBy(m => m.Id);
        }

        public IEnumerable<PersonDTO> PaternalAunt(string person)
        {
            return UncleAndAunt(person, "Paternal", "Aunt").OrderBy(m => m.Id);
        }

        public IEnumerable<PersonDTO> PaternalUncle(string person)
        {
            return UncleAndAunt(person, "Paternal", "Uncle").OrderBy(m => m.Id);
        }

        public IEnumerable<PersonDTO> Siblings(string person)
        {
            return baseRelationships.Siblings(person).OrderBy(m => m.Id);
        }

        public IEnumerable<PersonDTO> SisterInLaw(string person)
        {
            return InLaws(person, Gender.Female).OrderBy(m => m.Id);
        }

        public IEnumerable<PersonDTO> Son(string person)
        {
            return Children(person, Gender.Male).OrderBy(m => m.Id);
        }

        private IEnumerable<PersonDTO> UncleAndAunt(string person, string direction, string uncleOrAunt)
        {
            Gender parentsGender = direction == "Maternal" ? Gender.Female : Gender.Male;
            Gender uncleOrAuntGender = uncleOrAunt == "Aunt" ? Gender.Female : Gender.Male;
            var parents = baseRelationships.Parents(person, parentsGender);
            return baseRelationships.Siblings(parents, uncleOrAuntGender).OrderBy(m => m.Id);
        }

        private IEnumerable<PersonDTO> Children(string person, Gender gender)
        {
            return baseRelationships.Children(person, gender).OrderBy(m => m.Id);
        }

        private IEnumerable<PersonDTO> InLaws(string person, Gender gender)
        {
            var spouse = baseRelationships.Spouse(person);
            IEnumerable<PersonDTO> spouseInLaws = baseRelationships.Siblings(spouse, gender);

            var siblings = baseRelationships.Siblings(person);
            IEnumerable<PersonDTO> siblingsInLaws = baseRelationships.Spouse(siblings);

            return siblingsInLaws.Concat(spouseInLaws).OrderBy(m => m.Id);
        }
    }
}
