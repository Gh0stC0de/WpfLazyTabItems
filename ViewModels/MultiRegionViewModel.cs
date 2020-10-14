using System;
using System.Collections.Generic;
using LazyTabItems.Models;

namespace LazyTabItems.ViewModels
{
    public class MultiRegionViewModel : ViewModelBase, IActiveAware, IDetailViewModel<MasterItem>
    {
        private bool _isActive;
        private bool _isInitialized;

        public MultiRegionViewModel()
        {
            ChildViewModels = new Dictionary<string, ViewModelBase>
            {
                {"RegionEViewModel", new RegionEViewModel()},
                {"RegionFViewModel", new RegionFViewModel()},
            };

            RegionEViewModel = (RegionEViewModel) ChildViewModels["RegionEViewModel"];
            RegionFViewModel = (RegionFViewModel) ChildViewModels["RegionFViewModel"];

            IsActiveChanged += OnIsActiveChanged;
        }

        /// <summary>
        /// Contains all child view models.
        /// </summary>
        public Dictionary<string, ViewModelBase> ChildViewModels { get; }

        /// <summary>
        ///     Direct access to a specific child view model.
        /// </summary>
        /// <example>Easy use in xaml view.</example>
        public RegionEViewModel RegionEViewModel { get; set; }
        
        public RegionFViewModel RegionFViewModel { get; set; }
        
        public object ParentViewModel { get; set; }

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

        public event EventHandler<MasterItem> MasterChanged;
        
        public void RaiseMasterChanged(MasterItem master)
        {
            MasterChanged?.Invoke(this, master);
        }

        private void OnIsActiveChanged(object sender, bool isActive)
        {
            if (isActive) Initialize();
            else Save();

            RegionEViewModel.IsActive = isActive;
            RegionFViewModel.IsActive = isActive;
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            _isInitialized = true;
            Console.WriteLine($"Initializing {GetType().Name}");
        }

        public override void Save()
        {
            foreach (var viewModel in ChildViewModels)
            {
                viewModel.Value.Save();
            }

            base.Save();
        }
    }
}