using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "List<T>")]
    public class ListCollectionTests
    {
        private ITestOutputHelper _output;

        /// <summary>
        /// List<T> is an indexed base collection like Array<T> but is not a fixed size. List<T> does not have virtual members to customize the collection
        /// like Collection<T> for preformance reason. List<T> provide code for features like sorting and searching like Array provides.
        /// 
        /// Namespace: System.Collections.Generic
        /// List<T> Implements IList<T> which depends on ICollection<T>, IEnumerable<T>, IEnumerable implementations, and other legacy Interfaces
        /// 
        /// IList<T> adds indexing capabilitys to the collection contains members: 
        ///     T this[int index], int IndexOf(T item), void Insert(int index, T item), and void RemoveAt(int index)
        /// 
        /// ICollection<T> adds inserting and removal elements in the collection which contain members: 
        ///     int Count, bool IsReadOnly, void Add(T item), void Clear(), bool Contains(T item),
        ///     void CopyTo(T[] array, int arrayIndex), bool Remove(T item)
        /// 
        /// IEnumerable<T> allow you enumerate through the collection by implementing: IEnumerator<T> GetEnumerator()
        /// 
        /// IEnumerable: IEnumerator GetEnumerator(); Returns an enumerator that iterates through a collection.
        ///     IEnumerator:  object Current, bool MoveNext(), void Reset();
        /// </summary>
        public ListCollectionTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CreatingLists()
        {
            //creating empty list
            var list = new List<int>();
            var list1 = new List<int> { };
            var list2 = new List<int>(5); // 5 is the capacity of the array but doesn't implicitly initalize the collection

            Assert.Empty(list);
            Assert.Empty(list1);
            Assert.Empty(list2);

            int[] expected = { 1, 2, 3, 4, 5 };

            list = new List<int>(expected); // past IEnumerable<T> to initize the colllection
            Assert.Equal(expected, list);

            list = new List<int> { 1, 2, 3, 4, 5 }; //Using the initializer to setup values
            Assert.Equal(expected, list);

            list = new List<int>();
            list.Add(1); // Adding elements to list
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Assert.Equal(expected, list);
        }

        [Fact]
        public void WhenCapacitySetToTen_InitializedWithFiveElements_ShouldHaveCountOfFive()
        {
            //Sets the list capacity to 10 and initalize 5 elements the count property should be 5.
            //Capacity is the space allocated in memory for the indexed list to store elements.
            //Once Capacity is met the List code doubles the capacity automatically (create new fixed sized collection based on capacity and copy over existing elements)
            var list = new List<int>(10) { 1, 2, 3, 4, 5 };
            Assert.Equal(5, list.Count);

            list = new List<int>(5) { 1, 2, 3, 4, 5 };
            list.Add(6);
            Assert.Equal(6, list.Count);
            Assert.Equal(10, list.Capacity);
        }

        [Fact]
        public void ReadingFromLists()
        {
            List<string> daysOfWeek = Enum.GetNames(typeof(DayOfWeek)).ToList(); //LINQ extension method to make a list from an Enumerable<T>

            _output.WriteLine($"Using array index {daysOfWeek[0]}");

            _output.WriteLine("Using 'foreach':");
            foreach (var day in daysOfWeek)//Read only
            {
                _output.WriteLine(day);
            }

            _output.WriteLine("Using 'for'");
            for (int i = 0; i < daysOfWeek.Count; i++)
            {
                _output.WriteLine(daysOfWeek[i]);
            }
        }

        [Fact]
        public void ReplaceValueInList()
        {
            var list = new List<int> { 1, 2, 3 };
            list[0] = 3;
            list[1] = 2;
            list[2] = 1;
            Assert.Equal(new[] { 3, 2, 1 }, list);

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = i + 1; //replace value stored at the memory location
            }
            Assert.Equal(new[] { 1, 2, 3 }, list);
        }

        
        [Fact]
        public void ReverseLists()
        {
            var list = new List<int> { 1, 2, 3 };
            var expectedAry = new[] { 3, 2, 1 };
            list.Reverse();
            Assert.Equal(expectedAry, list);
        }

        [Fact]
        public void SortLists()
        {
            var list = new List<int> { 1, 3, 5, 2, 4 };
            var expectedAry = new[] { 1, 2, 3, 4, 5 };
            list.Sort();

            Assert.Equal(expectedAry, list);
            Assert.Equal(expectedAry, list.OrderBy(a => a)); //LINQ extension method returns the sorted results
        }

        [Fact]
        public void FindingElementsInList()
        {
            var list = new List<string> { "one", "two", "three", "four", "five", "one", "two" };

            Assert.Equal(2, list.IndexOf("three")); //searches for the first element that matches the value starting at index zero
            Assert.Equal(5, list.LastIndexOf("one")); // search for first element that matches the value starting from the last index

            Assert.Equal(1, list.FindIndex(a => a[0].Equals('t'))); //finds the first element that starts with 't'
            Assert.Equal(5, list.FindLastIndex(a => a[0].Equals('o'))); //finds the last element that starts with 'o'

            Assert.Equal(2, list.FindAll(a => a.Equals("two")).Count); //finds and returns the 'two' values the list
        }

        [Fact]
        public void BinarySearchInArrays()
        {
            //For binary search to work the collection must be sorted first and should be used on large collections.
            //It works by using the sorted collection midway point and see if that value is higher or lower.
            //This would cut the collection in half, than the steps are repeated until the element is found. 
            //Is very effective because the collection is cut in half on each comparison until the element is found.
            var sortedList = new List<int> { 1, 2, 3, 4, 5 };

            Assert.Equal(1, sortedList.BinarySearch(2));//get the first element found index
        }
    }
}
