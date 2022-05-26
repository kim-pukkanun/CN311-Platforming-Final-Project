using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public static class SocketController
{
    public static TcpClient client;
    public static String clientId;
    private static bool sent;
    
    public static bool SetConnection(String ip, Int32 port)
    {
        try
        {
            client = new TcpClient(ip, port);
            Byte[] data = new Byte[4096];
            NetworkStream stream = client.GetStream();
            Int32 bytes = stream.Read(data, 0, data.Length);
            String responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            JsonFormat format = JsonUtility.FromJson<JsonFormat>(responseData);
            
            if (format.Type == "OnConnect")
            {
                JsonOnConnect connect = JsonUtility.FromJson<JsonOnConnect>(format.Data);
                clientId = connect.ClientID;
                OnConnect onConnect = new OnConnect();
                onConnect.Players = connect.Players;
                Thread thread = new Thread(onConnect.Start);
                thread.Start();
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    foreach (String player in connect.Players)
                    {
                        AddPlayer.CreatePlayer(player);
                    }
                });
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
        //Thread.Sleep(15);
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