using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "Arrays")]
    public class ArrayTests
    {
        //Array are an Index-based collection with an Rich API, very lightweight, special C# syntax, and are fixed size.
        //-Elements are stored sequentially in memory (very efficient)
        //-Array and other collectitions are all reference types
        private ITestOutputHelper _output;
        public ArrayTests(ITestOutputHelper output)
        {
            //used to write to the test explorer output
            _output = output;
        }

        [Fact]
        public void CreatingArrays()
        {
            //creating empty arrays
            var ary = new int[0];
            var ary1 = new int[] { };
            int[] ary2 = { };

            Assert.Empty(ary);
            Assert.Empty(ary1);
            Assert.Empty(ary2);

            //arrays by default implicitly initialized the types in the array
            ary = new int[5];
            Assert.Equal(5, ary.Length);
            Assert.Equal(new[] { 0, 0, 0, 0, 0 }, ary);

            //initializing arrays with values
            ary = new int[5] { 1, 2, 3, 4, 5 };
            ary1 = new[] { 1, 2, 3, 4, 5 }; //notice type and size isn't necessary
            ary2 = new[] { 1, 1 + 1, 1 + 2, 2 * 2, 5 }; //expression and methods are allowed
            int[] ary3 = { 1, 2, 3, 4, 5 }; //'new' keyword not needed if explicitly given the type

            Assert.True(ary != ary1);//because Arrays are reference types
            Assert.Equal(ary, ary1);
            Assert.Equal(ary, ary2);
            Assert.Equal(ary, ary3);
        }

        [Fact]
        public void ReadingFromArrays()
        {
            string[] daysOfWeek = Enum.GetNames(typeof(DayOfWeek));

            _output.WriteLine($"Using array index {daysOfWeek[0]}");

            _output.WriteLine("Using 'foreach':");
            foreach (var day in daysOfWeek)//Read only
            {
                _output.WriteLine(day);
            }

            _output.WriteLine("Using 'for'");
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                _output.WriteLine(daysOfWeek[i]);
            }
        }

        [Fact]
        public void ReplaceIntInArray()
        {
            var ary = new[] { 1, 2, 3 };
            ary[0] = 3;
            ary[1] = 2;
            ary[2] = 1;
            Assert.Equal(new[] { 3, 2, 1 }, ary);

            for (int i = 0; i < ary.Length; i++)
            {
                ary[i] = i + 1; //replace value stored at the memory location
            }
            Assert.Equal(new[] { 1, 2, 3 }, ary);
        }

        [Fact]
        public void CopyingArrays()
        {
            //used to copy all the values to another array index: Be sure there is enough room allocated in the destination
            int[] ary = { 1, 2, 3, 4 };
            var ary1 = new int[ary.Length];
            ary.CopyTo(ary1, 0);
            Assert.Equal(ary, ary1);

            var ary2 = ary.ToArray(); //LINQ extension method extending array CopyTo
            Assert.Equal(ary, ary2);
        }

        [Fact]
        public void ReverseArrays()
        {
            var ary = new[] { 1, 2, 3 };
            var expectedAry = new[] { 3, 2, 1 };
            Array.Reverse(ary); //Array static method does in-place reverse
            Assert.Equal(expectedAry, ary);

            var ary1 = new[] { 1, 2, 3 };
            Assert.Equal(expectedAry, ary1.Reverse()); //LINQ extension method returns new reverse IEnumerable of elements
        }

        [Fact]
        public void SortArrays()
        {
            var ary = new[] { 1, 3, 5, 2, 4 };
            var expectedAry = new[] { 1, 2, 3, 4, 5 };
            Array.Sort(ary);

            Assert.Equal(expectedAry, ary);
            Assert.Equal(expectedAry, ary.OrderBy(a => a)); //LINQ extension method returns the sorted results
        }

        [Fact]
        public void FindingElementsInArray()
        {
            var ary = new[] { "one", "two", "three", "four", "five", "one", "two" };

            Assert.Equal(2, Array.IndexOf(ary, "three")); //searches for the first element that matches the value starting at index zero
            Assert.Equal(5, Array.LastIndexOf(ary, "one")); // search for first element that matches the value starting from the last index

            Assert.Equal(1, Array.FindIndex(ary, a => a[0].Equals('t'))); //finds the first element that starts with 't'
            Assert.Equal(5, Array.FindLastIndex(ary, a => a[0].Equals('o'))); //finds the last element that starts with 'o'

            Assert.Equal(2, Array.FindAll(ary, a => a.Equals("two")).Length); //finds and returns the 'two' values in an array
        }

        [Fact]
        public void BinarySearchInArrays()
        {
            //For binary search to work the collection must be sorted first and should be used on large collections.
            //It works by using the sorted collection midway point and see if that value is higher or lower.
            //This would cut the collection in half, than the steps are repeated until the element is found. 
            //Is very effective because the collection is cut in half on each comparison until the element is found.
            var sortedAry = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(1, Array.BinarySearch(sortedAry, 2));//get the first element found index
        }
    }
}
