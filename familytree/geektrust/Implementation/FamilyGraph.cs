using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Extention;
using geektrust.Family.Interfaces;
using System;
using System.Collections.Generic;

namespace geektrust.Family.Implementation
{
    public sealed partial class FamilyGraph : IFamilyGraph, IBaseRelation
    {
        private Dictionary<PersonDTO, Relationships> Families;
        public IPersonStorage storage { get; private set; }

        public FamilyGraph(IPersonStorage personStore)
        {
            Families = new Dictionary<PersonDTO, Relationships>();
            storage = personStore;
        }

        /// <summary>
        /// Adds a relation from source person to target person of the specified type relationshipType
        /// </summary>
        /// <param name="source">Name of source person</param>
        /// <param name="target">Name of target person</param>
        /// <param name="relationshipType">Type of relationship => Parent or Spouse</param>
        public void AddRelationship(string source, string target, string relationshipType)
        {
            RelationshipDTO edge;
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
                case Enums.Type.Parent:
                    AddParentRelationship(edge);
                    return;
                case Enums.Type.Spouse:
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
            var mother = storage.GetPeople(motherName);
            if (mother.Gender != Gender.Female)
            {
                throw new InvalidOperationException("Mother is not a female.");
            }
            var childGender = gender.ToGender();
            var child = storage.AddPerson(childName, childGender);
            var motherRelationship = GetRelationship(mother);
            AddRelationship(motherName, childName, "Parent");
            if (motherRelationship.Spouse != null)
            {
                AddRelationship(motherRelationship.Spouse.Name, childName, "Parent");
            }
        }

        #region Private Methods
        private void AddSpouseRelationship(RelationshipDTO edge)
        {
            Relationships sourcePersonRelationships, targetPeopleRelationships;

            if (!Families.TryGetValue(edge.SourcePerson, out sourcePersonRelationships))
            {
                sourcePersonRelationships = new Relationships();
                Families.Add(edge.SourcePerson, sourcePersonRelationships);
            }
            if (!Families.TryGetValue(edge.TargetPerson, out targetPeopleRelationships))
            {
                targetPeopleRelationships = new Relationships();
                Families.Add(edge.TargetPerson, targetPeopleRelationships);
            }

            if (targetPeopleRelationships.Spouse == null && sourcePersonRelationships.Spouse == null)
            {
                targetPeopleRelationships.AddSpouse(edge.SourcePerson);
                sourcePersonRelationships.AddSpouse(edge.TargetPerson);
            }
            else
            {
                throw new InvalidOperationException($"Cannot add spouse");
            }
        }
        private void AddParentRelationship(RelationshipDTO edge)
        {
            Relationships sourcePersonRelationships, targetPeopleRelationships;

            if (!Families.TryGetValue(edge.SourcePerson, out sourcePersonRelationships))
            {
                sourcePersonRelationships = new Relationships();
                Families.Add(edge.SourcePerson, sourcePersonRelationships);
            }
            if (!Families.TryGetValue(edge.TargetPerson, out targetPeopleRelationships))
            {
                targetPeopleRelationships = new Relationships();
                Families.Add(edge.TargetPerson, targetPeopleRelationships);
            }

            if (targetPeopleRelationships.CanAddParent(edge.SourcePerson))
            {
                targetPeopleRelationships.AddParent(edge.SourcePerson);
                sourcePersonRelationships.AddEdge(edge);
            }
            else
            {
                throw new InvalidOperationException($"Cannot add parents to {edge.TargetPerson.Name}");
            }
        }
        private Relationships GetRelationship(PersonDTO person)
        {
            Families.TryGetValue(person, out var personRelationships);
            return personRelationships;
        }
        private RelationshipDTO BuildRelationship(string source, string target, string relationshipType)
        {
            PersonDTO sourcePerson, targetPerson;
            var relationship = relationshipType.ToRelation();
            try
            {
                sourcePerson = storage.GetPeople(source);
                targetPerson = storage.GetPeople(target);
            }
            catch (Exception)
            {
                throw;
            }
            return new RelationshipDTO(sourcePerson, targetPerson, relationship);
        }
        #endregion
    }
}
