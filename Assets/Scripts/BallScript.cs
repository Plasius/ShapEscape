using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour {

	float x;
	float y;
	bool started;

	void start(){
		float height = Camera.main.orthographicSize * 2;
		transform.localScale = Vector3.one * height / 6f;

	}

	// Update is called once per frame
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
				g.GetComponent<ShapeScript> ().shoot ();
			}
			GameObject.Find ("Panel").transform.localScale = new Vector3(0, 0, 0);
			GameObject.Find ("Button").transform.localScale = new Vector3 (0,0,0);

		}


		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,10.0f));

	}
		


	void OnCollisionEnter2D (Collision2D col)
	{
		Destroy (this.gameObject);
		GameObject.Find ("Panel").transform.localScale = new Vector3(1, 1, 1);
		GameObject.Find ("Button").transform.localScale = new Vector3 (1,1,1);
		GameObject.Find ("Text").GetComponent<TimerScript> ().finish ();

	}


}
