using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace server.PipeServer
{
    public class NamedPipeServer
    {
        private static int numThreads = 0;

        public NamedPipeServer(int processId)
        {
            Thread server;

            numThreads = processId;

            //TextBlock1.Text += "*** Named pipe server stream with impersonation example ***\n";
            //TextBlock1.Text += "Waiting for client connect...\n";
            
            server = new Thread(ServerThread);
            server.Start();
            
            Thread.Sleep(250);
            
            if (server != null)
            {
                if (server.Join(250))
                {
                    //TextBlock1.Text += "Server thread" + servers[j].ManagedThreadId + " finished \n";
                    server = null;
                }
            }
        }

        private static void ServerThread(object data)
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("Pipe" + numThreads, PipeDirection.Out, 1);

            int threadId = Thread.CurrentThread.ManagedThreadId;

            pipeServer.WaitForConnection();

            Console.WriteLine("Client connected on thread[{0}].", threadId);

            try
            {
                StreamString ss = new StreamString(pipeServer);

                ss.WriteString("I am the one true server!");
                string filename = ss.ReadString();

                // Read in the contents of the file while impersonating the client.
                ReadFileToStream fileReader = new ReadFileToStream(ss, filename);

                // Display the name of the user we are impersonating.
                Console.WriteLine("Reading file: {0} on thread[{1}] as user: {2}.",
                    filename, threadId, pipeServer.GetImpersonationUserName());
                pipeServer.RunAsClient(fileReader.Start);
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            pipeServer.Close();
        }
    }
}
