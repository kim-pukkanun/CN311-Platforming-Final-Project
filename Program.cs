using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace CN311_Platforming_Final_Project_Server
{
    class Program
    {
        /*
         * Define your IP address and Port here.
         */
        private const String Ip = "192.168.1.104";
        private const Int32 Port = 13000;
        
        static void Main(string[] args)
        {
            IPAddress localIp = IPAddress.Parse(Ip);
            TcpListener serverSocket = new TcpListener(localIp, Port);
            TcpClient clientSocket = default(TcpClient);

            int clientCounter = 0;
            
            serverSocket.Start();
            Console.WriteLine("[INFO]: Server started.");

            while (true)
            {
                clientCounter++;
                clientSocket = serverSocket.AcceptTcpClient();
                //clientSocket.ReceiveTimeout = 5000;
                HandleClient handleClient = new HandleClient();
                handleClient.Start(clientSocket, Convert.ToString(clientCounter));
            }
                
            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("[INFO]: Server stopped.");
        }
    }
}