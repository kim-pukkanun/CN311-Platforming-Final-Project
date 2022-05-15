using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGoal : MonoBehaviour
{

    public int sceneChange;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Box")) {
            SceneManager.LoadScene(sceneChange);
        }
    }
}
