using System.Collections.Generic;

namespace server.PipeServer
{
    /// <summary>
    /// A class used to hold a collection of named pipe servers
    /// </summary>
    public class NamedPipeServerCollection
    {
        private Dictionary<int, NamedPipeServer> openwith = new Dictionary<int, NamedPipeServer>();

        public void CreateAPipeServer(int processId)
        {
            openwith.Add(processId, new NamedPipeServer(processId));
        }
    }
}
