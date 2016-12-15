using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class ClientModel : BaseModel
    {
        private Process _clientApp;

        ClientModel()
        {
            _clientApp = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = "C:\\own stuff\\\closeappwithpipe\\server\\Client\\bin\\Debug\\Client.exe";

            _clientApp.Start();
        }
    }
}
