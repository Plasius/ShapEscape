using UnityEngine;
using System.Collections;

public class ResizeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		float height = Camera.main.orthographicSize * 2;
		transform.localScale = Vector3.one * height / 6f*2;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
