using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    Animator Fadetransition;
    [SerializeField]
    Animator Slidetransition;

    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel("ShowResult AR"));
    }

    IEnumerator LoadLevel(string name)
    {
        yield return new WaitForSeconds(transitionTime);
        Fadetransition.SetTrigger("Start");
        Slidetransition.SetTrigger("Start");
        //Fadetransition.SetTrigger("KeepGoing");
        //Slidetransition.SetTrigger("KeepGoing");
        yield return new WaitForSeconds(1f);
          
        SceneManager.LoadScene(name);
    }
}
