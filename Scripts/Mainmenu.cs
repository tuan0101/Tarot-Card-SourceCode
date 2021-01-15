using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Mainmenu : MonoBehaviour
{
    public void PlayStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void PlayMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlayHistory()
    {
        SceneManager.LoadScene("History");
    }

    public void PlayResultScene()
    {
        SceneManager.LoadScene("ShowResult");
    }

    public void PlayAR()
    {
        SceneManager.LoadScene("ShowResult AR");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
