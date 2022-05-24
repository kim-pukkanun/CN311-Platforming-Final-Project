using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    private GameObject gameObject;
    private Animator animator;

    public void MoveToScene(int sceneID) {
        StartCoroutine(PlayFade(sceneID));
    }

    IEnumerator PlayFade(int sceneID) {
        gameObject = GameObject.Find("LevelChanger");
        animator = gameObject.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene(sceneID);
    }
}
