using System;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGoal : MonoBehaviour
{
    public int sceneChange;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Box")) {
            SceneManager.LoadScene(sceneChange);

            JsonEvent eventFormat = new JsonEvent
            {
                Type = "MoveScene",
                ClientID = SocketController.clientId,
                Info = Convert.ToString(sceneChange)
            };
            JsonFormat format = new JsonFormat
            {
                Type = "MoveScene",
                Data = JsonUtility.ToJson(eventFormat)
            };
            
            String str = JsonUtility.ToJson(format);
            SocketController.SendData(str);
            
            // Byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
            //
            // NetworkStream stream = SocketController.GetInstance().GetStream();
            // stream.Write(data, 0, data.Length);
            // stream.Flush();

            foreach (var player in AddPlayer.GetCollection())
            {
                player.Value.transform.position = new Vector3(-5.21f, -1.61f, 0.0f);
            }
        }
    }
}
