using geektrust.Family.Enums;
using System;

namespace geektrust.Family.DTO
{
    internal sealed class Relationship
    {
        public Relationship(PersonDTO source, PersonDTO target, RelationshipType relationshipType)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            RelationshipType = relationshipType;
        }

        public PersonDTO Source { get; }
        public PersonDTO Target { get; }
        public RelationshipType RelationshipType { get; }
    }
}
