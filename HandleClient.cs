using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace CN311_Platforming_Final_Project_Server
{
    public class HandleClient
    {
        private TcpClient clientSocket;
        private string clientNo;

        public void Start(TcpClient tcpClient, String no)
        {
            clientSocket = tcpClient;
            clientNo = no;
            Console.WriteLine("[CONNECTION]: Client #" + clientNo + " connected.");
            
            OnConnect("Connect");
            ClientCollection.Add(clientNo, tcpClient);
            Thread thread = new Thread(Handle);
            thread.Start();
        }

        private void Handle()
        {
            Byte[] bytesFrom = new Byte[1024];
            Byte[] sendBytes = null;
            String data;

            while (true)
            {
                try
                {
                    NetworkStream stream = clientSocket.GetStream();
                    int i;
                    while ((i = stream.Read(bytesFrom, 0, bytesFrom.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytesFrom, 0, i);
                        Console.WriteLine("[Client {0}]: {1}", clientNo, data);
                        break;
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("[ERROR]: " + e);
                    break;
                }
            }
            clientSocket.Close();
            ClientCollection.GetInstance().Remove(clientNo);
            Console.WriteLine("[CONNECTION]: Client #" + clientNo + " disconnected.");
            OnConnect("Disconnect");
        }

        private void OnConnect(String type)
        {
            JsonConnection connection = new JsonConnection
            {
                Type = type,
                ClientID = clientNo
            };
            JsonFormat format = new JsonFormat
            {
                Type = "Connection",
                Data = JsonSerializer.Serialize(connection)
            };
            
            String json = JsonSerializer.Serialize(format);
            ClientSocket.SendAll(json, clientNo);
        }
    }
}