using geektrust.Family.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("geektrust.Tests")]
namespace geektrust.Family.Implementation
{
    internal sealed class Relationships
    {
        public List<RelationshipDTO> RelationEdges { get; private set; }
        public List<PersonDTO> SourceParents { get; private set; }
        public PersonDTO Spouse { get; private set; }

        public Relationships()
        {
            RelationEdges = new List<RelationshipDTO>();
            SourceParents = new List<PersonDTO>();
        }
        public Relationships(List<RelationshipDTO> edges, List<PersonDTO> persons)
        {
            RelationEdges = edges;
            SourceParents = persons;
        }

        public bool CanAddParent(PersonDTO parent)
        {
            if (SourceParents.Count() == 2)
                return false;
            return !SourceParents.Any(m => m.Gender == parent.Gender);
        }

        public void AddEdge(RelationshipDTO edge)
        {
            RelationEdges.Add(edge);
        }

        public void AddParent(PersonDTO parent)
        {
            SourceParents.Add(parent);
        }

        public void AddSpouse(PersonDTO spouse)
        {
            Spouse = spouse;
        }
    }
}
