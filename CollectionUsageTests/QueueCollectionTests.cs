using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "Queue<T>")]
    public class QueueCollectionTests
    {
        /// <summary>
        /// Queue<T> is an non-indexed collection which allows values to be added (enqueue) and removed (dequeue) in the order they were added 
        /// First-in First-out collection
        /// </summary>

        [Fact]
        public void WhenUsingQueueEnQueue_ShouldAddItem()
        {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            var expected = new int[] { 1, 2, 3 };

            Assert.Equal(3, queue.Count);
            Assert.Equal(expected, queue);
            Assert.Equal(1, queue.Peek());
        }

        [Fact]
        public void WhenUsingQueueDequeue_ShouldRemoveItem()
        {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            var value = queue.Dequeue();
            var expected = new int[] { 2, 3 };

            Assert.Equal(1, value);
            Assert.Equal(2, queue.Count);
            Assert.Equal(expected, queue);
            Assert.Equal(2, queue.Peek());
        }
    }
}
