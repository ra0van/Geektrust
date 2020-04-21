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
            IPersonStore personStore = new PersonStore();
            personStore.AddPerson("Name", Gender.Male);
            var actual = personStore.ContainsPerson("Name");
            actual.Should().Be(true);
        }
        [Test]
        public void AddingExistingPersonShouldThrowException()
        {
            IPersonStore personStore = new PersonStore();
            personStore.AddPerson("Name", Gender.Male);
            Action act = () => personStore.AddPerson("Name", Gender.Male);
            act.Should().Throw<ArgumentException>();
        }
        [Test]
        public void NonExistantPersonContainsShouldReturnFalse()
        {
            IPersonStore personStore = new PersonStore();
            var actual = personStore.ContainsPerson("Name");
            actual.Should().Be(false);
        }
        [Test]
        public void AddingMultiplePersonShouldGenerateValidIds()
        {
            IPersonStore personStore = new PersonStore();
            var person1 = personStore.AddPerson("Name1", Gender.Male);
            var person2 = personStore.AddPerson("Name2", Gender.Male);
            var person3 = personStore.AddPerson("Name3", Gender.Male);
            
            person1.Id.Should().Be(1);
            person2.Id.Should().Be(2);
            person3.Id.Should().Be(3);
        }
    }
}
