using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LazyTabItems.Annotations;

namespace LazyTabItems.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Save()
        {
            Console.WriteLine($"{GetType().Name} - Saving...");
        }
    }
}