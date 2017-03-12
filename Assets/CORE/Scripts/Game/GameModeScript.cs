using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameModeScript : MonoBehaviour {
	string scene;

	void OnGUI(){
		GetComponent<Animator> ().SetTrigger ("LoggedIn");
		
	}

	public void ChangeScene(string scenePrefix){
		GetComponent<Animator> ().SetTrigger ("Exiting");
		scene = scenePrefix;
		PlayerPrefs.SetString ("GameMode", scenePrefix);
		StartCoroutine ("MyWaiter");

	}

	IEnumerator MyWaiter(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (scene+"Scene");

	}

}
