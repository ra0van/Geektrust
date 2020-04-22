using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Implementation;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace geektrust.Family.Tests
{
    public class PersonRelationshipTests
    {
        PersonDTO bob = new PersonDTO("Bob", Gender.Male, 1);

        [Test]
        public void AddingEdge_Test()
        {
            var relation = new Relationships();
            relation.AddParent(bob);
            relation.SourceParents.Should().BeEquivalentTo(new List<PersonDTO>() { bob });
        }
    }
}
