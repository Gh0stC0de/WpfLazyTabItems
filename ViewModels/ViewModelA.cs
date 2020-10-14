using System.Collections.Generic;
using System.Linq;
using LazyTabItems.Models;

namespace LazyTabItems.ViewModels
{
    public class ViewModelA : ViewModelBase
    {
        private TabItemData _selectedTab;
        private MasterItem _item;

        public ViewModelA()
        {
            TabItemSource = new List<TabItemData>
            {
                new TabItemData {Header = "Tab B", ViewModel = new ViewModelB()},
                new TabItemData {Header = "Tab C", ViewModel = new ViewModelC()},
                new TabItemData {Header = "Tab D", ViewModel = new ViewModelD()},
                new TabItemData {Header = "Multi Regions", ViewModel = new MultiRegionViewModel{ParentViewModel = this}},
            };
            SelectedTab = TabItemSource.FirstOrDefault();
        }

        public IList<TabItemData> TabItemSource { get; set; }

        public TabItemData SelectedTab
        {
            get => _selectedTab;
            set
            {
                var oldTab = _selectedTab;
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    OnSelectedTabChanged(oldTab, _selectedTab);
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

        private void OnSelectedTabChanged(TabItemData oldTabItemData, TabItemData tabItemData)
        {
            if (oldTabItemData != null && oldTabItemData.ViewModel is IActiveAware oldActiveAware) oldActiveAware.IsActive = false;
            if (tabItemData != null && tabItemData.ViewModel is IActiveAware activeAware) activeAware.IsActive = true;
        }

        private void OnItemChanged(MasterItem item)
        {
            var dependentViewModels = TabItemSource
                .Where(i => i.ViewModel is IDetailViewModel<MasterItem>)
                .Select(i => i.ViewModel)
                .Cast<IDetailViewModel<MasterItem>>();

            foreach (var viewModel in dependentViewModels)
            {
                viewModel.RaiseMasterChanged(item);
            }
        }
    }
}