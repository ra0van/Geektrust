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
            IPersonStorage personStorage = new PersonStorage();
            personStorage.AddPerson("Name", Gender.Male);
            var actual = personStorage.ContainsPerson("Name");
            actual.Should().Be(true);
        }
        [Test]
        public void AddingExistingPersonShouldThrowException()
        {
            IPersonStorage personStore = new PersonStorage();
            personStore.AddPerson("Name", Gender.Male);
            Action act = () => personStore.AddPerson("Name", Gender.Male);
            act.Should().Throw<ArgumentException>();
        }
        [Test]
        public void NonExistantPersonContainsShouldReturnFalse()
        {
            IPersonStorage personStore = new PersonStorage();
            var actual = personStore.ContainsPerson("Name");
            actual.Should().Be(false);
        }
        [Test]
        public void AddingMultiplePersonShouldGenerateValidIds()
        {
            IPersonStorage personStore = new PersonStorage();
            var person1 = personStore.AddPerson("Name1", Gender.Male);
            var person2 = personStore.AddPerson("Name2", Gender.Male);
            var person3 = personStore.AddPerson("Name3", Gender.Male);

            person1.Id.Should().Be(1);
            person2.Id.Should().Be(2);
            person3.Id.Should().Be(3);
        }
    }
}
