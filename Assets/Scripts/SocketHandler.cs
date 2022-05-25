using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    // case "ActivePlayers":
                    //     OnActivePlayers(JsonUtility.FromJson<JsonActivePlayer>(format.Data));
                    //     break;
                    case "Connection":
                        OnConnection(JsonUtility.FromJson<JsonConnection>(format.Data));
                        break;
                    case "PlayerPosition":
                        OnPlayerPosition(JsonUtility.FromJson<JsonPlayerPosition>(format.Data));
                        break;
                    case "MoveScene":
                        OnMoveScene(JsonUtility.FromJson<JsonEvent>(format.Data));
                        break;
                    case "Death":
                        OnDeath();
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
            player.transform.eulerAngles = new Vector3(0, 0, data.Rotate);

            PlayerController controller = player.GetComponent<PlayerController>();
            if (data.MoveX > 0.1f && !controller.isDisable) {
                controller.animator.SetBool("isRunning", true);
                player.transform.localScale = new Vector2(1, 1);
            } else if (data.MoveX < -0.1f && !controller.isDisable) {
                controller.animator.SetBool("isRunning", true);
                player.transform.localScale = new Vector2(-1, 1);
            } else {
                controller.animator.SetBool("isRunning", false);
            }
        });
    }

    // private void OnActivePlayers(JsonActivePlayer data)
    // {
    //     UnityMainThreadDispatcher.Instance().Enqueue(() =>
    //     {
    //         foreach (String player in data.Players)
    //         {
    //             AddPlayer.CreatePlayer(player);
    //         }
    //     });
    // }

    private void OnMoveScene(JsonEvent data)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            Debug.Log("Move to scene" + data.Info);
            SceneManager.LoadScene(Convert.ToInt32(data.Info));
        });
    }
    
    private void OnDeath()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            SceneManager.LoadScene(1);
        });
    }
}