using System;

namespace LazyTabItems
{
    public interface IDetailViewModel<T>
    {
        event EventHandler<T> MasterChanged;
        void RaiseMasterChanged(T master);
    }
}