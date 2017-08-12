using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            goBack();

    }

    public void openTumblr() {
        Application.OpenURL("https://plotberrystudios.tumblr.com/");
    }

    public void openStore() {
        Application.OpenURL("http://play.google.com/store/apps/details?id=com.plotberry.shapescape");
    }

    public void goBack() {
        SceneManager.LoadScene("MenuScene");
    }
}
