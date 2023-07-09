using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour


{
    public Animator transitionAnim;
    public string sceneName;



    public void challengeButtonOnClick()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        //Switch scene on click challenge button
        SceneManager.LoadScene(sceneName);

    }


}
