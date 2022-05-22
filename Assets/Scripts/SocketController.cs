using System;
using System.Net.Sockets;
using UnityEngine;

public static class SocketController
{
    private static TcpClient client;
    public static String clientId;
    
    public static bool SetConnection(String ip, Int32 port)
    {
        try
        {
            client = new TcpClient(ip, port);
            Byte[] data = new Byte[4096];
            NetworkStream stream = client.GetStream();
            Int32 bytes = stream.Read(data, 0, data.Length);
            String responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            JsonEvent format = JsonUtility.FromJson<JsonEvent>(responseData);
            
            if (format.Type == "MyID")
            {
                clientId = format.ClientID;
            }

            return true;
        }
        catch (ArgumentNullException e)
        {
            Debug.Log("ArgumentNullException: " + e);
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }

        return false;
    }
    public static TcpClient GetInstance()
    {
        return client;
    }

    public static void SendData(String message)
    {
        Byte[] data = new Byte[4096];
        data = System.Text.Encoding.ASCII.GetBytes(message);
        NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);
        stream.Flush();
    }

    public static void CloseConnection()
    {
        if (client != null)
        {
            Debug.Log("Close Connection");
            client.GetStream().Close();
            client.Close();
            client = null;
        }
    }
}