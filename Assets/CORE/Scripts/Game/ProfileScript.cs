using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

using UnityEngine.SceneManagement;

public class ProfileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void showAchievementsUI(){
		Social.ShowAchievementsUI ();
		
	}

	public void goBack(){
		Debug.Log ("returning");
		SceneManager.LoadScene ("MenuScene");
	}



}
