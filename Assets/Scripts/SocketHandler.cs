using System;
using System.Net.Sockets;
using UnityEngine;

public class SocketHandler
{
    public void Handle()
    {
        Byte[] data;

        NetworkStream stream = SocketController.GetInstance().GetStream();
        
        while (true)
        {
            data = new Byte[1024];
            String responseData = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            JsonFormat format = JsonUtility.FromJson<JsonFormat>(responseData);
            switch (format.Type)
            {
                case "Connection": 
                    OnConnection(JsonUtility.FromJson<JsonConnection>(format.Data));
                    break;
                default:
                    break;
            }
        }
    }

    private void OnConnection(JsonConnection data)
    {
        if (data.Type == "Connect")
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                GameObject gameObject = new GameObject("Playerrrr");
                AddPlayer player = gameObject.AddComponent<AddPlayer>();
                player.player = GameObject.Find("Player");
                player.CreatePlayer();
            });
        }
        Debug.Log(data.Type + " " + data.ClientID);
    }
}