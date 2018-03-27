using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStartScreen : MonoBehaviour {

    public static ShowStartScreen instance;

    public bool started = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
