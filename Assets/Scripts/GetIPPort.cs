using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetIPPort : MonoBehaviour
{
    private static string IP;
    private static string Port;

    public void GetIP(string ip) {
        //Debug.Log("Your IP: " + ip);
        IP = ip;
    }

    public void GetPort(string port) {
        //Debug.Log("Your Port: " + port);
        Port = port;
    }

    public void ConnectServer()
    {
        Int32 SocketPort = Convert.ToInt32(Port);

        if (SocketController.SetConnection(IP, SocketPort))
        {
            Debug.Log("You have connect to " + IP + ":" + Port);
            SocketHandler socketHandler = new SocketHandler();
            Thread thread = new Thread(socketHandler.Handle);
            thread.Start();
            SceneManager.LoadScene(1);
        }
    }
}
