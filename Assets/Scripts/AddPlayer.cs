using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour
{
    //private static GameObject newPlayer;
    private static Dictionary<String, GameObject> playerCollection;

    public static Dictionary<String, GameObject> GetCollection()
    {
        if (playerCollection == null)
        {
            playerCollection = new Dictionary<string, GameObject>();
        }

        return playerCollection;
    }

    public static void CreatePlayer(string playerId)
    {
        GameObject newPlayer = null;
        GameObject player = GameObject.Find("Player");
        newPlayer = Instantiate(player, new Vector3(0.0f, 0, 0), Quaternion.identity);
        newPlayer.name = playerId;
        newPlayer.GetComponent<PlayerController>().playerID = playerId;
        GetCollection().Add(playerId, newPlayer);
        DontDestroyOnLoad(newPlayer);
    }

    public static void RemovePlayer(string playerId)
    {
        GameObject player = playerCollection[playerId];
        Destroy(player);
        GetCollection().Remove(playerId);
        Debug.Log(playerId + " disconnected.");
    }
}
