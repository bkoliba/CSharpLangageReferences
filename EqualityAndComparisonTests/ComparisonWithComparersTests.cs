using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace EqualityAndComparisonTests
{
    //These tests shows how to do basic implementation of using IComparable<T>, IComparer<T>, and Comparer<T> to do comparisons on an struct Array
    [Trait("Comparison With Comparers", "")]
    public class ComparisonWithComparersTests
    {
        //Logger to write output results to visual studio runner output
        private ITestOutputHelper _outputLogger;
        public ComparisonWithComparersTests(ITestOutputHelper outputLogger)
        {
            _outputLogger = outputLogger;
        }

        //Implemented IComparable<PersonStruct> which sorts person by first name
        [Fact]
        void WhenSortingPeople_ShouldSortByFirstName() 
        {
            var people = GetPeople();
            Array.Sort(people);
            WriteToOutput(people);
            /*  
                Input            Output
                Smith, Billy	 smith, billy
                Adams, Billy	 Smith, Billy
                Smith, Jacob	 Adams, Billy
                smith, billy	 Smith, Jacob
                ryan, Lucas 	 ryan, Lucas
            */
        }

        //Comparer implements IComparer<PersonStruct> which sorts by person last name
        [Fact]
        void WhenSortingPeopleWithPersonLastNameComparer_ShouldSortByLastName() 
        {
            var people = GetPeople();
            Array.Sort(people, new PersonLastNameComparer());
            WriteToOutput(people);
            /*
                Input		     Output
                Smith, Billy	 Adams, Billy
                Adams, Billy	 ryan, Lucas
                Smith, Jacob	 smith, billy
                smith, billy	 Smith, Billy
                ryan, Lucas	     Smith, Jacob
            */
        }

        // Comparer derives from Comparer<PersonStruct> abstract class which sorts by person full name
        [Fact]
        void WhenSortingPeopleWithPersonFullNameComparer_ShouldSortByFullName()
        {
            var people = GetPeople();
            Array.Sort(people, PersonFullNameComparer.Instance);
            WriteToOutput(people);

            /*
                Input		     Output
                Smith, Billy	 Adams, Billy
                Adams, Billy	 ryan, Lucas
                Smith, Jacob	 smith, billy
                smith, billy	 Smith, Jacob
                ryan, Lucas	     Smith, Billy  
             */
        }

        //Using IComparable<T> allow you to setup how the object is to be sorted
        //Implemented IComparable<PersonStruct> to sort by first name
        private struct PersonStruct : IComparable<PersonStruct>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName => $"{LastName}, {FirstName}";

            //order by first name
            public int CompareTo(PersonStruct other)
            {
                return FirstName.CompareTo(other.FirstName);
            }
        }

        //Basic comparer which implements IComparer<T> to compare by last name. 
        //Recommended to derive from Comparer<T> Abstract class instead.(example below)
        private class PersonLastNameComparer : IComparer<PersonStruct>
        {
            public int Compare(PersonStruct personOne, PersonStruct personTwo)
            {
                return personOne.LastName.CompareTo(personTwo.LastName);
            }
        }

        //Best practice is to use Comparer<PersonStruct> abstract class, because it allows for you
        //to setup both IComparer<T> and IComparer by just overridden one Compare(T,T)
        //Also common practice to create a singleton to avoid instanting everywhere
        private sealed class PersonFullNameComparer : Comparer<PersonStruct>
        {
            private static readonly PersonFullNameComparer instance = new PersonFullNameComparer();
            public static PersonFullNameComparer Instance => instance;
            private PersonFullNameComparer() { }
            public override int Compare(PersonStruct personOne, PersonStruct personTwo)
            {
                return personOne.FirstName.CompareTo(personTwo.FullName);
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
