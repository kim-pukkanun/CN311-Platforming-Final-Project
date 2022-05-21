using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIPPort : MonoBehaviour
{
    private static string IP;
    private static string Port;

    public void GetIP(string ip) {
        Debug.Log("Your IP: " + ip);
        IP = ip;
    }

    public void GetPort(string port) {
        Debug.Log("Your Port: " + port);
        Port = port;
    }

    public void ConnectServer() {
        Debug.Log("You have connect to " + IP + ", " + Port);
    }
}
