using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class casualScript : MonoBehaviour {
	public string[] shapeNames= new string[1];


	void Start () {
		RestoreHighScore();
		string color= PlayerPrefs.GetString("ShapeColor", "cyan");
		if (color == "random") {
			int size = shapeNames.Length;
			size--;
			string colorw = shapeNames [Random.Range(1, size)].ToLower ();
			GameObject.Find ("Circle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + colorw + "/circle_" + colorw);
			colorw = shapeNames [Random.Range(1, size)].ToLower ();
			GameObject.Find ("Triangle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + colorw + "/triangle_" + colorw);
			colorw = shapeNames [Random.Range(1, size)].ToLower ();
			GameObject.Find ("Square").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + colorw + "/square_" + colorw);

		} else {
			GameObject.Find ("Square").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+color+"/square_"+ color);
			GameObject.Find ("Triangle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+color+"/triangle_"+ color);
			GameObject.Find ("Circle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+color+"/circle_"+ color);
		
		}
		GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite>("Sprites/BGs/bg"+PlayerPrefs.GetString("BGColor","purple"));

	}

	void RestoreHighScore(){
		GameObject.Find("ScoreText").GetComponent<Text>().text="Best: "+ PlayerPrefs.GetFloat (PlayerPrefs.GetString("GameMode")+"Score");

	}

	public void ReturnToMenu(){
		SceneManager.LoadScene ("GameModeScene");

	}

}
