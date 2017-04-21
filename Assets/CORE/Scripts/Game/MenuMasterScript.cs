using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuMasterScript : MonoBehaviour {
	string scene;

	void OnGUI(){


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








}
