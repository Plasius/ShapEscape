using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameModeScript : MonoBehaviour {
	string scene;
	// Use this for initialization
	void Start () {
	}

	void OnGUI(){
		GetComponent<Animator> ().SetTrigger ("LoggedIn");
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ChangeScene(string s){
		GetComponent<Animator> ().SetTrigger ("Exiting");
		scene = s;

		PlayerPrefs.SetString ("BGColor", "purple");
		PlayerPrefs.SetString ("ShapeColor", "cyan");
		PlayerPrefs.SetString ("GameMode", s);

		StartCoroutine ("MyWaiter");
	}

	IEnumerator MyWaiter(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (scene+"Scene");
	}

}
