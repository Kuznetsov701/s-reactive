using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Reactive
{
    public class ReactiveCollectionItem<T> : ReactiveBase<T>, IReactive<T>, IDisposable where T : class, INotifyPropertyChanged
    {
        ObservableCollection<T> collection;
        string[] propertyes;

        public ReactiveCollectionItem(ObservableCollection<T> collection, params string[] propertyes)
        {
            this.propertyes = propertyes ?? throw new ArgumentNullException(nameof(propertyes));
            (this.collection = collection ?? throw new ArgumentNullException(nameof(collection))).CollectionChanged += Collection_CollectionChanged;
            SubscribeOnChangedOfAllItems();
        }

        private void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (INotifyPropertyChanged i in e.NewItems)
                    i.PropertyChanged += I_PropertyChanged;
            if(e.OldItems != null)
                foreach(INotifyPropertyChanged i in e.OldItems)
                    i.PropertyChanged -= I_PropertyChanged;
        }

        private void I_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(propertyes.Contains(e.PropertyName))
                Invoke(sender as T);
        }

        public override void Dispose()
        {
            UnsubscribeOnChangedOfAllItems();
            collection.CollectionChanged -= Collection_CollectionChanged;
        }

        private void SubscribeOnChangedOfAllItems()
        {
            foreach (INotifyPropertyChanged i in collection)
                i.PropertyChanged += I_PropertyChanged;
        }

        private void UnsubscribeOnChangedOfAllItems()
        {
            foreach (INotifyPropertyChanged i in collection)
                i.PropertyChanged -= I_PropertyChanged;
        }
    }
}