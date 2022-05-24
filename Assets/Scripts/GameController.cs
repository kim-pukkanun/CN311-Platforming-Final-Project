using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Instance
    {
         get { return instance; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if ((SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Ending")) || (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("MainMenu"))) {
            Destroy(this.gameObject);
        }
    }

}
