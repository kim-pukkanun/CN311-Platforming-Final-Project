using System;
using System.IO;
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
            try
            {
                data = new Byte[4096];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                JsonFormat format = JsonUtility.FromJson<JsonFormat>(responseData);
                Debug.Log(format.Type);
                switch (format.Type)
                {
                    case "Connection":
                        OnConnection(JsonUtility.FromJson<JsonConnection>(format.Data));
                        break;
                    case "PlayerPosition":
                        OnPlayerPosition(JsonUtility.FromJson<JsonPlayerPosition>(format.Data));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    private void OnConnection(JsonConnection data)
    {
        if (data.Type == "Connect")
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                AddPlayer.CreatePlayer(data.ClientID);
            });
        } else if (data.Type == "Disconnect")
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                AddPlayer.RemovePlayer(data.ClientID);
            });
        }
        Debug.Log(data.Type + " " + data.ClientID);
    }

    private void OnPlayerPosition(JsonPlayerPosition data)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            GameObject player = AddPlayer.GetCollection()[data.ClientID];
            //Debug.Log(player.transform.position);
            player.transform.position = new Vector3(data.X, data.Y, 0.0f);
        });
    }
}