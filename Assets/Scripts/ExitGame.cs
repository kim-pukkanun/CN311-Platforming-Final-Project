using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuiteGame() {
        Application.Quit();
        Debug.Log("Quit");
    }
}
