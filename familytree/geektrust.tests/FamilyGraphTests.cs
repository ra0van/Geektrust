using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Implementation;
using geektrust.Family.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
            Person george = storage.AddPerson("George", Gender.Male);
            Person mary = storage.AddPerson("Mary", Gender.Female);
            Person bob = storage.AddPerson("Bob", Gender.Male);
            Person sally = storage.AddPerson("Sally", Gender.Female);
            familyGraph = new FamilyGraph(storage);
        }
        [Test]
        public void AddSpouseForExistingPeopleShouldNotThrowException()
        {
            Action act = () => familyGraph.AddRelationship("George", "Mary", "Spouse");
            act.Should().NotThrow();
        }
        [Test]
        public void AddParentForExistingPeopleShouldNotThrowException()
        {
            Action act = () => familyGraph.AddRelationship("Bob", "Sally", "Parent");
            act.Should().NotThrow();
        }

    }
}
