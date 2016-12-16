using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.SelectPanel;

namespace server.Model
{
    public class ClientModel : BaseModel, IPanelItem
    {
        private Process _clientApp;

        public ClientModel()
        {
            _clientApp = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = "C:\\own stuff\\closeappwithpipe\\server\\Client\\bin\\Debug\\Client.exe";

            _clientApp.StartInfo = startInfo;
            _clientApp.Start();
        }
        
        public string Name
        {
            get { return _clientApp?.Id.ToString() ?? ""; }
            set { }
        }
        public bool IsSelected { get; set; }
    }
}
