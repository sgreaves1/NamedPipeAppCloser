﻿using System;
using System.Collections.ObjectModel;
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
        private string _textToSend;
        private string _information;

        public MainWindowViewModel()
        {
            InitCommands();

            TextToSend = "Text to send.";
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

        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                OnPropertyChanged();
            }
        }

        public string Information
        {
            get { return _information; }
            set
            {
                _information = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddClientCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        private void InitCommands()
        {
            AddClientCommand = new RelayCommand(ExecuteAddClientCommand);
            ClearCommand = new RelayCommand(ExecuteClearCommand, CanExecuteClearCommand);
        }

        private bool CanExecuteClearCommand()
        {
            if (TextToSend != "")
                return true;

            return false;
        }

        private void ExecuteClearCommand()
        {
            TextToSend = "";
        }

        private void ExecuteAddClientCommand()
        {
            ClientModel client = new ClientModel();
            client.InformationReady += InformationReady;
            client.StartClient();
            Models.Add(client);
        }

        private void InformationReady(object sender, EventArgs eventArgs)
        {
            Information += ((InformationReadyEventArgs) eventArgs).Message;
        }
    }
}
