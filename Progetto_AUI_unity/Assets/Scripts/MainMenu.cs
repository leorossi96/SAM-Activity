using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public LevelSet levelSet = null;

    public void PlayGameSearch()
    {
        SceneManager.LoadScene("Search");
    }

    public void LoadMenuLogin()
    {
        levelSet = GameObject.Find("LevelSet").GetComponent<LevelSet>();
        Destroy(levelSet.gameObject);
        SceneManager.LoadScene("Menu");
    }

    public void PlayGameRunFirstLevel()
    {
        SceneManager.LoadScene("TempleRun");
    }

    public void PlayGameRunSecondLevel()
    {
        SceneManager.LoadScene("New Scene");
    }


    public void QuitGame()
    {
        Application.Quit();
    }


}
