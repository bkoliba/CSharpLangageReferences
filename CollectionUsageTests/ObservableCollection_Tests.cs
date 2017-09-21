using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CollectionUsageTests
{
    [Trait("Collection Usage", "ObservableCollection<T>")]
    public class ObservableCollection_Tests
    {
        private ITestOutputHelper _output;

        public ObservableCollection_Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// ObservableCollection<T> derives from Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
        /// ObservableCollection<T> will raise CollectionChanged ever when adding or removing items from the collection.
        /// Namespace:System.Collections.ObjectModel
        /// </summary>

        [Fact]
        public void UsingObservableCollectionsToAddAndRemoveItems_ShouldRaiseEvents()
        {
            var list = new ObservableCollection<int>() { 1, 2, 3 };
            list.CollectionChanged += List_CollectionChanged; //wire up collection change event which  will be raise when the collection has changed

            list.Add(4);
            //NotifyCollectionChangedEventArgs on CollectionChange Event values
            //Action: Add
            //New Items: 4
            //Old Items: 

            list.Remove(4);
            //NotifyCollectionChangedEventArgs on CollectionChange Event values
            //Action: Remove
            //New Items:
            //Old Items: 4

            list.Remove(10);
            //will not raise event since 10 is not in the collection to remove, so no changes happen to the collection
        }

        private void List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _output.WriteLine($"Action: {e.Action}");
            _output.WriteLine($"New Items: {String.Join(",", e.NewItems?.Cast<int>() ?? new int[] { })}");
            _output.WriteLine($"Old Items: {String.Join(",", e.OldItems?.Cast<int>() ?? new int[] { })}");
        }
    }
}
