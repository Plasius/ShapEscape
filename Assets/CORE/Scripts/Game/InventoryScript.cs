using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryScript : MonoBehaviour {
	string[] shapeList = {"cyan", "lemon", "lime", "magenta", "vulkan" };
	string[] puckList= {"Puck"};
	string[] bgList = {"black", "blue", "purple" };
	public GridLayoutGroup grid;
	public GameObject button;

	// Use this for initialization
	void Start () {
		grid = gameObject.GetComponent<GridLayoutGroup> ();
		ShapeLoader ();
	}

	public void GoBack(){
		SceneManager.LoadScene ("MenuScene");
	}

	// Update is called once per frame
	void Update () {
		
	}

	void clear(){
		grid.transform.DetachChildren ();

	}

	public void PuckLoader(){
		clear ();
		foreach (string color in puckList) {
			GameObject b = Instantiate (button) as GameObject;

			Transform child = b.transform.GetChild (0);
			child.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Pucks/"+color);

			//b.GetComponent<Button> ().onClick.AddListener (() => {});

			b.transform.SetParent (grid.transform, false);


		}
	}

	public void ShapeLoader(){
		clear ();
		foreach (string color in shapeList) {
			GameObject b = Instantiate (button) as GameObject;

			Transform child = b.transform.GetChild (0);
			child.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + color + "/circle_" + color);

			b.GetComponent<Button> ().onClick.AddListener (() => {
				PlayerPrefs.SetString ("ShapeColor", color);
			});

			b.transform.SetParent (grid.transform, false);
		}
	}

	public void BGLoader(){
		clear ();
		foreach (string color in bgList) {
			GameObject b = Instantiate (button) as GameObject;

			Transform child = b.transform.GetChild (0);
			child.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg" + color);

			b.GetComponent<Button> ().onClick.AddListener (() => {
				PlayerPrefs.SetString ("BGColor", color);
			});

			b.transform.SetParent (grid.transform, false);
		}
	}

}
