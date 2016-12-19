using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.SelectPanel;
using server.PipeServer;

namespace server.Model
{
    public class ClientModel : BaseModel, IPanelItem
    {
        private Process _clientApp;

        private NamedPipeServer _pipeServer;

        public EventHandler InformationReady;
        
        public void StartClient()
        {
            _clientApp = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = "C:\\own stuff\\closeappwithpipe\\server\\Client\\bin\\Debug\\Client.exe";

            _clientApp.StartInfo = startInfo;
            _clientApp.Start();

            _pipeServer = new NamedPipeServer((int)_clientApp?.Id);
            _pipeServer.InformationReady += PipeServerOnInformationReady;
            _pipeServer.CreatePipe();
        }

        private void PipeServerOnInformationReady(object sender, EventArgs eventArgs)
        {
            InformationReady?.Invoke(this, eventArgs);
        }

        public string Name
        {
            get { return _clientApp?.Id.ToString() ?? ""; }
            set { }
        }
        public bool IsSelected { get; set; }
    }
}
