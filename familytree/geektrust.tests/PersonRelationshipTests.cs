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
        public void AddingEdgeTest()
        {
            var personRelationship = new Relationships();
            personRelationship.AddParent(bob);
            personRelationship.Parents.Should().BeEquivalentTo(new List<PersonDTO>() { bob });
        }
    }
}
