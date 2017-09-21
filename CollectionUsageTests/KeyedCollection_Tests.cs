using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "KeyedCollection<TKey,TValue>")]
    public class KeyedCollection_Tests
    {
        /// <summary>
        /// KeyedCollection<TKey,TValue> is an abstract class which must be implemented to use the collection features.
        /// There is one abstract method to impliment:TKey GetKeyForItem(TValue) 
        /// Collection is easier to manage since you dont have to define the key twice, once in TKey and also live on TValue.
        /// The Collection has a list and a dictionary which are kept in sync, which means you could lookup a value by index or TKey
        /// Could also customize the add, remove, and clear functionality like collection<T> does
        /// Namespace:System.Collections.ObjectModel
        /// </summary>

        [Fact]
        public void UsingKeyedCollection()
        {
            var dictionary = new PersonByIdDictionary()
            {
                new Person(1,"Hello","World"),
                new Person(2,"John","Smith"),
                new Person(3,"John", "Adams")
            };

            var firstPerson = dictionary[1]; //Finding value by key
            Assert.Equal("Hello", firstPerson.FirstName);
            Assert.Equal("World", firstPerson.LastName);

            var list = (IList<Person>)dictionary;//getting access to list index explicitly since both TKey and index are integers; not need if both are not integers
            firstPerson = list[0];
            Assert.Equal("Hello", firstPerson.FirstName);
            Assert.Equal("World", firstPerson.LastName);
        }


        private class PersonByIdDictionary : KeyedCollection<int, Person>
        {
            protected override int GetKeyForItem(Person person)
            {
                return person.Id; //Gets person ID for TKey
            }
        }

        private struct Person
        {
            public int Id { get; }
            public string FirstName { get; }
            public string LastName { get; }
            public Person(int id, string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
                Id = id;
            }
        }
    }
}
