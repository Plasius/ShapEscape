﻿using UnityEngine;
using System.Collections;

public class ShapeScript : MonoBehaviour {
	Rigidbody2D rb2D;
	public  float maximumSpeed;
	public Sprite[] sprites= new Sprite[1];


	void Start () {
		float height = Camera.main.orthographicSize * 2 * Screen.width / Screen.height;
		transform.localScale = new Vector3(height / 3.0f, height / 3.0f, height / 3.0f);
		rb2D = GetComponent<Rigidbody2D> ();

	}

	public void Shoot(){
		rb2D = GetComponent<Rigidbody2D> ();
		rb2D.AddForce (new Vector2(Random.Range(-maximumSpeed, maximumSpeed),Random.Range(-maximumSpeed, maximumSpeed )));

	}

	void Update () {
		rb2D.velocity = rb2D.velocity.normalized * maximumSpeed;

	}

}
