using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace EqualityAndComparisonTests
{
    //Shows how to create an comparer which implements IEquatableComparer<T> and one that derives from EquatableComparable<T>.
    [Trait("Equality Comparer", "")]
    public class ComparisonWithEqualityComparersTests
    {
        private ITestOutputHelper _outputLogger;

        public ComparisonWithEqualityComparersTests(ITestOutputHelper outputLogger)
        {
            _outputLogger = outputLogger;
        }

        //Using PersonEqualityComparer derived by EqualityComparer<T> abstract base class
        [Fact]
        void WhenAddingSameItemToHashSetWithDifferentCase_ShouldOnlyAddOneItem()
        {
            var people = new HashSet<PersonStruct>(PersonEqualityComparer.Instance)
            {
                new PersonStruct() { FirstName = "Billy", LastName = "Smith" },
                new PersonStruct() { FirstName = "billy", LastName = "Smith" },
                new PersonStruct() { FirstName = "Billy", LastName = "smith" }
            };

            Assert.Equal(1, people.Count);
        }

        //Using PersonEqualityComparerImplementation implementing the IEqualityComparer<T> generic interface
        //Recommended to use PersonEqualityComparer instead of PersonEqualityComparerImplementation
        [Fact]
        void WhenAddingSameItemToDictionaryWithDifferentCase_ShouldThrowArgumentException()
        {
            var people = new Dictionary<PersonStruct, string>(new PersonEqualityComparerImplementation());
            people.Add(new PersonStruct() { FirstName = "Billy", LastName = "Smith" }, String.Empty);
            Assert.Throws<ArgumentException>(() => people.Add(new PersonStruct() { FirstName = "billy", LastName = "Smith" }, string.Empty));
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
                //would need to check for nulls if wasn't a value type
                return !x.FirstName.Equals(y.FirstName, StringComparison.InvariantCultureIgnoreCase)
                    ? false
                    : x.LastName.Equals(y.LastName, StringComparison.InvariantCultureIgnoreCase);
            }

            public override int GetHashCode(PersonStruct obj)
            {
                return obj.FirstName.ToUpperInvariant().GetHashCode() ^ obj.LastName.ToUpperInvariant().GetHashCode();
            }
        }

        //Equality comparer implemented by IEqualityComparer<T>
        //Recommended to derive from the abstract EqualityComparer<T>
        private class PersonEqualityComparerImplementation : IEqualityComparer<PersonStruct>
        {
            public bool Equals(PersonStruct x, PersonStruct y)
            {
                //would need to check for nulls if wasn't a value type
                return !x.FirstName.Equals(y.FirstName, StringComparison.InvariantCultureIgnoreCase)
                    ? false
                    : x.LastName.Equals(y.LastName, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(PersonStruct obj)
            {
                return obj.FirstName.ToUpperInvariant().GetHashCode() ^ obj.LastName.ToUpperInvariant().GetHashCode();
            }
        }

        //Used the write the results to Visual Studio runner 'Output'
        private void WriteToOutput(PersonStruct[] sortedPeople)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Input\t\tOutput");
            var people = GetPeople();
            for (int i = 0; i < people.Length; i++)
            {
                sb.AppendLine($"{people[i].FullName}\t {(i < sortedPeople.Length ? sortedPeople[i].FullName : String.Empty)}");
            }
            _outputLogger.WriteLine(sb.ToString());
        }

        //Test person seed list
        private PersonStruct[] GetPeople()
        {
            return new[]
            {
                new PersonStruct(){FirstName="Billy", LastName = "Smith"},
                new PersonStruct(){FirstName="Billy", LastName = "Adams"},
                new PersonStruct(){FirstName="Jacob", LastName = "Smith"},
                new PersonStruct(){FirstName="billy", LastName = "smith"},
                new PersonStruct(){FirstName="Lucas", LastName = "ryan"},
            };
        }
    }
}
