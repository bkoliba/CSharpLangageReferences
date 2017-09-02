using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EqualityTests
{
    public class ReferenceTypeEquality
    {
        [Theory]
        [InlineData("Billy", "Koliba", false)]
        [InlineData("John", "Smith", false)]
        [InlineData("Hello", "World", true)]
        void ReferenceTypeEqualityTests(string firstName, string lastName, bool expectedResult)
        {
            var person = new Person(firstName, lastName);
            var personReference = person;
            var otherPerson = new Person("Hello", "World");

            Assert.True(ReferenceEquals(person, personReference));
            Assert.True(person.Equals(personReference));
            Assert.Equal(expectedResult, person.Equals(otherPerson));
            Assert.Equal(expectedResult, person == otherPerson);
            Assert.Equal(expectedResult, person.GetHashCode() == otherPerson.GetHashCode());
        }

        [Theory]
        [InlineData("Billy", "Koliba", 1, false)]
        [InlineData("John", "Smith", 15, false)]
        [InlineData("Hello", "World", 15, false)]
        [InlineData("Hello", "World", 10, true)]
        void ReferenceWithInheritanceEqualityTests(string firstName, string lastName,int age, bool expectedResult)
        {
            var person = new PersonWithAge(firstName, lastName,age);
            var personReference = person;
            var otherPerson = new PersonWithAge("Hello", "World",10);

            Assert.True(ReferenceEquals(person, personReference));
            Assert.True(person.Equals(personReference));
            Assert.Equal(expectedResult, person.Equals(otherPerson));
            Assert.Equal(expectedResult, person == otherPerson);
            Assert.Equal(!expectedResult, person != otherPerson);
            Assert.Equal(expectedResult, person.GetHashCode() == otherPerson.GetHashCode());
            Assert.Equal(!expectedResult, person.GetHashCode() != otherPerson.GetHashCode());
        }

        //Note: virtual Equal method for reference types where the proper equal will be called when using inheritance, only use IEquatable when the class is sealed
        private class PersonWithAge : Person
        {
            public int Age { get; set; }
            public PersonWithAge(string firstName, string lastName, int age) : base(firstName, lastName)
            {
                Age = age;
            }

            public PersonWithAge(string firstName, string lastName) : base(firstName, lastName)
            {
            }

            public override bool Equals(object obj)
            {
                if (!base.Equals(obj))
                    return false;
                var agedPerson = obj as PersonWithAge;
                return agedPerson.Age == Age;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode() ^ Age.GetHashCode();
            }
            public static bool operator==(PersonWithAge firstPerson, PersonWithAge secondPerson)
            {
                return Equals(firstPerson, secondPerson);
            }

            public static bool operator!=(PersonWithAge firstPerson, PersonWithAge secondPerson)
            {
                return !Equals(firstPerson, secondPerson);
            }
        }

        private class Person
        {
            public string FirstName { get; }
            public string LastName { get; }
            public Person(string firstName, string lastName)
            {
                FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
                LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            }
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                if (ReferenceEquals(obj, this))
                    return true;
                if (obj.GetType() != this.GetType())
                    return false;
                var person = obj as Person;
                return person.FirstName == FirstName && person.LastName == LastName;
            }

            public static bool operator ==(Person firstPerson, Person secondPerson)
            {
                return Equals(firstPerson, secondPerson);
            }
            public static bool operator !=(Person firstPerson, Person secondPerson)
            {
                return !Equals(firstPerson, secondPerson);
            }

            public override int GetHashCode()
            {
                return FirstName.GetHashCode() ^ LastName.GetHashCode();
            }
        }
    }
}
