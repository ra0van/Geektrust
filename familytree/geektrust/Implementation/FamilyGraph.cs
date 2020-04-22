using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Extention;
using geektrust.Family.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace geektrust.Family.Implementation
{
    public sealed class FamilyGraph : IFamilyGraph, IBaseRelation
    {
        private Dictionary<PersonDTO, Relationships> Families;
        public IPersonStorage PersonStore { get; private set; }

        public FamilyGraph(IPersonStorage personStore)
        {
            Families = new Dictionary<PersonDTO, Relationships>();
            PersonStore = personStore;
        }

        /// <summary>
        /// Adds a relation from source person to target person of the specified type relationshipType
        /// </summary>
        /// <param name="source">Name of source person</param>
        /// <param name="target">Name of target person</param>
        /// <param name="relationshipType">Type of relationship => Parent or Spouse</param>
        public void AddRelationship(string source, string target, string relationshipType)
        {
            Relationship edge;
            try
            {
                edge = BuildRelationship(source, target, relationshipType);
            }
            catch (ArgumentException)
            {
                throw;
            }
            switch (edge.RelationshipType)
            {
                case RelationshipType.Parent:
                    AddParentRelationship(edge);
                    return;
                case RelationshipType.Spouse:
                    AddSpouseRelationship(edge);
                    return;
            }
        }
        /// <summary>
        /// Adds a new child to the mother.
        /// Expects mother to be a female and child's name to be unique
        /// </summary>
        /// <param name="motherName">Mothers Name</param>
        /// <param name="childName">Childs Name</param>
        /// <param name="gender">Gender of the child</param>
        public void AddChild(string motherName, string childName, string gender)
        {
            var mother = PersonStore.GetPeople(motherName);
            if (mother.Gender != Gender.Female)
            {
                throw new InvalidOperationException("Mother is not a female.");
            }
            var childGender = gender.ToGenderEnum();
            var child = PersonStore.AddPerson(childName, childGender);
            var motherRelationship = GetRelationship(mother);
            AddRelationship(motherName, childName, "Parent");
            if (motherRelationship.Spouse != null)
            {
                AddRelationship(motherRelationship.Spouse.Name, childName, "Parent");
            }
        }

        #region Private Methods
        private void AddSpouseRelationship(Relationship edge)
        {
            Relationships sourcePersonRelationships, targetPeopleRelationships;

            if (!Families.TryGetValue(edge.Source, out sourcePersonRelationships))
            {
                sourcePersonRelationships = new Relationships();
                Families.Add(edge.Source, sourcePersonRelationships);
            }
            if (!Families.TryGetValue(edge.Target, out targetPeopleRelationships))
            {
                targetPeopleRelationships = new Relationships();
                Families.Add(edge.Target, targetPeopleRelationships);
            }

            if (targetPeopleRelationships.Spouse == null && sourcePersonRelationships.Spouse == null)
            {
                targetPeopleRelationships.AddSpouse(edge.Source);
                sourcePersonRelationships.AddSpouse(edge.Target);
            }
            else
            {
                throw new InvalidOperationException($"Cannot add spouse");
            }
        }
        private void AddParentRelationship(Relationship edge)
        {
            Relationships sourcePersonRelationships, targetPeopleRelationships;

            if (!Families.TryGetValue(edge.Source, out sourcePersonRelationships))
            {
                sourcePersonRelationships = new Relationships();
                Families.Add(edge.Source, sourcePersonRelationships);
            }
            if (!Families.TryGetValue(edge.Target, out targetPeopleRelationships))
            {
                targetPeopleRelationships = new Relationships();
                Families.Add(edge.Target, targetPeopleRelationships);
            }

            if (targetPeopleRelationships.CanAddParent(edge.Source))
            {
                targetPeopleRelationships.AddParent(edge.Source);
                sourcePersonRelationships.AddEdge(edge);
            }
            else
            {
                throw new InvalidOperationException($"Cannot add parents to {edge.Target.Name}");
            }
        }
        private Relationships GetRelationship(PersonDTO person)
        {
            Families.TryGetValue(person, out var personRelationships);
            return personRelationships;
        }
        private Relationship BuildRelationship(string source, string target, string relationshipType)
        {
            PersonDTO sourcePerson, targetPerson;
            var relationship = relationshipType.ToRelationshipEnum();
            try
            {
                sourcePerson = PersonStore.GetPeople(source);
                targetPerson = PersonStore.GetPeople(target);
            }
            catch (Exception)
            {
                throw;
            }
            return new Relationship(sourcePerson, targetPerson, relationship);
        }
        #endregion

        #region Base Relationships
        public IEnumerable<PersonDTO> Parents(IEnumerable<PersonDTO> people, Gender? gender = null)
        {
            return people.SelectMany(m => Parents(m, gender));
        }
        public IEnumerable<PersonDTO> Parents(IEnumerable<string> people, Gender? gender = null)
        {
            var peopleObject = PersonStore.GetPeople(people);
            return Parents(peopleObject, gender);
        }
        public IEnumerable<PersonDTO> Children(IEnumerable<PersonDTO> people, Gender? gender = null)
        {
            return people.SelectMany(m => Children(m, gender));
        }
        public IEnumerable<PersonDTO> Children(IEnumerable<string> people, Gender? gender = null)
        {
            var peopleObject = PersonStore.GetPeople(people);
            return Children(peopleObject, gender);
        }
        public IEnumerable<PersonDTO> Siblings(IEnumerable<PersonDTO> people, Gender? gender = null)
        {
            return people.SelectMany(m => Siblings(m, gender));
        }
        public IEnumerable<PersonDTO> Siblings(IEnumerable<string> people, Gender? gender = null)
        {
            var peopleObject = PersonStore.GetPeople(people);
            return Siblings(peopleObject, gender);
        }
        public IEnumerable<PersonDTO> Spouse(IEnumerable<PersonDTO> people)
        {
            return people.SelectMany(m => Spouse(m));
        }
        public IEnumerable<PersonDTO> Spouse(IEnumerable<string> people)
        {
            var peopleObject = PersonStore.GetPeople(people);
            return Spouse(peopleObject);
        }

        public IEnumerable<PersonDTO> Parents(PersonDTO person, Gender? gender = null)
        {
            List<PersonDTO> result = new List<PersonDTO>();
            Relationships personRelationships = GetRelationship(person);
            if (personRelationships == null)
            {
                return result;
            }
            IEnumerable<PersonDTO> parents = personRelationships.Parents
                .Where(m => gender == null || m.Gender == gender);
            result.AddRange(parents);
            return result;
        }
        public IEnumerable<PersonDTO> Parents(string person, Gender? gender = null)
        {
            var personObject = PersonStore.GetPeople(person);
            return Parents(personObject, gender);
        }
        public IEnumerable<PersonDTO> Children(PersonDTO person, Gender? gender = null)
        {
            List<PersonDTO> result = new List<PersonDTO>();
            Relationships personRelationships = GetRelationship(person);
            List<PersonDTO> children = personRelationships.Edges
                .Where(m => m.RelationshipType == RelationshipType.Parent)
                .Where(m => gender == null || m.Target.Gender == gender)
                .Select(m => m.Target)
                .ToList();
            result.AddRange(children);
            return result;
        }
        public IEnumerable<PersonDTO> Children(string person, Gender? gender = null)
        {
            var personObject = PersonStore.GetPeople(person);
            return Children(personObject, gender);
        }
        public IEnumerable<PersonDTO> Siblings(PersonDTO person, Gender? gender = null)
        {
            var parents = Parents(person);
            var children = Children(parents, gender);
            return children.Distinct()
                    .Where(m => !m.Equals(person));
        }
        public IEnumerable<PersonDTO> Siblings(string person, Gender? gender = null)
        {
            var personObject = PersonStore.GetPeople(person);
            return Siblings(personObject, gender);
        }
        public IEnumerable<PersonDTO> Spouse(PersonDTO person)
        {
            List<PersonDTO> result = new List<PersonDTO>();
            Relationships personRelationships = GetRelationship(person);
            if (personRelationships.Spouse != null)
            {
                result.Add(personRelationships.Spouse);
            }
            return result;
        }
        public IEnumerable<PersonDTO> Spouse(string person)
        {
            var personObject = PersonStore.GetPeople(person);
            return Spouse(personObject);
        }
        #endregion
    }
}
