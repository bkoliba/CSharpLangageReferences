using System;
using Xunit;

namespace EqualityTests
{
    [Trait("Equality","Value Type Equality")]
    public class EqualityValueTypeTests
    {
        //When overriding and implementing equality for a value type do the following:
        //-	Override object.Equals()
        //-	Implement IEquatable<T>.Equals(Best to store equal logic in this method)
        //-	Implement == and != overloads
        //-	Override object.GetHashCode()
        private struct PersonStruct : IEquatable<PersonStruct>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            //Implemetation of IEquatable<T>.Equal()
            public bool Equals(PersonStruct otherPerson)
            {
                return FirstName == otherPerson.FirstName && LastName == otherPerson.LastName;
            }

            //Overriding object.Equals()
            public override bool Equals(object obj)
            {
                if (obj is PersonStruct person)
                    return Equals(person);
                return false;
            }

            //Implementation of ==
            public static bool operator ==(PersonStruct firstPerson, PersonStruct secondPerson)
            {
                return firstPerson.Equals(secondPerson);
            }

            //Implementation of !=
            public static bool operator !=(PersonStruct firstPerson, PersonStruct secondPerson)
            {
                return !firstPerson.Equals(secondPerson);
            }

            //Overriding the GetHashCode() - is needed for hash table collections such as dictionary 
            public override int GetHashCode()
            {
                return FirstName.GetHashCode() ^ LastName.GetHashCode();
            }
        }

        [Fact]
        void WhenTwoValuesAreTheSame_ShouldBeEqual()
        {
            var person = new PersonStruct() { FirstName = "John", LastName = "Smith" };
            var samePerson = person;

            Assert.True(person.Equals(samePerson));
            Assert.True(person.GetHashCode() == samePerson.GetHashCode());
            Assert.True(person == samePerson);
            Assert.False(person != samePerson);
        }

        [Fact]
        void WhenTwoStructsContainsSameValues_ShouldBeEqual()
        {
            var person = new PersonStruct() { FirstName = "John", LastName = "Smith" };
            var samePerson = new PersonStruct() { FirstName = "John", LastName = "Smith" }; ;

            Assert.True(person.Equals(samePerson));
            Assert.True(person.GetHashCode() == samePerson.GetHashCode());
            Assert.True(person == samePerson);
            Assert.False(person != samePerson);
        }

        [Fact]
        void WhenTwoStructsContainsDifferentValues_ShouldNotBeEqual()
        {
            var person = new PersonStruct() { FirstName = "John", LastName = "Smith" };
            var otherPerson = new PersonStruct() { FirstName = "Bob", LastName = "Smith" }; ;

            Assert.False(person.Equals(otherPerson));
            Assert.False(person.GetHashCode() == otherPerson.GetHashCode());
            Assert.False(person == otherPerson);
            Assert.True(person != otherPerson);
        }
    }
}
