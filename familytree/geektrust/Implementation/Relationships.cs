using geektrust.Family.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("geektrust.Tests")]
namespace geektrust.Family.Implementation
{
    internal sealed class Relationships
    {
        public List<RelationshipDTO> Edges { get; private set; }
        public List<PersonDTO> Parents { get; private set; }
        public PersonDTO Spouse { get; private set; }

        public Relationships()
        {
            Edges = new List<RelationshipDTO>();
            Parents = new List<PersonDTO>();
        }
        public Relationships(List<RelationshipDTO> edges, List<PersonDTO> persons)
        {
            Edges = edges;
            Parents = persons;
        }

        public bool CanAddParent(PersonDTO parent)
        {
            if (Parents.Count() == 2)
                return false;
            return !Parents.Any(m => m.Gender == parent.Gender);
        }

        public void AddEdge(RelationshipDTO edge)
        {
            Edges.Add(edge);
        }

        public void AddParent(PersonDTO parent)
        {
            Parents.Add(parent);
        }

        public void AddSpouse(PersonDTO spouse)
        {
            Spouse = spouse;
        }
    }
}
