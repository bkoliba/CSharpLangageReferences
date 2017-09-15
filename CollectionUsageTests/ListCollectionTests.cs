using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CollectionUsageTests
{
    [Trait("List Collections", "")]
    public class ListCollectionTests
    {
        /// <summary>
        /// List<T> is an indexed base collection like Array<T> but isn't an fixed size.
        /// 
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

            list = new List<int> { 1, 2, 3, 4, 5 }; //Use the initializer to setup values
            Assert.Equal(expected, list);
        }
    }
}
