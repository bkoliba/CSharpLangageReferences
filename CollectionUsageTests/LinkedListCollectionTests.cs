using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "LinkedList<T>")]
    public class LinkedListCollectionTests
    {
        /// <summary>
        /// LinkedList<T> is a list with fast adding/removing of elements because the collection does not have to copy/move 
        /// elements around in memory for adding and removing.
        /// LinkedList is not an index base collection because it isn't stored in an continuous block of memory, 
        /// instead it is stored anywhere in memory. This collection is still enumerable because the keeps a reference (link/pointer) to the next and previous node.
        /// LinkedList<T> items are stored in collection as LinkedListNode<T> items(used to store information about the next and previous nodes)
        /// </summary>

        private ITestOutputHelper _output;
        public LinkedListCollectionTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ReadingNodesFromLinkedList()
        {
            var list = new LinkedList<int>();
            //note there are differnet add methods: AddLast, AddBefore, AddAfter, AddFirst
            //collection always knows its first and last elements so adding and to front or end of collection has great preformance.
            list.AddLast(1);
            list.AddFirst(2);
            list.AddLast(3);

            //output: 2,1,3
            foreach (var item in list)
            {
                _output.WriteLine(item.ToString());
            }
        }

        [Fact]
        public void WhenInsertingItemBeforeNodeInLinkedList_ShouldAddItem()
        {
            var expected = new int[] { 1, 2, 3 };
            var list = new LinkedList<int>();
            var oneNode = list.AddLast(1);
            LinkedListNode<int> threeNode = list.AddLast(3); //when adding the LinkedListNode is returned

            list.AddBefore(threeNode, 2);

            Assert.Equal(expected, list);
        }

        [Fact]
        public void WhenInsertingItemAfterNodeInLinkedList_ShouldAddItem()
        {
            var expected = new int[] { 1, 2, 3 };
            var list = new LinkedList<int>();
            var oneNode = list.AddLast(1);
            LinkedListNode<int> threeNode = list.AddLast(3); //when adding the LinkedListNode is returned

            list.AddAfter(oneNode, 2);

            Assert.Equal(expected, list);
        }

        [Fact]
        public void WhenFindingElementToAddAfter_ShouldAddItem()
        {
            var list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(4);
            list.AddLast(2);

            var twoNode = list.Find(2); //find the first node match the search parameter, use FindLast to get the last item
            list.AddAfter(twoNode, 3);
            var expected = new int[] { 1, 2, 3, 4, 2 };

            Assert.Equal(expected, list);
        }

        [Fact]
        public void WhenRemovingItemFromLinkedList_ShouldRemoveItem()
        {
            var list = new LinkedList<int>();

            list.AddLast(1);
            list.Remove(1);

            Assert.Equal(0, list.Count);
        }
    }
}
