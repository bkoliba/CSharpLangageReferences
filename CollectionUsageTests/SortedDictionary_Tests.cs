using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage","SortedList<TKey,TValue>")]
    public class SortedDictionary_Tests
    {
        private ITestOutputHelper _output;

        /// <summary>
        /// SortedDictionary API functions like a SortedList<TKey, TValue> because they both implement IDictionary<TKey,TValue> interface
        /// Please reference Dictionary<T> Tests for basic Read, Write, and Remove examples.
        /// SortedDictionary differs from SortedList on how values are stored, SortedDictionary stores values in a complex balance tree instead of a hashtable or List.
        /// SortedDictionary allows for quick key lookup. Does not take longer to add to the balance tree either. One negative compared to SortedList is the extra overhead that is needed.
        /// Namespace: System.Collections.Generic
        /// </summary>

        public SortedDictionary_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UsingSortedDictionary()
        {
            var sortedList = new SortedDictionary<int, string>()
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
