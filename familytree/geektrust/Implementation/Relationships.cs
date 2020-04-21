using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace geektrust.Family.Implementation
{
    public sealed class Relationships : IRelationships
    {
        private IBaseRelationships BaseRelationships;
        public Relationships(IBaseRelationships baseRelationships)
        {
            BaseRelationships = baseRelationships;
        }
        public IEnumerable<Person> BrotherInLaw(string person)
        {
            return InLaws(person, Gender.Male).OrderBy(m => m.Id);
        }

        public IEnumerable<Person> Daughter(string person)
        {
            return Children(person, Gender.Female).OrderBy(m => m.Id);
        }

        public IEnumerable<Person> MaternalAunt(string person)
        {
            return UncleAndAunt(person, "Maternal", "Aunt").OrderBy(m => m.Id); 
        }

        public IEnumerable<Person> MaternalUncle(string person)
        {
            return UncleAndAunt(person, "Maternal", "Uncle").OrderBy(m => m.Id);
        }

        public IEnumerable<Person> PaternalAunt(string person)
        {
            return UncleAndAunt(person, "Paternal", "Aunt").OrderBy(m => m.Id); 
        }

        public IEnumerable<Person> PaternalUncle(string person)
        {
            return UncleAndAunt(person, "Paternal", "Uncle").OrderBy(m => m.Id); 
        }

        public IEnumerable<Person> Siblings(string person)
        {
            return BaseRelationships.Siblings(person).OrderBy(m => m.Id);
        }

        public IEnumerable<Person> SisterInLaw(string person)
        {
            return InLaws(person, Gender.Female).OrderBy(m => m.Id);
        }

        public IEnumerable<Person> Son(string person)
        {
            return Children(person, Gender.Male).OrderBy(m => m.Id);
        }

        private IEnumerable<Person> UncleAndAunt(string person, string direction, string uncleOrAunt)
        {
            Gender parentsGender = direction == "Maternal" ? Gender.Female : Gender.Male;
            Gender uncleOrAuntGender = uncleOrAunt == "Aunt" ? Gender.Female : Gender.Male;
            var parents = BaseRelationships.Parents(person, parentsGender);
            return BaseRelationships.Siblings(parents, uncleOrAuntGender).OrderBy(m => m.Id); 
        }

        private IEnumerable<Person> Children(string person, Gender gender)
        {
            return BaseRelationships.Children(person, gender).OrderBy(m => m.Id);
        }

        private IEnumerable<Person> InLaws(string person, Gender gender)
        {
            var spouse = BaseRelationships.Spouse(person);
            IEnumerable<Person> spouseInLaws = BaseRelationships.Siblings(spouse, gender);

            var siblings = BaseRelationships.Siblings(person);
            IEnumerable<Person> siblingsInLaws = BaseRelationships.Spouse(siblings);

            return siblingsInLaws.Concat(spouseInLaws).OrderBy(m => m.Id);
        }
    }
}
