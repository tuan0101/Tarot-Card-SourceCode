using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunText : MonoBehaviour
{

    public Text title;
    public Text quote;
    public GameObject textMode;
    public GameObject box;
    public void TurnOnAndOff()
    {
        if (textMode.activeSelf)
        {
            textMode.SetActive(false);
            box.SetActive(false);
        }
        else
        {
            textMode.SetActive(true);
            box.SetActive(true);
        }

    }
}
