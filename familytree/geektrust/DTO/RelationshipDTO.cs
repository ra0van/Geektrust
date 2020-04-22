using geektrust.Family.Enums;
using System;

namespace geektrust.Family.DTO
{
    internal sealed class RelationshipDTO
    {
        public RelationshipDTO(PersonDTO source, PersonDTO target, Enums.Type relationshipType)
        {
            SourcePerson = source ?? throw new ArgumentNullException(nameof(source));
            TargetPerson = target ?? throw new ArgumentNullException(nameof(target));
            RelationshipType = relationshipType;
        }

        public PersonDTO SourcePerson { get; }
        public PersonDTO TargetPerson { get; }
        public Enums.Type RelationshipType { get; }
    }
}
