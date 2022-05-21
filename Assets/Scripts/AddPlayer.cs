using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour
{
    public GameObject player;

    public void CreatePlayer() {
        Instantiate(player, new Vector3(0.0f, 0, 0), Quaternion.identity);
    }
}
