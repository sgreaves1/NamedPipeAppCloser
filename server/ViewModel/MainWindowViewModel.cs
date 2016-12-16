using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyLibrary.SelectPanel;
using server.Command;
using server.Model;

namespace server.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<IPanelItem> _models = new ObservableCollection<IPanelItem>();
        private IPanelItem _selectedItem;

        public MainWindowViewModel()
        {
            InitCommands();
        }

        public ObservableCollection<IPanelItem> Models
        {
            get {  return _models; }
            set
            {
                _models = value;
                OnPropertyChanged();
            }
        }

        public IPanelItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddClientCommand { get; set; }

        private void InitCommands()
        {
            AddClientCommand = new RelayCommand(ExecuteAddClientCommand);
        }

        private void ExecuteAddClientCommand()
        {
            Models.Add(new ClientModel());
        }
    }
}
