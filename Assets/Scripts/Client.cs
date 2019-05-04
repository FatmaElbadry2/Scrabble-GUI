using System;
using System.IO.Pipes;
using System.Threading;
using System.IO;
using System.Text;

public class Client
{
    public static int threadcounter = 1;
    public static NamedPipeClientStream pipeClient;
    //public GUIInterface guiinterface=new GUIInterface();
    public Client()
    {
    }

    public void run()
    {
        Thread thr = new Thread(RunClientAsync);
        thr.Start();
    }
    public void Send(string msg)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] bytes = encoding.GetBytes(msg);
        pipeClient.Write(bytes, 0, bytes.Length);
    }

    public void RunClientAsync()
    {
        using (pipeClient = new NamedPipeClientStream(".", "newpipe", PipeDirection.InOut))
        {
            // Console.WriteLine("Connecting to server...");
            pipeClient.Connect();
            //  Console.WriteLine("Connected :)");
            while (true)
            {
                if (GameManager.Scenename != "\0" && !GameManager.ModeSent)
                {
                    Send(GameManager.Scenename + ",\0");
                    GameManager.ModeSent=true;

                }

                StringBuilder messageBuilder = new StringBuilder();
                string messageChunk = string.Empty;
                byte[] messageBuffer = new byte[40];
                pipeClient.Read(messageBuffer, 0, messageBuffer.Length);
                messageChunk = Encoding.UTF8.GetString(messageBuffer);
                messageBuilder.Append(messageChunk);
                messageBuffer = new byte[messageBuffer.Length];
                GUIInterface.Messages.Add(messageBuilder.ToString());
                string[] parameters = messageBuilder.ToString().Split(',');
                if (parameters[0] == "-1")
                {
                    pipeClient.Close();
                    //Console.WriteLine("Closed");
                    break;
                }
                string message=GUIInterface.CheckButton();
                if (message!="\0"){
                  Send(message);
                }
            }
            int x = 0;

        }
    }
}

