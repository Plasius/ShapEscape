using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuScript : MonoBehaviour {
	int currentModeIndex=0;
	int currentShapeIndex=0;
	int currentBGIndex=0;
	bool waiting=false;
	bool suc=false;


	public string[] gameModeNames= new string[1];
	public string[] shapeNames= new string[1];
	public string[] bgNames= new string[1];



	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate ();




		GameObject.Find ("Background").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg"+bgNames[currentBGIndex].ToLower());	

		GameObject.Find ("GameModeText").GetComponent<Text> ().text = gameModeNames [currentModeIndex];

		GameObject.Find ("BGNameText").GetComponent<Text> ().text = "Background: "+bgNames [currentBGIndex];

		GameObject.Find ("ShapeNameText").GetComponent<Text> ().text = shapeNames [currentShapeIndex];
		SetShapesToCurrentIndex ();



		//shoot
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Shape");
		foreach(GameObject g in go){
			g.GetComponent<ShapeScript> ().Shoot ();
		}
	}

	void Update(){

		if (waiting==false && suc==false) {
			waiting = true;
			// authenticate user:
			Social.localUser.Authenticate ((bool success) => {
				// handle success or failure
				waiting=false;
				suc=success;
			});

		}
	
	}



	public void ChangeScene(){
		PlayerPrefs.SetString ("BGColor", bgNames[currentBGIndex].ToLower());
		PlayerPrefs.SetString ("ShapeColor", shapeNames[currentShapeIndex].ToLower());
		PlayerPrefs.SetString ("GameMode", gameModeNames[currentModeIndex]);

		StartCoroutine ("wow");

	}


	IEnumerator wow(){
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene (gameModeNames[currentModeIndex].ToLower()+"Scene");
	}


	//BACKGROUND
	public void GoBGRight(){
		currentBGIndex++;
		if (currentBGIndex == bgNames.Length)
			currentBGIndex = 0;

		GameObject.Find ("BGNameText").GetComponent<Text> ().text ="Background: "+ bgNames [currentBGIndex];
		GameObject.Find ("Background").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg"+bgNames[currentBGIndex].ToLower());

	}

	public void GoBGLeft(){
		currentBGIndex--;
		if (currentBGIndex <0)
			currentBGIndex = bgNames.Length-1;

		GameObject.Find ("BGNameText").GetComponent<Text> ().text ="Background: "+ bgNames [currentBGIndex];
		GameObject.Find ("Background").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg"+bgNames[currentBGIndex].ToLower());

	}

	//MODE
	public void GoModeRight(){
		currentModeIndex++;
		if (currentModeIndex == gameModeNames.Length)
			currentModeIndex = 0;

		GameObject.Find ("GameModeText").GetComponent<Text> ().text = gameModeNames [currentModeIndex];
	}

	public void GoModeLeft(){
		currentModeIndex--;
		if (currentModeIndex < 0)
			currentModeIndex = gameModeNames.Length - 1;

		GameObject.Find ("GameModeText").GetComponent<Text> ().text = gameModeNames [currentModeIndex];
	}




	//SHAPES
	public void GoShapesRight(){
		currentShapeIndex++;
		if (currentShapeIndex == shapeNames.Length)
			currentShapeIndex = 0;

		GameObject.Find ("ShapeNameText").GetComponent<Text> ().text = shapeNames [currentShapeIndex];
		SetShapesToCurrentIndex ();

	}

	public void GoShapesLeft(){
		currentShapeIndex--;
		if (currentShapeIndex < 0)
			currentShapeIndex = shapeNames.Length - 1;

		GameObject.Find ("ShapeNameText").GetComponent<Text> ().text = shapeNames [currentShapeIndex];
		SetShapesToCurrentIndex ();
	}


	void SetShapesToCurrentIndex(){
		//GameObject.Find ("Circle").GetComponent<SpriteRenderer>().sprite= circleSprites[currentShapeIndex];
		if (shapeNames [currentShapeIndex] == "RANDOM") {
			int size = shapeNames.Length;
			size--;
			string color = shapeNames [Random.Range(1, size)].ToLower ();
			GameObject.Find ("Circle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + color + "/circle_" + color);
			color = shapeNames [Random.Range(1, size)].ToLower ();
			GameObject.Find ("Triangle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + color + "/triangle_" + color);
			color = shapeNames [Random.Range(1, size)].ToLower ();
			GameObject.Find ("Square").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + color + "/square_" + color);

			return;
		}
		GameObject.Find ("Circle").GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite>("Sprites/Shapes/"+shapeNames[currentShapeIndex].ToLower()+"/circle_"+ shapeNames[currentShapeIndex].ToLower());
		GameObject.Find ("Square").GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite>("Sprites/Shapes/"+shapeNames[currentShapeIndex].ToLower()+"/square_"+ shapeNames[currentShapeIndex].ToLower());
		GameObject.Find ("Triangle").GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite>("Sprites/Shapes/"+shapeNames[currentShapeIndex].ToLower()+"/triangle_"+ shapeNames[currentShapeIndex].ToLower());


		GameObject.Find ("ShapeNameText").GetComponent<Text>().text= "Shapes: "+shapeNames[currentShapeIndex];

	}
}
