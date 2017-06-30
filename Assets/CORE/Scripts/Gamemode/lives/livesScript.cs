using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class livesScript : MonoBehaviour {


	void Start () {
		RestoreScore ();
		string shapeColor= PlayerPrefs.GetString("ShapeColor", "lime");
		string bgColor=PlayerPrefs.GetString("BGColor", "blue");
		string puckColor= PlayerPrefs.GetString("PuckColor", "neon");


		GameObject.Find ("Puck").GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite> ("Sprites/Pucks/puck_"+ puckColor);

		GameObject.Find ("Square").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+shapeColor+"/square_"+ shapeColor);
		GameObject.Find ("Triangle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+shapeColor+"/triangle_"+ shapeColor);
		GameObject.Find ("Circle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+shapeColor+"/circle_"+ shapeColor);

		GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite>("Sprites/BGs/bg"+bgColor);
	}

	void RestoreScore(){
		GameObject.Find("ScoreText").GetComponent<Text>().text="Best: "+ PlayerPrefs.GetFloat (PlayerPrefs.GetString("GameMode")+"Score");

	}
		
	public void ReturnToMenu(){
		SceneManager.LoadScene ("GameModeScene");

	}

}
