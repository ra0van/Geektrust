using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Implementation;
using geektrust.Family.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Family.Tests
{
    public class RelationshipsTests
    {

        Person george,mary,bob,sally,dave,davesMaternalUncle,davesPaternalAunt,davesMaternalGrandDad,
        davesMaternalGrandMom,davesPaternalGrandDad,davesPaternalGrandMom,amy,
        bamy,miller,amysBrother,amysBrother1,amysMom,amysDad;


       Relationships relationships;

        [OneTimeSetUp]
        public void SetUp()
        {
            IPersonStore PersonStore = new PersonStore();

            george = PersonStore.AddPerson("George", Gender.Male);
            mary = PersonStore.AddPerson("Mary", Gender.Female);
            bob = PersonStore.AddPerson("Bob", Gender.Male);
            sally = PersonStore.AddPerson("Sally", Gender.Female);
            dave = PersonStore.AddPerson("Dave", Gender.Male);
            davesMaternalUncle = PersonStore.AddPerson("Hulk", Gender.Male);
            davesPaternalAunt = PersonStore.AddPerson("Aunt", Gender.Female);
            davesMaternalGrandDad = PersonStore.AddPerson("Thor", Gender.Male);
            davesMaternalGrandMom = PersonStore.AddPerson("Wonder", Gender.Female);
            davesPaternalGrandDad = PersonStore.AddPerson("Thor1", Gender.Male);
            davesPaternalGrandMom = PersonStore.AddPerson("Wonder1", Gender.Female);

            amy = PersonStore.AddPerson("Amy", Gender.Female);
            bamy = PersonStore.AddPerson("Bamy", Gender.Female);
            miller = PersonStore.AddPerson("Miller", Gender.Male);
            amysBrother = PersonStore.AddPerson("BigB", Gender.Male);
            amysBrother1 = PersonStore.AddPerson("ABig", Gender.Male);
            amysMom = PersonStore.AddPerson("Miley", Gender.Female);
            amysDad = PersonStore.AddPerson("Brad", Gender.Male);

            FamilyGraph familyGraph = new FamilyGraph(PersonStore);

            //Daves Family
            familyGraph.AddRelationship("Thor", "Mary", "Parent");
            familyGraph.AddRelationship("Wonder", "Mary", "Parent");
            familyGraph.AddRelationship("Thor", "Hulk", "Parent");
            familyGraph.AddRelationship("Wonder", "Hulk", "Parent");

            familyGraph.AddRelationship("Thor1", "George", "Parent");
            familyGraph.AddRelationship("Wonder1", "George", "Parent");
            familyGraph.AddRelationship("Thor1", "Aunt", "Parent");
            familyGraph.AddRelationship("Wonder1", "Aunt", "Parent");

            familyGraph.AddRelationship("George", "Bob", "Parent");
            familyGraph.AddRelationship("Mary", "Bob", "Parent");
            familyGraph.AddRelationship("George", "Dave", "Parent");
            familyGraph.AddRelationship("Mary", "Dave", "Parent");
            familyGraph.AddRelationship("George", "Sally", "Parent");
            familyGraph.AddRelationship("Mary", "Sally", "Parent");


            //Spouse
            familyGraph.AddRelationship("Dave", "Amy", "Spouse");

            //Amy's Family
            familyGraph.AddRelationship("Brad", "Miller", "Parent");
            familyGraph.AddRelationship("Miley", "Miller", "Parent");
            familyGraph.AddRelationship("Brad", "ABig", "Parent");
            familyGraph.AddRelationship("Miley", "ABig", "Parent");
            familyGraph.AddRelationship("Brad", "BigB", "Parent");
            familyGraph.AddRelationship("Miley", "BigB", "Parent");
            familyGraph.AddRelationship("Brad", "Amy", "Parent");
            familyGraph.AddRelationship("Miley", "Amy", "Parent");
            familyGraph.AddRelationship("Brad", "Bamy", "Parent");
            familyGraph.AddRelationship("Miley", "Bamy", "Parent");

            relationships = new Relationships(familyGraph);
        }

        [Test]
        public void MaternalUncleTest()
        {
            var davesUncle = relationships.MaternalUncle("Dave");
            var expected = new List<Person>() { davesMaternalUncle };
            davesUncle.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void MaternalUncleTest1()
        {
            var amysUncle = relationships.MaternalUncle("Amy");
            var expected = new List<Person>() { };
            amysUncle.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void PaternalAuntTest()
        {
            var davesAunt = relationships.PaternalAunt("Dave");
            var expected = new List<Person>() { davesPaternalAunt };
            davesAunt.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void MaternalAuntTest1()
        {
            var amysUncle = relationships.MaternalAunt("Amy");
            var expected = new List<Person>() { };
            amysUncle.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }

        [Test]
        public void BrotherInLawTest()
        {
            var AmysInLaws = relationships.BrotherInLaw("Amy");
            var expected = new List<Person>() { bob };
            AmysInLaws.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void SisterInLawTest()
        {
            var AmysInLaws = relationships.SisterInLaw("Amy");
            var expected = new List<Person>() { sally };
            AmysInLaws.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void BrotherInLawTest1()
        {
            var DavesInLaws = relationships.BrotherInLaw("Dave");
            var expected = new List<Person>() { miller, amysBrother, amysBrother1 };
            DavesInLaws.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void SisterInLawTest1()
        {
            var AmysInLaws = relationships.SisterInLaw("Bob");
            var expected = new List<Person>() { amy };
            AmysInLaws.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void SonTest1()
        {
            var GeorgeSons = relationships.Son("George");
            var expected = new List<Person>() { bob, dave };
            GeorgeSons.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void DaughterTest1()
        {
            var GeorgeSons = relationships.Daughter("George");
            var expected = new List<Person>() { sally };
            GeorgeSons.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
    }
}
