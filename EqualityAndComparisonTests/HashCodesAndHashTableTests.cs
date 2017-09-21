using System;
using System.Collections.Generic;
using Xunit;

namespace EqualityAndComparisonTests
{
    [Trait("Equality","Hash tables and Hash Codes")]
    public class HashCodesAndHashTableTests
    {
        /// <summary>
        /// Hash tables are used by a couple of collections such as HashSet and Dictionaries. The HashTable uses the hash code
        /// of the value % 5 and takes the remainder to determine what bucket the item would be stored into the collection. This allows for great
        /// preformance when the HashCode algorithm divides the items up evenly accross the buckets. When implementing equality be sure to
        /// also implement GetHashCode using the same fields that were used in implementing equality.
        /// </summary>

        [Fact]
        void WhenUsingInvarientIgnoreCaseComparerWithDifferentCase_ShouldReturnSameHashCode()
        {
            var comparer = StringComparer.InvariantCultureIgnoreCase;
            Assert.Equal(comparer.GetHashCode("Hello"), comparer.GetHashCode("hello"));
        }

        [Fact]
        void WhenUsingInvarientComparerWithDifferentCase_ShouldReturnDifferentHashCode()
        {
            var comparer = StringComparer.InvariantCulture;
            Assert.NotEqual(comparer.GetHashCode("Hello"), comparer.GetHashCode("hello"));
        }

        [Fact]
        void WhenCaseIsDifferentUsongPersonEqualityComparer_ShouldReturnSameHashCode()
        {
            var person1 = new PersonStruct() { FirstName = "Hello", LastName = "World" };
            var person2 = new PersonStruct() { FirstName = "hello", LastName = "world" };
            var comparer = PersonEqualityComparer.Instance;
            Assert.True(comparer.Equals(person1, person2));
            Assert.True(comparer.GetHashCode(person1) == comparer.GetHashCode(person2));
        }

        private struct PersonStruct
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName => $"{LastName}, {FirstName}";
        }

        private sealed class PersonEqualityComparer : EqualityComparer<PersonStruct>
        {
            private static readonly PersonEqualityComparer instance = new PersonEqualityComparer();
            public static PersonEqualityComparer Instance => instance;
            private PersonEqualityComparer() { }
            public override bool Equals(PersonStruct x, PersonStruct y)
            {
                var comparer = StringComparer.InvariantCultureIgnoreCase; // deals with null chaking
                return comparer.Equals(x.FirstName, y.FirstName) ? comparer.Equals(x.LastName, y.LastName) : false;
            }

            //Using string comparer to XOR '^' the hash codes together. This usually gets a pretty even spread across the collections hash table buckets.
            //Make sure to use same fields in GetHashCode as you did in Equals
            public override int GetHashCode(PersonStruct person)
            {
                var comparer = StringComparer.InvariantCultureIgnoreCase;
                return comparer.GetHashCode(person.FirstName) ^ comparer.GetHashCode(person.LastName);
            }
        }
    }
}
