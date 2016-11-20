using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
	int currentIndex=0;

	public string[] gameModeNames= new string[1];
	public Sprite[] circleSprites= new Sprite[1];
	public Sprite[] triangleSprites= new Sprite[1];
	public Sprite[] squareSprites= new Sprite[1];
	public Sprite[] backgroundSprites= new Sprite[1];


	// Use this for initialization
	void Start () {
		SetColorToCurrentIndex ();
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Shape");
		foreach(GameObject g in go){
			g.GetComponent<ShapeScript> ().shoot ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeScene(){
		SceneManager.LoadScene ("GameScene");

	}

	public void GoRight(){
		currentIndex++;
		if (currentIndex == circleSprites.Length)
			currentIndex = 0;
		SetColorToCurrentIndex ();
	}

	public void GoLeft(){
		currentIndex--;
		if (currentIndex < 0)
			currentIndex = circleSprites.Length - 1;
		SetColorToCurrentIndex ();
	}

	void SetColorToCurrentIndex(){
		GameObject.Find ("GameModeText").GetComponent<Text> ().text = gameModeNames [currentIndex];
		GameObject.Find ("Circle").GetComponent<SpriteRenderer>().sprite= circleSprites[currentIndex];
		GameObject.Find ("Square").GetComponent<SpriteRenderer>().sprite= squareSprites[currentIndex];
		GameObject.Find ("Triangle").GetComponent<SpriteRenderer>().sprite= triangleSprites[currentIndex];
		GameObject.Find ("Background").GetComponent<SpriteRenderer>().sprite= backgroundSprites[currentIndex];

	}

}
