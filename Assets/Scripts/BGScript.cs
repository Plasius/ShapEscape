using UnityEngine;
using System.Collections;

public class BGScript : MonoBehaviour {
	public Sprite[] sprites= new Sprite[1];
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = sprites [Random.Range(0, sprites.Length)];
	}

	// Update is called once per frame
	void Update () {

	}
}
