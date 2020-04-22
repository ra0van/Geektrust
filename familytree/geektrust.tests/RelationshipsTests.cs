using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Implementation;
using geektrust.Family.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Family.Tests
{
    public class RelationshipsTests
    {

        PersonDTO george, mary, bob, sally, dave, davesMaternalUncle, davesPaternalAunt, davesMaternalGrandDad,
        davesMaternalGrandMom, davesPaternalGrandDad, davesPaternalGrandMom, amy,
        bamy, miller, amysBrother, amysBrother1, amysMom, amysDad;


        BaseRelationships relationships;

        [OneTimeSetUp]
        public void SetUp()
        {
            IPersonStorage storage = new PersonStorage();

            george = storage.AddPerson("George", Gender.Male);
            mary = storage.AddPerson("Mary", Gender.Female);
            bob = storage.AddPerson("Bob", Gender.Male);
            sally = storage.AddPerson("Sally", Gender.Female);
            dave = storage.AddPerson("Dave", Gender.Male);
            davesMaternalUncle = storage.AddPerson("Hulk", Gender.Male);
            davesPaternalAunt = storage.AddPerson("Aunt", Gender.Female);
            davesMaternalGrandDad = storage.AddPerson("Thor", Gender.Male);
            davesMaternalGrandMom = storage.AddPerson("Wonder", Gender.Female);
            davesPaternalGrandDad = storage.AddPerson("Thor1", Gender.Male);
            davesPaternalGrandMom = storage.AddPerson("Wonder1", Gender.Female);

            amy = storage.AddPerson("Amy", Gender.Female);
            bamy = storage.AddPerson("Bamy", Gender.Female);
            miller = storage.AddPerson("Miller", Gender.Male);
            amysBrother = storage.AddPerson("BigB", Gender.Male);
            amysBrother1 = storage.AddPerson("ABig", Gender.Male);
            amysMom = storage.AddPerson("Miley", Gender.Female);
            amysDad = storage.AddPerson("Brad", Gender.Male);

            FamilyGraph familyGraph = new FamilyGraph(storage);

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

            relationships = new BaseRelationships(familyGraph);
        }

        [Test]
        public void MaternalUncleTest()
        {
            var davesUncle = relationships.MaternalUncle("Dave");
            var expected = new List<PersonDTO>() { davesMaternalUncle };
            davesUncle.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void MaternalUncleTest1()
        {
            var amysUncle = relationships.MaternalUncle("Amy");
            var expected = new List<PersonDTO>() { };
            amysUncle.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void PaternalAuntTest()
        {
            var davesAunt = relationships.PaternalAunt("Dave");
            var expected = new List<PersonDTO>() { davesPaternalAunt };
            davesAunt.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void MaternalAuntTest1()
        {
            var amysUncle = relationships.MaternalAunt("Amy");
            var expected = new List<PersonDTO>() { };
            amysUncle.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }

        [Test]
        public void BrotherInLawTest()
        {
            var AmysInLaws = relationships.BrotherInLaw("Amy");
            var expected = new List<PersonDTO>() { bob };
            AmysInLaws.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void SisterInLawTest()
        {
            var AmysInLaws = relationships.SisterInLaw("Amy");
            var expected = new List<PersonDTO>() { sally };
            AmysInLaws.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void BrotherInLawTest1()
        {
            var DavesInLaws = relationships.BrotherInLaw("Dave");
            var expected = new List<PersonDTO>() { miller, amysBrother, amysBrother1 };
            DavesInLaws.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void SisterInLawTest1()
        {
            var AmysInLaws = relationships.SisterInLaw("Bob");
            var expected = new List<PersonDTO>() { amy };
            AmysInLaws.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void SonTest1()
        {
            var GeorgeSons = relationships.Son("George");
            var expected = new List<PersonDTO>() { bob, dave };
            GeorgeSons.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
        [Test]
        public void DaughterTest1()
        {
            var GeorgeSons = relationships.Daughter("George");
            var expected = new List<PersonDTO>() { sally };
            GeorgeSons.Should().BeEquivalentTo(expected, m => m.WithStrictOrdering());
        }
    }
}
