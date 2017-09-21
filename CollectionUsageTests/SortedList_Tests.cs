using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage","SortedList<TKey,TValue>")]
    public class SortedList_Tests
    {
        private ITestOutputHelper _output;

        /// <summary>
        /// SortedList API functions like a Dictionary<TKey, TValue> because they both implement IDictionary<TKey,TValue> interface
        /// Please use Dictionary<T> Tests to view basic Read, Write, and Remove examples.
        /// SortedList differs from Dictionary on how values are stored, SortedList stores values in List instead of a hashtable.
        /// Since the keys are always sorted, key lookup is quick because a lookup uses binary search. Does take longer to add and remove items(moving elements around in memory).
        /// Namespace: System.Collections.Generic
        /// </summary>

        public SortedList_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UsingSortedList()
        {
            var sortedList = new SortedList<int, string>()
            {
                { 3,"three"},
                {2,"two" },
                {1,"one" }
            };

            foreach (var item in sortedList)
            {
                _output.WriteLine($"Key: {item.Key} Value: {item.Value}");
            }
            //Key: 1 Value: one
            //Key: 2 Value: two
            //Key: 3 Value: three
        }
    }
}
