using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace EqualityTests
{
    public class CSharpEquality
    {
        [Theory]
        [InlineData(3, 3, true)]
        void AreIntsEqualOp(int x, int y, bool expected)
        {
            Assert.Equal(expected, x == y);
        }

        [Theory]
        [InlineData(3, 3, true)]
        void AreIntsEqualMethod(int x, int y, bool expected)
        {
            Assert.Equal(expected, x.Equals(y));
        }

        [Fact]
        void AreExceptionsEqualMethod()
        {
            var exception1 = new FormatException("Test");
            var exception2 = new FormatException("Test");

            Assert.False(exception1.Equals(exception2));
        }

        [Fact]
        void AreExceptionsEqualOp()
        {
            var exception1 = new FormatException("Test");
            var exception2 = new FormatException("Test");

            Assert.False(exception1 == exception2);
        }

        [Fact]
        void AreStringsEqual()
        {
            string str1 = "test";
            string str2 = string.Copy(str1);
            Assert.True(str1 == str2);//op_Equality
            Assert.True(str1.Equals(str2));//Equals method
            Assert.False(ReferenceEquals(str1, str2));// ceq
        }

        [Fact]
        void AreStructsEqualOperatorOveride()
        {
            var person1 = new PersonStruct() { FirstName = "Billy", LastName = "Koliba" };
            var person2 = new PersonStruct() { FirstName = "Billy", LastName = "Koliba" };

            Assert.True(person1 == person2);
            Assert.True(person1.Equals(person2));
        }

        [Fact]
        void AreTuplesEqual()
        {
            var t1 = Tuple.Create(3); // Is reference type
            var t2 = Tuple.Create(3);
            var t3 = t1;
            Assert.False(ReferenceEquals(t1, t2));
            Assert.True(ReferenceEquals(t1, t3));
            Assert.True(t1.Equals(t2)); // works because it has been overidded from Microsoft
            Assert.False(t1 == t2); // microsoft failed to impliment operator
            Assert.True(t1 != t2);
        }
        private struct PersonStruct : IEquatable<PersonStruct>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public static bool operator ==(PersonStruct personStruct1, PersonStruct personStruct2)
            {
                return IsEqual(personStruct1, personStruct2);
            }

            public static bool operator !=(PersonStruct personStruct1, PersonStruct personStruct2)
            {
                return !IsEqual(personStruct1, personStruct2);
            }

            private static bool IsEqual(PersonStruct personStruct1, PersonStruct personStruct2)
            {
                return personStruct1.FirstName == personStruct2.FirstName && personStruct1.LastName == personStruct2.LastName;
            }
            public bool Equals(PersonStruct other)
            {
                return FirstName == other.FirstName &&
                       LastName == other.LastName;

            }
        }
    }
}
