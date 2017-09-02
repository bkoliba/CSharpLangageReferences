using System;
using Xunit;
using Xunit.Abstractions;

namespace EqualityTests
{
    public class DotNetEquality
    {
        private ITestOutputHelper _outputLogger;

        public DotNetEquality(ITestOutputHelper outputLogger)
        {
            _outputLogger = outputLogger;
        }

        [Theory]
        [InlineData(null, null, true)]
        [InlineData("hello", "hello", true)]
        [InlineData(null, 1, false)]
        [InlineData((int)1, 1, true)]
        public void TestValueEquality(object value1, object value2, bool expectedResult)
        {
            _outputLogger.WriteLine($"{value1} = {value2}");
            Assert.Equal(expectedResult, object.Equals(value1, value2));
        }

        [Fact]
        void TestStructEquality()
        {
            var person1 = new PersonStruct() { FirstName = "Billy", LastName="Koliba" };
            var person2 = new PersonStruct() { FirstName = "Billy", LastName="Koliba"};

            Assert.True(person1.Equals(person2));

            person2.LastName = "Smith";

            Assert.False(person1.Equals(person2));
        }

        [Fact]
        void TestReferenceEquality()
        {
            var person1 = new PersonReference() { FirstName = "Billy", LastName = "Koliba" };
            var person2 = new PersonReference() { FirstName = "Billy", LastName = "Koliba" };

            Assert.False(person1.Equals(person2));

            person2.LastName = "Smith";

            Assert.False(person1.Equals(person2));

            person1 = person2;

            Assert.True(person1.Equals(person2));
        }

        [Fact]
        void TestReferenceEqualityWithEquatable()
        {
            var person1 = new PersonReferenceWithEquatable() { FirstName = "Billy", LastName = "Koliba" };
            var person2 = new PersonReferenceWithEquatable() { FirstName = "Billy", LastName = "Koliba" };

            Assert.True(person1.Equals(person2));

            person2.LastName = "Smith";

            Assert.False(person1.Equals(person2));
            Assert.False(ReferenceEquals(person1, person2));

            person1 = person2;

            Assert.True(person1.Equals(person2));
            Assert.True(ReferenceEquals(person1, person2));
        }
        private class PersonReference
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private class PersonReferenceWithEquatable : PersonReference, IEquatable<PersonReferenceWithEquatable>
        {
            public bool Equals(PersonReferenceWithEquatable person)
            {
                return person.FirstName == person.FirstName && person.LastName == LastName;
            }
        }
        private struct PersonStruct
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
