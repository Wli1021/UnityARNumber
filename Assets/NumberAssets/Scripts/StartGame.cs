using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour


{
    
    public string sceneName;



    public void startButtonOnClick()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        
        yield return new WaitForSeconds(1.5f);

        //Switch scene on click  buttonstart
        SceneManager.LoadScene(sceneName);

    }


}
