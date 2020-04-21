using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Implementation;
using geektrust.Family.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace geektrust.Family.Tests
{
    public class BaseRelationshipsTests
    {
        public IPersonStore PersonStore;

        Person george, mary, bob, sally, dave;

        [OneTimeSetUp]
        public void SetUp()
        {
            PersonStore = new PersonStore();
            george = PersonStore.AddPerson("George", Gender.Male);
            mary = PersonStore.AddPerson("Mary", Gender.Female);
            bob = PersonStore.AddPerson("Bob", Gender.Male);
            sally = PersonStore.AddPerson("Sally", Gender.Female);
            dave = PersonStore.AddPerson("Dave", Gender.Male);
        }

        [Test]
        public void ParentsTest()
        {
            //AddRelationship George and Mary as Bob's Parents
            FamilyGraph familyGraph = new FamilyGraph(PersonStore);
            familyGraph.AddRelationship("George", "Bob", "Parent");
            familyGraph.AddRelationship("Mary", "Bob", "Parent");
            IEnumerable<Person> actual = familyGraph.Parents(bob);
            IEnumerable<Person> expected = PersonStore.GetPeople(new List<string>() { "George", "Mary" });
            actual.Should().BeEquivalentTo(expected);

        }
        [Test]
        public void EmptyParentsTest()
        {
            //AddRelationship George and Mary as Bob's Parents
            FamilyGraph familyGraph = new FamilyGraph(PersonStore);
            IEnumerable<Person> actual = familyGraph.Parents(bob);
            IEnumerable<Person> expected = new List<Person>();
            actual.Should().BeEquivalentTo(expected);

        }
        [Test]
        public void SiblingsTest()
        {
            //AddRelationship George and Mary as Bob's Parents
            FamilyGraph familyGraph = new FamilyGraph(PersonStore);
            familyGraph.AddRelationship("George", "Bob", "Parent");
            familyGraph.AddRelationship("Mary", "Bob", "Parent");
            familyGraph.AddRelationship("George", "Dave", "Parent");
            familyGraph.AddRelationship("Mary", "Dave", "Parent");
            familyGraph.AddRelationship("George", "Sally", "Parent");
            familyGraph.AddRelationship("Mary", "Sally", "Parent");
            IEnumerable<Person> actual = familyGraph.Siblings(dave);
            IEnumerable<Person> expected = PersonStore.GetPeople(new List<string>() { "Bob", "Sally" });
            actual.Should().BeEquivalentTo(expected);
        }
        [Test]
        public void SiblingsHaveSameParentsTest()
        {
            //AddRelationship George and Mary as Bob's Parents
            FamilyGraph familyGraph = new FamilyGraph(PersonStore);
            familyGraph.AddRelationship("George", "Bob", "Parent");
            familyGraph.AddRelationship("Mary", "Bob", "Parent");
            familyGraph.AddRelationship("George", "Dave", "Parent");
            familyGraph.AddRelationship("Mary", "Dave", "Parent");
            familyGraph.AddRelationship("George", "Sally", "Parent");
            familyGraph.AddRelationship("Mary", "Sally", "Parent");
            familyGraph.Parents(dave).Should().BeEquivalentTo(familyGraph.Parents(bob));
        }
        [Test]
        public void ChildrenTest()
        {
            //AddRelationship George and Mary as Bob's Parents
            FamilyGraph familyGraph = new FamilyGraph(PersonStore);
            familyGraph.AddRelationship("George", "Bob", "Parent");
            familyGraph.AddRelationship("Mary", "Bob", "Parent");
            familyGraph.AddRelationship("George", "Dave", "Parent");
            familyGraph.AddRelationship("Mary", "Dave", "Parent");
            familyGraph.AddRelationship("George", "Sally", "Parent");
            familyGraph.AddRelationship("Mary", "Sally", "Parent");
            IEnumerable<Person> actual = familyGraph.Children(george);
            IEnumerable<Person> expected = new List<Person>() { dave, bob, sally };
            actual.Should().BeEquivalentTo(expected);
        }

    }
}
