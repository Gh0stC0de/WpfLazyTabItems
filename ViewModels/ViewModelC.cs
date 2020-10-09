using System;
using LazyTabItems.Models;

namespace LazyTabItems.ViewModels
{
    public class ViewModelC : ViewModelBase, IActiveAware, IDetailViewModel<MasterItem>
    {
        private bool _isActive;
        private bool _isInitialized;
        private IDetailViewModel<MasterItem> _detailViewModelImplementation;
        private string _masterName;

        public ViewModelC()
        {
            Name = GetType().Name;
            Console.WriteLine($"Constructing {Name}");
            IsActiveChanged += OnIsActiveChanged;
            MasterChanged += (sender, item) => MasterName = item.Name;
        }

        public string Name { get; }
        public string MasterName
        {
            get => _masterName;
            set
            {
                _masterName = value;
                OnPropertyChanged();
            }
        }
        public event EventHandler<bool> IsActiveChanged;
        public event EventHandler<MasterItem> MasterChanged;

        public void RaiseMasterChanged(MasterItem master)
        {
            MasterChanged?.Invoke(this, master);
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value != _isActive)
                {
                    _isActive = value;
                    IsActiveChanged?.Invoke(this, _isActive);
                }
            }
        }

        private void OnIsActiveChanged(object sender, bool b)
        {
            if (b) Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            _isInitialized = true;
            Console.WriteLine($"Initializing {Name}");
        }
    }
}