using System;
using System.Net.Sockets;
using UnityEngine;

public static class SocketController
{
    private static TcpClient client;
    
    public static bool SetConnection(String ip, Int32 port)
    {
        try
        {
            client = new TcpClient(ip, port);
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