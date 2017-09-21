using System;
using Xunit;

namespace EqualityTests
{
    [Trait("Equality", "Reference Type Equality")]
    public class EqualityReferenceTypeTests
    {
        //When wanting to implement equality for a reference type, you would want to perform the following.
        //-	Override object.Equals() (Equality logic should go here because it is virtual)
        //-	Override object.GetHashCode()
        //-	Implement == and != overloads
        //-	IEquatable<T> if the class is sealed (It is not recommended to apply IEquatable<T> because of inheritance, which makes it hard to strong type equality)
        private class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

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

        [Fact]
        void WhenPersonReferenceAreEqual_ShouldBeEqualForAll()
        {
            var person = new Person() { FirstName = "John", LastName = "Smith" };
            var samePerson = person;

            Assert.True(person.Equals(samePerson));
            Assert.True(person == samePerson);
            Assert.False(person != samePerson);
            Assert.True(person.GetHashCode() == samePerson.GetHashCode());
            Assert.True(Equals(person, samePerson));
            Assert.True(ReferenceEquals(person, samePerson));
        }

        [Fact]
        void WhenPersonFieldsAreEqual_ShouldBeEqual()
        {
            var person = new Person() { FirstName = "John", LastName = "Smith" };
            var samePerson = new Person() { FirstName = "John", LastName = "Smith" };

            Assert.True(person.Equals(samePerson));
            Assert.True(person == samePerson);
            Assert.False(person != samePerson);
            Assert.True(person.GetHashCode() == samePerson.GetHashCode());
            Assert.True(Equals(person, samePerson));
            Assert.False(ReferenceEquals(person, samePerson));
        }

        [Fact]
        void WhenPersonFieldsNotTheSame_ShouldNotBeEqual()
        {
            var person = new Person() { FirstName = "John", LastName = "Smith" };
            var otherPerson = new Person() { FirstName = "Bob", LastName = "Smith" };

            Assert.False(person.Equals(otherPerson));
            Assert.False(person == otherPerson);
            Assert.True(person != otherPerson);
            Assert.False(person.GetHashCode() == otherPerson.GetHashCode());
            Assert.False(Equals(person, otherPerson));
            Assert.False(ReferenceEquals(person, otherPerson));
        }

        //IEquatable<T> Equals can be implemented because it is sealed, which means the only non null type would have to be PersonWithAge
        private sealed class PersonWithAge : Person, IEquatable<PersonWithAge>
        {
            public int Age { get; set; }

            public bool Equals(PersonWithAge other)
            {
                if (!base.Equals(other))
                    return false;
                return Age == other.Age;

            }

            public override bool Equals(object obj)
            {
                if (!base.Equals(obj))
                    return false;
                var agedPerson = obj as PersonWithAge;
                return Equals(agedPerson);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode() ^ Age.GetHashCode();
            }

            public static bool operator ==(PersonWithAge firstPerson, PersonWithAge secondPerson)
            {
                return Equals(firstPerson, secondPerson);
            }

            public static bool operator !=(PersonWithAge firstPerson, PersonWithAge secondPerson)
            {
                return !Equals(firstPerson, secondPerson);
            }
        }

        [Fact]
        void WhenPersonWithAgeReferenceAreEqual_ShouldBeEqualForAll()
        {
            var person = new PersonWithAge() { FirstName = "John", LastName = "Smith", Age = 25 };
            var samePerson = person;

            Assert.True(person.Equals(samePerson));
            Assert.True(person == samePerson);
            Assert.False(person != samePerson);
            Assert.True(person.GetHashCode() == samePerson.GetHashCode());
            Assert.True(Equals(person, samePerson));
            Assert.True(ReferenceEquals(person, samePerson));
        }

        [Fact]
        void WhenPersonWithAgeFieldsAreEqual_ShouldBeEqual()
        {
            var person = new PersonWithAge() { FirstName = "John", LastName = "Smith", Age = 25 };
            var samePerson = new PersonWithAge() { FirstName = "John", LastName = "Smith", Age = 25 };

            Assert.True(person.Equals(samePerson));
            Assert.True(person == samePerson);
            Assert.False(person != samePerson);
            Assert.True(person.GetHashCode() == samePerson.GetHashCode());
            Assert.True(Equals(person, samePerson));
            Assert.False(ReferenceEquals(person, samePerson));
        }

        [Fact]
        void WhenPersonWithAgeFieldsNotTheSame_ShouldNotBeEqual()
        {
            var person = new PersonWithAge() { FirstName = "John", LastName = "Smith", Age = 25 };
            var otherPerson = new PersonWithAge() { FirstName = "John", LastName = "Smith", Age = 30 };

            Assert.False(person.Equals(otherPerson));
            Assert.False(person == otherPerson);
            Assert.True(person != otherPerson);
            Assert.False(person.GetHashCode() == otherPerson.GetHashCode());
            Assert.False(Equals(person, otherPerson));
            Assert.False(ReferenceEquals(person, otherPerson));
        }
    }
}
