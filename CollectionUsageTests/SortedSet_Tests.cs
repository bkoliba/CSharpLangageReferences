using System;
using System.Collections.Generic;
using Xunit;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "SortedSet<T>")]
    public class SortedSet_Tests
    {
        /// <summary>
        /// SortedSet<T> give the same set features as HashSet<T> since implementing ISet<T>. Instead of using a hashtable, SortedSet<T> uses a balanced tree
        /// which keeps the elements in order. The collection makes sure the set is always unque. Reference HashSet<T> tests to view set function examples.
        /// Namespace: System.Collection.Generic
        /// </summary>

        [Fact]
        public void WhenAddingElementsToSortedSet_ShouldOnlyBeStoredOnce()
        {
            var set = new SortedSet<int>(new int[] { 2, 1, 1, 4, 5, 6, 5, 3, 4 });
            var expected = new int[] { 1, 2, 3, 4, 5, 6 };

            Assert.Equal(expected, set);
        }

        [Fact]
        public void UsingSortedSet_EqualityComparer_AllowForCustomEquality()
        {
            var set = new SortedSet<string>(new string[] { "bat", "Apple", "Smith", "BAR", "apple" }, StringComparer.InvariantCultureIgnoreCase);
            var expected = new string[] {"Apple", "BAR", "bat", "Smith" };

            Assert.Equal(expected, set);
        }
    }
}
