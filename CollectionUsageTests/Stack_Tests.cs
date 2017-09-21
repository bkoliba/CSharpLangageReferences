using System.Collections.Generic;
using Xunit;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "Stack<T>")]
    public class Stack_Tests
    {
        /// <summary>
        /// Stack<T> allows a collection to be created by adding items (push) and removing items (pop) from the same spot.
        /// Last-in First-out collection
        /// Namespace: System.Collection.Generic
        /// </summary>

        [Fact]
        public void WhenUsingPush_ShouldAddItem()
        {
            var stack = new Stack<int>(); //cant use an initializer
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var expected = new int[] { 3,2,1}; //notice reverse or because Last-in First-out collection

            Assert.Equal(3, stack.Count);
            Assert.Equal(expected, stack);
            Assert.Equal(3, stack.Peek()); //shows the next item to be popped
        }

        [Fact]
        public void WhenUsingPop_ShouldRemoveItem()
        {
            var stack = new Stack<int>(); //cant use an initializer
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var value = stack.Pop();
            var expected = new int[] { 2, 1 }; //what is left in stack

            Assert.Equal(3, value);
            Assert.Equal(2, stack.Count);
            Assert.Equal(expected, stack);
            Assert.Equal(2, stack.Peek()); //shows the next item to be popped
        }
    }
}
