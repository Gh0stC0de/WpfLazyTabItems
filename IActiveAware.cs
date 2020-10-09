using System;

namespace LazyTabItems
{
    public interface IActiveAware
    {
        event EventHandler<bool> IsActiveChanged;

        bool IsActive { get; set; }
    }
}