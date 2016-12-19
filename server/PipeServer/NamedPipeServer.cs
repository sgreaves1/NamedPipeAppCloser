using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace server.PipeServer
{
    public class NamedPipeServer
    {
        private static int numThreads = 4;

        void CreatePipe()
        {
            int i;
            Thread[] servers = new Thread[numThreads];

            //TextBlock1.Text += "*** Named pipe server stream with impersonation example ***\n";
            //TextBlock1.Text += "Waiting for client connect...\n";

            for (i = 0; i < numThreads; i++)
            {
                servers[i] = new Thread(ServerThread);
                servers[i].Start();
            }
            Thread.Sleep(250);
            while (i < 0)
            {
                for (int j = 0; j < numThreads; j++)
                {
                    if (servers[j] != null)
                    {
                        if (servers[j].Join(250))
                        {
                            //TextBlock1.Text += "Server thread" + servers[j].ManagedThreadId + " finished \n";
                            servers[j] = null;
                            i--;
                        }
                    }
                }
            }
        }

        private static void ServerThread(object data)
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("testPipe", PipeDirection.Out, numThreads);

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
