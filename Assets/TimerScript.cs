using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public float cTime;
	private Text timerText;
	static bool started=false;

	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text> ();
		started = false;
	}

	public static void StartTimer(){
		started = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			cTime += Time.deltaTime;
			timerText.text = cTime.ToString ("f2");
		}
	}
}
