using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Implementation;
using geektrust.Family.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace geektrust.Tests
{
    public class PersonStoreTests
    {
        [Test]
        public void AddNewPerson()
        {
            IPersonStorage storage = new PersonStorage();
            storage.AddPerson("Name", Gender.Male);
            var response = storage.ContainsPerson("Name");
            response.Should().Be(true);
        }
        [Test]
        public void AddingExistingPersonShouldThrowException()
        {
            IPersonStorage storage = new PersonStorage();
            storage.AddPerson("Name", Gender.Male);
            Action act = () => storage.AddPerson("Name", Gender.Male);
            act.Should().Throw<ArgumentException>();
        }
        [Test]
        public void NonExistantPersonContainsShouldReturnFalse()
        {
            IPersonStorage storage = new PersonStorage();
            var actual = storage.ContainsPerson("Name");
            actual.Should().Be(false);
        }
        [Test]
        public void AddingMultiplePeople_ShouldGenerateValidIds()
        {
            IPersonStorage storage = new PersonStorage();
            var person1 = storage.AddPerson("Name1", Gender.Male);
            var person2 = storage.AddPerson("Name2", Gender.Male);
            var person3 = storage.AddPerson("Name3", Gender.Male);

            person1.Id.Should().Be(1);
            person2.Id.Should().Be(2);
            person3.Id.Should().Be(3);
        }
    }
}
