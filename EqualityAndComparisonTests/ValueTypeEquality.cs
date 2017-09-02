using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EqualityTests
{
    public class ValueTypeEquality
    {
        [Fact]
        void WhenUsingOverrideEqualityForValueType()
        {
            var firstPerson = new PersonStruct() { FirstName="Billy", LastName="Koliba" };
            var secondPerson = new PersonStruct() { FirstName = "Amy", LastName = "Koliba" };
            var thirdPerson = new PersonStruct() { FirstName = "Billy", LastName = "Koliba" };

            Assert.True(firstPerson.Equals(thirdPerson));
            Assert.False(firstPerson.Equals(secondPerson));
            Assert.True(firstPerson == thirdPerson);
            Assert.False(firstPerson == secondPerson);
            Assert.True(firstPerson != secondPerson);
            Assert.True(firstPerson.GetHashCode() == thirdPerson.GetHashCode());
            Assert.True(firstPerson.GetHashCode() != secondPerson.GetHashCode());
            Assert.True(firstPerson.Equals((object)thirdPerson));
            Assert.False(firstPerson.Equals(1));
        }

        //Note: Implement IEquatable<> when doing value types

        private struct PersonStruct : IEquatable<PersonStruct>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            //Equal logic here
            public bool Equals(PersonStruct otherPerson)
            {
                return FirstName == otherPerson.FirstName && LastName == otherPerson.LastName;
            }

            public static bool operator ==(PersonStruct firstPerson, PersonStruct secondPerson)
            {
                return firstPerson.Equals(secondPerson);
            }

            public static bool operator !=(PersonStruct firstPerson, PersonStruct secondPerson)
            {
                return !firstPerson.Equals(secondPerson);
            }

            //check type before calling equal method
            public override bool Equals(object obj)
            {
                if (obj is PersonStruct person)
                    return Equals(person);
                return false;
            }

            public override int GetHashCode()
            {
                return FirstName.GetHashCode() ^ LastName.GetHashCode();
            }
        }
    }
}
