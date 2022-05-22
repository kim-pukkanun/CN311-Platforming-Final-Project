using System;
using System.Net.Sockets;
using System.Text;

namespace CN311_Platforming_Final_Project_Server
{
    public static class ClientSocket
    {
        public static void Send(String message, String clientNo)
        {
            Byte[] sendBytes = null;
            sendBytes = Encoding.ASCII.GetBytes(message);
            NetworkStream stream = ClientCollection.GetInstance()[clientNo].GetStream();
            stream.Write(sendBytes, 0, sendBytes.Length);
            stream.Flush();
            //Console.WriteLine("[SERVER]: Send data to Client {0}.", clientNo);
        }
        public static void SendAll(String message, String exceptNo = null)
        {
            Byte[] sendBytes = null;
            sendBytes = Encoding.ASCII.GetBytes(message);

            foreach (var client in ClientCollection.GetInstance())
            {
                if (client.Key != exceptNo)
                {
                    client.Value.GetStream().Write(sendBytes, 0, sendBytes.Length);
                    client.Value.GetStream().Flush();
                    //Console.WriteLine("[SERVER]: Send data to Client {0}.", client.Key);
                }
            }
        }
    }
}