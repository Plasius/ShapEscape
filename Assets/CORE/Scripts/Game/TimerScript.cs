using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour {

	public float cTime;
	private Text timerText;
	static bool started=false;
	string gamemode;

	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text> ();
		started = false;
		gamemode= PlayerPrefs.GetString("GameMode");
	}

	public static void StartTimer(){
		started = true;
	}

	public void finish(){
		started = false;

		if(PlayerPrefs.GetFloat(gamemode+"Score") < float.Parse(cTime.ToString("f2")))
			PlayerPrefs.SetFloat (gamemode+"Score", float.Parse(cTime.ToString("f2")));

		IEnumerator wow = waow();
		StartCoroutine (wow);

	}

	public IEnumerator waow(){
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene (gamemode+"Scene");
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			cTime += Time.deltaTime;
			timerText.text = cTime.ToString ("f2");
		}
	}


}
