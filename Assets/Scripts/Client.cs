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

    public static void run()
    {
        Thread thr = new Thread(RunClientAsync);
        thr.Start();
    }
    public static void Send(string msg)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] bytes = encoding.GetBytes(msg);
        pipeClient.Write(bytes, 0, bytes.Length);
    }

    public static void RunClientAsync()
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
                    Send(GameManager.Scenename + "\0");
                    GameManager.ModeSent=true;

                }
            
                string message=GUIInterface.CheckButton();
                if (message!="\0"){
                  Send(message);
                }
                else{
                    Send("-1,\0");
                }
                
                
                StringBuilder messageBuilder = new StringBuilder();
                string messageChunk = string.Empty;
                byte[] messageBuffer = new byte[50];
                pipeClient.Read(messageBuffer, 0, messageBuffer.Length);
                messageChunk = Encoding.UTF8.GetString(messageBuffer);
                messageBuilder.Append(messageChunk);
                messageBuffer = new byte[messageBuffer.Length];
               // GUIInterface.Messages.Add(messageBuilder.ToString());
                if (messageBuilder.ToString()[0]!='n'){
                    GUIInterface.Messages.Add(messageBuilder.ToString());
                }

                string[] parameters = messageBuilder.ToString().Split(',');
                if (parameters[0] == "-1")
                {
                    pipeClient.Close();
                    //Console.WriteLine("Closed");
                    break;
                }
               // Send("pleasework\0");
             
            }
            int x = 0;

        }
    }
}

