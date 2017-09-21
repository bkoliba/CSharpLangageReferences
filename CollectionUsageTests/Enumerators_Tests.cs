using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "Enumerators")]
    public class Enumerators_Tests
    {
        private ITestOutputHelper _output;

        /// <summary>
        /// Enumerator<T> is implemented by all the major collections which is why foreach keyword works for all collections. The methods implemented on Enumerator<T> 
        /// are Current, MoveNext, and Reset. Current keeps track of the current element, MoveNext moves to the next element and returns a bool to indicate if the collection
        /// is at its end. Reset starts all over on collection iterator.
        /// </summary>

        public Enumerators_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        void UsingEnumerator()
        {
            var list = new List<int> { 1, 2, 3 };

            //List<T> implements IEnumerable<T> which means there is a method called GetEnumerator to return the collection enumerator
            using (IEnumerator<int> enumerator = list.GetEnumerator())
            {
                var moreItems = enumerator.MoveNext(); //must call first before viewing current
                while(moreItems)
                {
                    var item = enumerator.Current;
                    _output.WriteLine(item.ToString());
                    moreItems = enumerator.MoveNext(); //returns false when next collection item is empty.
                }
            }

            //Same results of the above code block
            foreach (var item in list)
            {
                _output.WriteLine(item.ToString());
            }
            //Output
            //1
            //2
            //3
        }
    }
}
