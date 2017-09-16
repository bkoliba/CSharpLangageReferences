using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "ReadOnlyCollection<T>")]
    public class ListReadOnlyCollectionTests
    {
        private ITestOutputHelper _output;

        public ListReadOnlyCollectionTests(ITestOutputHelper output)
        {
            _output = output;
        }
        /// <summary>
        /// ReadOnlyCollection is an read-only wrapper for IList<T> collections like Collection<T> and List<T>. Which means
        /// the instance doesn't provide Add, Clear, or remove methods.
        /// </summary>

        [Fact]
        public void ReadOnlyUsage()
        {
            var list = new List<int>() { 1, 2, 3, 4 };
            var readOnlyList = list.AsReadOnly(); // copys list to an ReadOnlyCollection<T>
            readOnlyList = new ReadOnlyCollection<int>(list); //creates ReadOnlyCollection<T> by passing in IList<T> collection

            foreach (var item in readOnlyList)
            {
                _output.WriteLine(item.ToString());
            }
        }
    }
}
