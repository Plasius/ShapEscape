using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class TimerScript : MonoBehaviour {
	public float cTime;
	private Text timerText;
	static bool started=false;
	string ScoreBoardID=null;
	string gamemode;


	void Start () {
		timerText = GetComponent<Text> ();
		started = false;
		gamemode= PlayerPrefs.GetString("GameMode");
		SetScoreBoardID ();

	}

	public static void StartTimer(){
		started = true;

	}

	public void finish(){
		started = false;

		if(PlayerPrefs.GetFloat(gamemode+"Score") < float.Parse(cTime.ToString("f2")))
			PlayerPrefs.SetFloat (gamemode+"Score", float.Parse(cTime.ToString("f2")));


		StartCoroutine (poster());

		StartCoroutine (changer());

	}
		
	void SetScoreBoardID(){
		switch(gamemode){
		case "casual":
			ScoreBoardID="CgkI2caNgrAdEAIQBg";
			break;

		case "lives":
			ScoreBoardID= "CgkI2caNgrAdEAIQBw";
			break;

		}

	}

	public IEnumerator changer(){
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene (gamemode+"Scene");
	}

	public IEnumerator poster(){
		Social.ReportScore ((long)(cTime*100)+1, ScoreBoardID, (bool success) => {});
		yield return new WaitForSeconds(0);

	}


	public void ShowLeader(){
		PlayGamesPlatform.Instance.ShowLeaderboardUI (ScoreBoardID);

	}
		
	void Update () {
		if (started) {
			cTime += Time.deltaTime;
			timerText.text = cTime.ToString ("f2");
		}
	}
		
}
