using System;
using System.Net.Sockets;
using System.Collections.Generic;

namespace CN311_Platforming_Final_Project_Server
{
    public class ClientCollection
    {
        private static Dictionary<String, TcpClient> clientCollection;

        public static Dictionary<String, TcpClient> GetInstance()
        {
            if (clientCollection == null)
            {
                clientCollection = new Dictionary<String, TcpClient>();
            }

            return clientCollection;
        }

        public static void Add(String key, TcpClient tcpClient)
        {
            if (!GetInstance().ContainsKey(key))
            {
                GetInstance().Add(key, tcpClient);
            }
        }
    }
}