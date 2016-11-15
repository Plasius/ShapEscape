using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour {

	float x;
	float y;
	bool started=false;

	// Update is called once per frame
	void Update(){
		x = Input.mousePosition.x;
		y = Input.mousePosition.y;
	}

	void OnMouseDrag(){
		if (!started)
			started = true;
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,10.0f));
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		Destroy (this.gameObject);
		SceneManager.LoadScene ("GameScene");

	}


	void LateUpdate(){
		if(started)
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,10.0f));
	}

}
