using geektrust.Family.Enums;
using System;
using System.Collections.Generic;

namespace geektrust.Family.DTO
{
    public sealed class PersonDTO : IEquatable<PersonDTO>
    {
        public PersonDTO(string name, Gender gender, int id)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Name = name;
            Gender = gender;
            this.id = id;
        }

        public string Name { get; }
        public Gender Gender { get; }
        public int Id => id;
        private readonly int id;

        public override bool Equals(object obj)
        {
            return Equals(obj as PersonDTO);
        }

        public bool Equals(PersonDTO other)
        {
            bool equals = other != null &&
                   Name == other.Name &&
                   Gender == other.Gender &&
                   Id == other.Id;
            return equals;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Gender, Id);
        }

        public static bool operator ==(PersonDTO person1, PersonDTO person2)
        {
            return EqualityComparer<PersonDTO>.Default.Equals(person1, person2);
        }

        public static bool operator !=(PersonDTO person1, PersonDTO person2)
        {
            return !(person1 == person2);
        }
    }
}
