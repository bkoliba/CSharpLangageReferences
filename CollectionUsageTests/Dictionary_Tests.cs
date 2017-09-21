using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "Dictionary<TKey,TValue>")]
    public class Dictionary_Tests
    {
        private ITestOutputHelper _output;

        /// <summary>
        /// Dictionary<TKey,TValue> is a collection of key value pairs, where only the key could be in the collection once.
        /// Dictionaries stores the values in Hash Table which makes searching quick. Hash Tables uses an algorithm in conjunction
        /// with the TKey Hash code to store items in buckets. This division allows searching against only one bucket instead of the whole collection,
        /// since the collection can determine always what bucket would contain an value.
        /// Namespace:System.Collections.Generic
        /// </summary>

        public Dictionary_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void WhenAddingItemDictionary_ShouldAddItem()
        {
            var keyValues = new KeyValuePair<int, string>[] {
                KeyValuePair.Create(1, "one"),
                KeyValuePair.Create(2, "two"),
                KeyValuePair.Create(3, "three"),
            };

            var dictionary = new Dictionary<int, string>(keyValues); //initializing dictionary with IEnumerable<KeyValuePair<TKey, TValue>>

            Assert.Equal(3, dictionary.Count);
            Assert.Equal("two", dictionary[2]);

            dictionary = new Dictionary<int, string>() //initializing dictionary with class initializer, passing key value pairs
            {
                {1,"one" }, // initalize key value pair with braces: {TKey,TValue}
                {2,"two" },
                {3,"three" }
            };

            Assert.Equal(3, dictionary.Count);
            Assert.Equal("two", dictionary[2]);

            dictionary = new Dictionary<int, string>();
            dictionary[1] = "one"; //Add or update value by using key
            dictionary[2] = "two";
            dictionary.Add(3, "three"); //Add using instance Add method, throws exception if existing

            Assert.Equal(3, dictionary.Count);
            Assert.Equal("two", dictionary[2]);
            Assert.Throws<ArgumentException>(() => dictionary.Add(1,"one"));

            Assert.False(dictionary.TryAdd(1, "one"));
            Assert.Equal(3, dictionary.Count);
        }

        [Fact]
        public void ReadingFromDictionaries()
        {
            var dictionary = new Dictionary<int, string>()
            {
                {1,"one" },
                {2,"two" },
                {3,"three" }
            };
            Assert.Equal("one", dictionary[1]);

            foreach (var item in dictionary)
            {
                _output.WriteLine($"Key: {item.Key} Value: {item.Value}");
            }
            //Key: 1 Value: one
            //Key: 2 Value: two
            //Key: 3 Value: three

            foreach (var key in dictionary.Keys)
            {
                _output.WriteLine($"Key: {key}");
            }
            //Key: 1
            //Key: 2
            //Key: 3

            foreach (var value in dictionary.Values)
            {
                _output.WriteLine($"Value: {value}");
            }
            //Value: one
            //Value: two
            //Value: three
        }

        [Fact]
        public void WhenRemovingItemFromDictionary_ShouldRemoveItem()
        {
            var dictionary = new Dictionary<int, string>()
            {
                {1,"one" },
                {2,"two" },
                {3,"three" }
            };

            dictionary.Remove(4); //remove item if exist

            Assert.Equal(3, dictionary.Count);

            dictionary.Remove(1);

            Assert.Equal(2, dictionary.Count);
        }

        [Fact]
        public void FindingDictionaryItems()
        {
            var dictionary = new Dictionary<int, string>()
            {
                {1,"one" },
                {2,"two" },
                {3,"three" }
            };
            Assert.Throws<KeyNotFoundException>(() => dictionary[4]); //throw KeyNotFoundException when key doesn't exist

            var value = String.Empty;
            if (dictionary.ContainsKey(1))
                value = dictionary[1];
            Assert.Equal("one", value);

            value = String.Empty;
            Assert.True(dictionary.TryGetValue(1, out value));
            Assert.Equal("one", value);
        }
    }
}
