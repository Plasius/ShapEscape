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
    string gamemode;
    public casualScript script;
    public livesScript script2;


    void Start () {
		timerText = GetComponent<Text> ();
        gamemode = PlayerPrefs.GetString("GameMode");
    }

	public static void StartTimer(){
		started = true;

	}

	public void finish(){
		started = false;
        switch (gamemode) {
            case "casual":
                casualScript.instance.dealWithSession(cTime);
                break;
            case "lives":
                livesScript.instance.dealWithSession(cTime);
                break;
        }
        
	}
		
		
	void Update () {
		if (started) {
			cTime += Time.deltaTime;
			timerText.text = cTime.ToString ("f2");
		}
	}
		
}
