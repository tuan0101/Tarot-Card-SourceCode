using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingCards : MonoBehaviour
{
    public static List<int> indexList = null;

    public void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        this.gameObject.GetComponent<WaitingCards>();
        indexList = new List<int>();
    }

    public void setList(int value)
    {
        indexList.Add(value);
    }

}
