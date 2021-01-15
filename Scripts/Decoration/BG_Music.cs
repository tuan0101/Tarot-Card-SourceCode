using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Music : MonoBehaviour
{
    private static GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);

    }

}
