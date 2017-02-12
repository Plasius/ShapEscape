using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuMasterScript : MonoBehaviour {
	string scene;
	bool suc=false;

	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate ();
	}

	void Update(){
		if (!suc) {
			Social.localUser.Authenticate ((bool success) => {
				// handle success or failure
				if(success){
					if(Social.localUser.authenticated){
						GetComponent<Animator> ().SetTrigger ("LoggedIn");
						suc=true;
					}
				}
			});
		
		}

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
