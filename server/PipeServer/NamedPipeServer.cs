using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using server.Model;

namespace server.PipeServer
{
    public class NamedPipeServer
    {
        private static int _processId;

        public static event EventHandler InformationReady;

        public NamedPipeServer(int processId)
        {
            _processId = processId;
        }

        public void CreatePipe()
        {
            InformationReady?.Invoke(this, new InformationReadyEventArgs() { Message = " Named Pipe Server "+ "Pipe" + _processId + " \n" });
            InformationReady?.Invoke(this, new InformationReadyEventArgs() { Message = " Pipe" + _processId + " Waiting for client connect...\n" });

            Thread server;

            server = new Thread(ServerThread);
            server.Start();

            Thread.Sleep(250);

            if (server.Join(250))
            {
                InformationReady?.Invoke(this, new InformationReadyEventArgs() { Message = " Pipe" + _processId + " finished \n" });

                server = null;
            }
        }

        private static void ServerThread(object data)
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Pipe" + _processId, PipeDirection.Out, 1);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            pipeServer.WaitForConnection();

            InformationReady?.Invoke(null, new InformationReadyEventArgs() { Message = " Pipe" + _processId + " Client Connected\n" });
            
            try
            {
                StreamString ss = new StreamString(pipeServer);

                ss.WriteString("I am the one true server!");
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            pipeServer.Close();
        }
    }
}
