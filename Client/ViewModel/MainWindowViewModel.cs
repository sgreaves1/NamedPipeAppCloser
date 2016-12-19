namespace Client.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _output;

        public string Output
        {
            get { return _output; }
            set
            {
                _output = value;
                OnPropertyChanged();
            }
        }
    }
}
