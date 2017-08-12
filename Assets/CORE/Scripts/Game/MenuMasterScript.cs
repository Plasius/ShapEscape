using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuMasterScript : MonoBehaviour {
	string scene;
    Toggle t;


	void OnGUI(){
        t = GameObject.Find("MusicToggle").GetComponent<Toggle>();

        if (PlayerPrefs.GetInt("Music", 1) != 1)
        {
            //we dont need music
            t.isOn = false;
            AudioListener.volume = 0;
        }
        else {

        }

        GetComponent<Animator> ().SetTrigger ("LoggedIn");
	}
    

    public void ChangeScene(string s){
		GetComponent<Animator> ().SetTrigger ("Exiting");
		scene = s;
		StartCoroutine ("MyWaiter");

	}


	IEnumerator MyWaiter(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (scene);

	}

    public void exitGame() {
        Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    public void MusicToggle() {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Music", 0);
        }
        else {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Music", 1);
        }
    }


}
