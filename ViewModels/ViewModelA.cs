using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using LazyTabItems.Models;

namespace LazyTabItems.ViewModels
{
    public class ViewModelA : ViewModelBase
    {
        private TabItem _selectedTab;
        private MasterItem _item;

        public ViewModelA()
        {
            TabViewModels = new List<object>
            {
                new ViewModelB(),
                new ViewModelC(),
                new ViewModelD(),
                new MultiRegionViewModel{ParentViewModel = this},
            };
        }

        public IList<object> TabViewModels { get; set; }

        public TabItem SelectedTab
        {
            get => _selectedTab;
            set
            {
                var oldTab = _selectedTab;
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    OnSelectedTabChanged(oldTab?.DataContext, _selectedTab.DataContext);
                }
            }
        }

        public MasterItem Item
        {
            get => _item;
            set
            {
                if (_item != value)
                {
                    _item = value;
                    OnItemChanged(_item);
                    OnPropertyChanged();
                }
            }
        }

        private void OnSelectedTabChanged(object oldTabDataContext, object selectedTabDataContext)
        {
            if (oldTabDataContext != null && oldTabDataContext is IActiveAware oldActiveAware) oldActiveAware.IsActive = false;
            if (selectedTabDataContext != null && selectedTabDataContext is IActiveAware activeAware) activeAware.IsActive = true;
        }

        private void OnItemChanged(MasterItem item)
        {
            var dependentViewModels = TabViewModels
                .Where(i => i is IDetailViewModel<MasterItem>)
                .Select(i => i)
                .Cast<IDetailViewModel<MasterItem>>();

            foreach (var viewModel in dependentViewModels)
            {
                viewModel.RaiseMasterChanged(item);
            }
        }
    }
}