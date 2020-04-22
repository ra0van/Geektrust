using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace geektrust.Family.Implementation
{
    public sealed partial class FamilyGraph : IFamilyGraph, IBaseRelation
    {
        #region Base Relationships

        public IEnumerable<PersonDTO> Children(IEnumerable<PersonDTO> people, Gender? gender = null)
        {
            return people.SelectMany(m => Children(m, gender));
        }

        public IEnumerable<PersonDTO> Siblings(IEnumerable<PersonDTO> people, Gender? gender = null)
        {
            return people.SelectMany(m => Siblings(m, gender));
        }

        public IEnumerable<PersonDTO> Spouse(IEnumerable<PersonDTO> people)
        {
            return people.SelectMany(m => Spouse(m));
        }

        public IEnumerable<PersonDTO> Parents(PersonDTO person, Gender? gender = null)
        {
            List<PersonDTO> response = new List<PersonDTO>();
            Relationships personRelationships = GetRelationship(person);
            if (personRelationships == null)
            {
                return response;
            }
            IEnumerable<PersonDTO> parents = personRelationships.SourceParents
                .Where(m => gender == null || m.Gender == gender);
            response.AddRange(parents);
            return response;
        }
        public IEnumerable<PersonDTO> Parents(string person, Gender? gender = null)
        {
            var personObject = storage.GetPeople(person);
            return Parents(personObject, gender);
        }
        public IEnumerable<PersonDTO> Children(PersonDTO person, Gender? gender = null)
        {
            List<PersonDTO> response = new List<PersonDTO>();
            Relationships personRelationships = GetRelationship(person);
            List<PersonDTO> children = personRelationships.RelationEdges
                .Where(m => m.RelationshipType == geektrust.Family.Enums.Type.Parent)
                .Where(m => gender == null || m.TargetPerson.Gender == gender)
                .Select(m => m.TargetPerson)
                .ToList();
            response.AddRange(children);
            return response;
        }
        public IEnumerable<PersonDTO> Children(string name, Gender? gender = null)
        {
            var response = storage.GetPeople(name);
            return Children(response, gender);
        }
        public IEnumerable<PersonDTO> Siblings(PersonDTO person, Gender? gender = null)
        {
            var parents = Parents(person);
            var filteredResponse = Children(parents, gender);
            return filteredResponse.Distinct()
                    .Where(m => !m.Equals(person));
        }
        public IEnumerable<PersonDTO> Siblings(string name, Gender? gender = null)
        {
            var response = storage.GetPeople(name);
            return Siblings(response, gender);
        }
        public IEnumerable<PersonDTO> Spouse(PersonDTO person)
        {
            List<PersonDTO> response = new List<PersonDTO>();
            Relationships relations = GetRelationship(person);
            if (relations.Spouse != null)
            {
                response.Add(relations.Spouse);
            }
            return response;
        }
        public IEnumerable<PersonDTO> Spouse(string name)
        {
            var response = storage.GetPeople(name);
            return Spouse(response);
        }
        #endregion
    }

}