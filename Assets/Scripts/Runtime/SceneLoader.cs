using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader 
{  
    
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void LoadScene(int numberScene){
        SceneManager.LoadScene(numberScene);
    }

    public int GetCurrentScene(){
        return SceneManager.GetActiveScene().buildIndex;
    }
}
