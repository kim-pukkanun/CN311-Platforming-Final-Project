using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{

    public GameObject panel;
    private int n = 1;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    public void JoinServer() {
        if (n == 1) {
            panel.SetActive(true);
            n -= 1;
        } else {
            panel.SetActive(false);
            n += 1;
        }
        
    }

    public void GetIP(string IP) {
        Debug.Log(IP);
    }

    public void GetPort(string port) {
        Debug.Log(port);
    }
}
