using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void PlayGameSearch()
    {
        SceneManager.LoadScene("Search");
    }

    public void PlayGameTemple()
    {
        SceneManager.LoadScene("TempleRun");
    }


    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }


}
