using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using server.Command;
using server.Model;

namespace server.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<ClientModel> _models = new ObservableCollection<ClientModel>();

        public MainWindowViewModel()
        {
            InitCommands();
        }

        public ObservableCollection<ClientModel> Models
        {
            get {  return _models; }
            set
            {
                _models = value;
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
