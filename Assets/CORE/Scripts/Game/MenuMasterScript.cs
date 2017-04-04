using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuMasterScript : MonoBehaviour {
	string scene;
	bool auting=false;

	void Start(){
		GooglePlayGames.PlayGamesPlatform.Activate ();
	}


	void OnGUI(){
		if(!auting)
		Social.localUser.Authenticate ((bool success) => {
				if (success)

					GetComponent<Animator> ().SetTrigger ("LoggedIn");
				else 
					auting=false;
				return;
			});
		auting = true;
	}
	
	public void ChangeScene(string s){
		GetComponent<Animator> ().SetTrigger ("Exiting");
		scene = s;
		StartCoroutine ("MyWaiter");

	}

	void Update(){
	}

	IEnumerator MyWaiter(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (scene);

	}


}
