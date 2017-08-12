using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAliveScript : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("Music", 1) != 1)
        {
            AudioListener.volume = 0;
        }
        else {
            AudioListener.volume = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
