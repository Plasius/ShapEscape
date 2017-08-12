using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class casualBallScript : MonoBehaviour {
	float x;
	float y;
	bool started;


	void Start(){
		float height = Camera.main.orthographicSize * 2 * Screen.width / Screen.height;
		transform.localScale = new Vector3(height / 3.0f, height / 3.0f, height / 3.0f);

	}

	void Update(){
		x = Input.mousePosition.x;
		y = Input.mousePosition.y;
		if ( started && !Input.GetKey (KeyCode.Mouse0)) {
			OnCollisionEnter2D (new Collision2D());

		}
        

    }

	void OnMouseDrag(){
		if (!started) {
			started = true;
			TimerScript.StartTimer();
			GameObject[] go = GameObject.FindGameObjectsWithTag ("Shape");
			foreach(GameObject g in go){
				g.GetComponent<ShapeScript> ().Shoot ();

			}
			GameObject.Find ("StartPanel").transform.localScale = new Vector3(0, 0, 0);

		}
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,10.0f));

	}

	void OnCollisionEnter2D (Collision2D col){
        Destroy(this.gameObject);
        GameObject.Find("Text").GetComponent<TimerScript>().finish();
		GameObject.Find ("Text").transform.localScale = new Vector3 (0,0,0);
        GameObject.Find("RecentText").GetComponent<Text>().text ="Recent: " + float.Parse(GameObject.Find("Text").GetComponent<TimerScript>().cTime.ToString("f2"));
        GameObject.Find("BestText").GetComponent<Text>().text = "Best: " + PlayerPrefs.GetFloat(PlayerPrefs.GetString("GameMode") + "Score");
        GameObject.Find("EndPanel").transform.localScale = new Vector3(1, 1, 1);

    }
		
}
