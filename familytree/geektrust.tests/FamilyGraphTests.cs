using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Implementation;
using geektrust.Family.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace geektrust.Family.Tests
{
    public class FamilyGraphTests
    {
        public IPersonStorage storage;
        public IFamilyGraph familyGraph;

        [OneTimeSetUp]
        public void SetUp()
        {
            storage = new PersonStorage();
            PersonDTO george = storage.AddPerson("George", Gender.Male);
            PersonDTO mary = storage.AddPerson("Mary", Gender.Female);
            PersonDTO bob = storage.AddPerson("Bob", Gender.Male);
            PersonDTO sally = storage.AddPerson("Sally", Gender.Female);
            familyGraph = new FamilyGraph(storage);
        }
        [Test]
        public void AddSpouseForExistingPeople_ShouldNotThrowException()
        {
            Action result = () => familyGraph.AddRelationship("George", "Mary", "Spouse");
            result.Should().NotThrow();
        }
        [Test]
        public void AddParentForExistingPeople_ShouldNotThrowException()
        {
            Action result = () => familyGraph.AddRelationship("Bob", "Sally", "Parent");
            result.Should().NotThrow();
        }

    }
}
