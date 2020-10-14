using System;

namespace LazyTabItems.ViewModels
{
    public class RegionFViewModel : ViewModelBase, IActiveAware
    {
        private bool _isActive;
        private bool _isInitialized;

        public RegionFViewModel()
        {
            Name = GetType().Name;
            IsActiveChanged += OnIsActiveChanged;
        }

        public string Name { get; set; }

        public event EventHandler<bool> IsActiveChanged;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    IsActiveChanged?.Invoke(this, _isActive);
                }
            }
        }

        private void OnIsActiveChanged(object sender, bool isActive)
        {
            if (isActive) Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            _isInitialized = true;
            Console.WriteLine($"Initializing {GetType().Name}");
        }
    }
}