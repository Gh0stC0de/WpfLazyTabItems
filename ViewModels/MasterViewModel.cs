using System.Collections.Generic;
using System.Linq;
using LazyTabItems.Models;

namespace LazyTabItems.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        private MasterItem _selectedMaster;
        private IList<MasterItem> _masters;

        public MasterViewModel()
        {
            DetailViewModel = new ViewModelA();
            Masters = new List<MasterItem>
            {
                new MasterItem {Name = "Master Item 1"},
                new MasterItem {Name = "Master Item 2"},
                new MasterItem {Name = "Master Item 3"}
            };
            SelectedMaster = Masters.FirstOrDefault();
        }

        public IList<MasterItem> Masters
        {
            get => _masters;
            set
            {
                _masters = value;
                OnPropertyChanged();
            }
        }

        public MasterItem SelectedMaster
        {
            get => _selectedMaster;
            set
            {
                if (_selectedMaster != value)
                {
                    _selectedMaster = value;
                    OnSelectedMasterItemChanged(_selectedMaster);
                    OnPropertyChanged();
                }
            }
        }

        public ViewModelA DetailViewModel { get; set; }

        private void OnSelectedMasterItemChanged(MasterItem selectedMaster)
        {
            DetailViewModel.Item = selectedMaster;
        }
    }
}