using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Implementation;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace geektrust.Family.Tests
{
    public class PersonRelationshipTests
    {
        Person bob = new Person("Bob", Gender.Male, 1);

        [Test]
        public void AddingEdgeTest()
        {
            var personRelationship = new PersonRelationships();
            personRelationship.AddParent(bob);
            personRelationship.Parents.Should().BeEquivalentTo(new List<Person>() { bob });
        }
    }
}
