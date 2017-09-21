using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;
using Xunit.Abstractions;
namespace CollectionUsageTests
{
    [Trait("Collection Usage", "ReadOnlyDictionary<TKey,TValue>")]
    public class ReadOnlyDictionary_Tests
    {
        private ITestOutputHelper _output;

        /// <summary>
        /// ReadOnlyDictionary<TKey,TValue> is a dictionary wrapper that prevents new items to be added to the collection
        /// Namespace:System.Collections.ObjectModel
        /// </summary>

        public ReadOnlyDictionary_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void UsingReadOnlyDictionary()
        {
            var dictionary = new Dictionary<int, string>()
            {
                {1,"one" },
                {2,"two" },
                {3,"three" }
            };

            var readOnlyDictionary = new ReadOnlyDictionary<int, string>(dictionary);

            foreach (var item in readOnlyDictionary)
            {
                _output.WriteLine($"Key: {item.Key} Value: {item.Value}");
            }
            //Key: 1 Value: one
            //Key: 2 Value: two
            //Key: 3 Value: three
        }
    }
}
