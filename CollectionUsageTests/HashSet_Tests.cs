using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "HashSet<T>")]
    public class HashSet_Tests
    {
        private ITestOutputHelper _output;

        //uniqueness
        /// <summary>
        /// HashSet<T> is a set that implements ISet<T>. A Set is a collection of unique data which has no ordering. Since HashSet<T> implements ISet<T> it has several operations 
        /// available which allows you to interact with other collections of the same generic type. The internals uses a Hash Table to store the values for the collection. Since stored
        /// in a Hash Table lookup is really fast to determine if a item is already in the collection.
        /// Namespace: System.Collections.Generic
        /// </summary>

        public HashSet_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void WhenAddingElementsToHashSet_ShouldOnlyBeStoredOnce()
        {
            var set = new HashSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(2); //2 is in the collection so not added
            set.Add(3);
            set.Add(3); //3 is in the collection so not added
            set.Add(3);

            Assert.Equal(3, set.Count);
            _output.WriteLine(string.Join(", ", set));//1, 2, 3

            var set1 = new HashSet<int>(new[] { 1, 2, 2, 3, 3, 3 }); //setting up HashSet using IEnumerable<T> as parameter to the constructor

            Assert.Equal(3, set1.Count);
            _output.WriteLine(string.Join(", ", set1));//1, 2, 3

            Assert.Equal(set, set1);
        }

        [Fact]
        public void UsingHashSet_EqualityComparer_AllowForCustomEquality()
        {
            var set = new HashSet<string>(new[] { "hello", "Hello", "JoHn", "Billy", "john" }); //All items will be added since case are not equal

            Assert.Equal(5, set.Count);
            _output.WriteLine(String.Join(", ", set));//hello, Hello, JoHn, Billy, john            

            var set1 = new HashSet<string>(set, StringComparer.CurrentCultureIgnoreCase); //used in builtin string comparer to ignore the case for the Hash Set

            Assert.Equal(3, set1.Count);
            _output.WriteLine(String.Join(", ", set1));//hello, JoHn, Billy
        }

        //ExceptWith - Removes all elements in the specified collection from the current set.
        [Fact]
        public void UsingHashSet_ExceptWith()
        {
            var set = new HashSet<int>(new int[] { 1, 2, 3, 4, 5 });
            var exceptItems = new int[] { 4, 5, 6, 7, 8 };

            set.ExceptWith(exceptItems); //Only need to provide IEnumerable<T> instead of another set, doesn't return new set it modified existing one 
            var expected = new[] { 1, 2, 3 };

            Assert.Equal(expected, set);
            _output.WriteLine(String.Join(", ", set));//1, 2, 3
        }

        //IntersectWith - Modifies the current set so that it contains only elements that are also in a specified collection.
        [Fact]
        public void UsingHashSet_IntersectWith()
        {
            var set = new HashSet<int>(new int[] { 1, 2, 3, 4, 5 });
            var intersectItems = new int[] { 4, 5, 6, 7, 8 };

            set.IntersectWith(intersectItems);
            var expected = new[] { 4, 5 };

            Assert.Equal(expected, set);

            _output.WriteLine(String.Join(", ", set));
        }

        //SymmetricExceptWith - Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.
        [Fact]
        public void UsingHashSet_SymmetricExceptWith()
        {
            var setA = new HashSet<int>(new int[] { 1, 2, 3, 4 });
            var setB = new int[] { 2, 4, 5, 6 };
            var expected = new int[] { 1, 3, 5, 6 };

            setA.SymmetricExceptWith(setB);

            Assert.Equal(expected, setA);
        }

        //UnionWith - Modifies the current set so that it contains all elements that are present in the current set, in the specified collection, or in both.
        [Fact]
        public void UsingHashSet_UnionWith()
        {
            var setA = new HashSet<int>(new int[] { 1, 2, 3, 4 });
            var setB = new int[] { 2, 4, 5, 6 };
            var expected = new int[] { 1, 2, 3, 4, 5, 6 };

            setA.UnionWith(setB);

            Assert.Equal(expected, setA);
        }


        //IsSubsetOf - Determines whether a set is a subset of a specified collection.
        //IsProperSubsetOf - Determines whether the current set is a proper (strict) subset of a specified collection.
        [Fact]
        public void UsingHashSet_IsSubsetOf()
        {
            var setA = new HashSet<int>(new int[] { 1, 4, 7 });
            var setB = new int[] { 1, 4, 5, 7 };
            var setC = new int[] { 1, 4, 7 };

            Assert.True(setA.IsSubsetOf(setB));
            Assert.True(setA.IsProperSubsetOf(setB));

            Assert.True(setA.IsSubsetOf(setC));
            Assert.False(setA.IsProperSubsetOf(setC)); // False setA has the same values setC and no additional values
        }

        //IsSupersetOf - Determines whether the current set is a superset of a specified collection.
        //IsProperSupersetOf - Determines whether the current set is a proper (strict) superset of a specified collection.
        [Fact]
        public void UsingHashSet_IsSupersetOf()
        {
            var setA = new HashSet<int>(new int[] { 1, 2, 3, 4 });
            var setB = new int[] { 1, 3, 4 };
            var setC = new int[] { 1, 2, 3, 4 };

            Assert.True(setA.IsSupersetOf(setB));//All setB values are in setA
            Assert.True(setA.IsProperSupersetOf(setB)); //All setB values are in setA, with additional values in setA

            Assert.True(setA.IsSupersetOf(setC));//All setC values are in setA
            Assert.False(setA.IsProperSupersetOf(setC));//All setA and setC has same values
        }

        //Overlaps -  Determines whether the current set overlaps with the specified collection.
        [Fact]
        public void UsingHashSet_Overlaps()
        {
            var setA = new HashSet<int>(new int[] { 1, 2, 3, 4 });
            var setB = new int[] { 1, 3, 4 };
            var setC = new int[] { 4, 5, 6 };

            Assert.True(setA.Overlaps(setB));//contain 1, 3, 4 in both
            Assert.True(setA.Overlaps(setC));//contain 4 in both
        }

        //SetEquals - Determines whether the current set and the specified collection contain the same elements.
        [Fact]
        public void UsingHashSet_SetEquals()
        {
            var setA = new HashSet<int>(new int[] { 1, 2, 3, 4 });
            var setB = new int[] { 1, 3, 4 };
            var setC = new int[] { 1, 2, 3, 4 };

            Assert.False(setA.SetEquals(setB));//False must contain exactly the same values
            Assert.True(setA.SetEquals(setC));
        }
    }
}
