using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WarehouseMS.Presentation.Models;

public class ObservableItemCollection<T> : ObservableCollection<T>
    where T : INotifyPropertyChanged
{
    public ObservableItemCollection()
    {
        CollectionChanged += ObservableCollectionChanged;
    }

    public ObservableItemCollection(IEnumerable<T> pItems) : this()
    {
        foreach (var item in pItems)
        {
            this.Add(item);
        }
    }

    private void ObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Object item in e.NewItems)
            {
                ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
            }
        }
        if (e.OldItems != null)
        {
            foreach (Object item in e.OldItems)
            {
                ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
            }
        }
    }

    private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender));
        OnCollectionChanged(args);
    }
}