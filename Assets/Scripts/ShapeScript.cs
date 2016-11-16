using UnityEngine;
using System.Collections;

public class ShapeScript : MonoBehaviour {
	
	Rigidbody2D rb2D;
	public  float maximumSpeed;
	public Sprite[] sprites= new Sprite[1];

	// Use this for initialization
	void Start () {
		float height = Camera.main.orthographicSize * 2;
		transform.localScale = Vector3.one * height / 6f;
		rb2D = GetComponent<Rigidbody2D> ();



		GetComponent<SpriteRenderer> ().sprite = sprites [Random.Range(0, sprites.Length)];
	}

	public void shoot(){
		rb2D.AddForce (new Vector2(Random.Range(-maximumSpeed, maximumSpeed),Random.Range(-maximumSpeed, maximumSpeed )));
	}

	// Update is called once per frame
	void Update () {
		rb2D.velocity = rb2D.velocity.normalized * maximumSpeed;


		/*
		float speed = Vector3.Magnitude (rb2D.velocity);  // test current object speed
		if (speed > maximumSpeed)

		{
			float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease

			Vector3 normalisedVelocity = rb2D.velocity.normalized;
			Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

			rb2D.AddForce(-brakeVelocity);  // apply opposing brake force
		}*/




	}
}
