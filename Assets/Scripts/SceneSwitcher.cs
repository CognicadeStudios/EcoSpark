using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject CirclePanel;
    public Animator transitionAnim;

    public void LoadSceneCircleFrombutton(string sceneName)
    {    
        StartCoroutine(CircleTransition(sceneName));
    }

    IEnumerator CircleTransition(string sceneName)
    {
        CirclePanel.transform.position = Input.mousePosition;
        transitionAnim.SetTrigger("LeaveScene");
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(sceneName);
        yield return new WaitForEndOfFrame();
        CirclePanel.transform.localPosition = Vector3.zero;
    }
}
