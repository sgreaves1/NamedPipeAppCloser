using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using server.Command;
using server.Model;

namespace server.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            InitCommands();
        }

        public ICommand AddClientCommand { get; set; }

        private void InitCommands()
        {
            AddClientCommand = new RelayCommand(ExecuteAddClientCommand);
        }

        private void ExecuteAddClientCommand()
        {
            ClientModel model = new ClientModel();
        }
    }
}
