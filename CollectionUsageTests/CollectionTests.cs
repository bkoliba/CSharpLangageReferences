using System;
using Xunit;
using System.Collections.ObjectModel;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "Collection<T>")]
    public class CollectionTests
    {
        /// <summary>
        /// Collection<T> is an indexed base collection like Array<T> but is not a fixed size.
        /// 
        /// Collection<T> does have virtual members to customize the collection functionality.
        /// ClearItems(),InsertItem(int index, T item),RemoveItem(int index), and SetItem(int index, T item) 
        /// 
        /// Since both Collection<T> and List<T> implements IList<T> the functionality is similar, but doesn't
        /// provide much sorting and find options like List<T> which could be made up by using LINQ but preformance hit for making new list on usage.
        /// 
        /// 
        /// Namespace: System.Collections.ObjectModel
        /// Collection<T> Implements IList<T> which depends on ICollection<T>, IEnumerable<T>, IEnumerable implementations, and other legacy Interfaces
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
        public void WhenUsingGreaterThanZeroCollection_ShouldOnlyAllowPositiveValues()
        {
            var expected = new[] { 1, 2, 3, 4 };
            var collection = new GreaterThanZeroCollection() { 1, 2, 3, 4 }; //derives from Collection<int>
            Assert.Equal(expected, collection);
            Assert.Equal(4, collection.Count);

            collection.Add(5);
            Assert.Equal(5, collection.Count);
            Assert.Equal(5, collection[collection.Count - 1]);

            Assert.Throws<ArgumentException>(() => collection.Add(0));
            Assert.Throws<ArgumentException>(() => collection.Add(-3));
        }

        //Using collection<T> as base class to override insert method to limit access to items in the collection
        private class GreaterThanZeroCollection : Collection<int>
        {
            //Used by the Collection<T> Add and insert methods
            protected override void InsertItem(int index, int item)
            {
                if (item <= 0)
                    throw new ArgumentException("Value must be greater than zero");
                base.InsertItem(index, item);
            }
            //could also overwrite RemoveItem, ClearItems, and SetItems
        }
    }
}
