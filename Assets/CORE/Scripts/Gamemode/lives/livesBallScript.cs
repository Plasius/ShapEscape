using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class livesBallScript : MonoBehaviour {
	int lives=3;
	float x;
	float y;
	bool started;


	void Start(){
		float height = Camera.main.orthographicSize * 2;
		transform.localScale = Vector3.one * height / 6f;
		GameObject.Find ("LivesText").GetComponent<Text> ().text = "Lives:  "+lives;

	}

	void Update(){
		x = Input.mousePosition.x;
		y = Input.mousePosition.y;
		if ( started && !Input.GetKey (KeyCode.Mouse0)) {
			lives = 0;
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
			GameObject.Find ("Panel").transform.localScale = new Vector3(0, 0, 0);
			GameObject.Find ("Button").transform.localScale = new Vector3 (0,0,0);

		}
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,10.0f));

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		lives--;
		if (lives >= 0) {
			GameObject.Find ("LivesText").GetComponent<Text> ().text = "Lives:  " + lives;
			GetComponent<CircleCollider2D> ().enabled = false;
			InvokeRepeating ("FlashGO", 0, 0.2f);
			StartCoroutine ("MyWaiter");
			return;
		} else {
			GameObject.Find ("LivesText").GetComponent<Text> ().text = "Lives:  "+0;

		}
		Destroy (this.gameObject);
		GameObject.Find ("Panel").transform.localScale = new Vector3(1, 1, 1);
		GameObject.Find ("Button").transform.localScale = new Vector3 (1,1,1);
		GameObject.Find ("Text").GetComponent<TimerScript> ().finish ();

	}

	void FlashGO(){
		if(GetComponent<SpriteRenderer> ().enabled)
			GetComponent<SpriteRenderer> ().enabled = false;
		else
			GetComponent<SpriteRenderer> ().enabled = true;
		
	}

	IEnumerator MyWaiter(){
		yield return new WaitForSeconds (1); 
		CancelInvoke ("FlashGO");
		GetComponent<CircleCollider2D> ().enabled = true;

	}

}
