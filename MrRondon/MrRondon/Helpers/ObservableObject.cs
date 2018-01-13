using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MrRondon.Helpers
{
    public class ObservableObject : INotifyPropertyChanged
    {
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;

            backingStore = value;
            onChanged?.Invoke();
            Notify(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void Notify(string propertyName = "")
        {
            var changed = PropertyChanged;
            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}