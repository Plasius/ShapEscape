using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class WindowScript : MonoBehaviour {
	public GameObject loadingPanel;

	public GameObject squareImage;
	public GameObject triangleImage;
	public GameObject puckImage;
	public GameObject bgImage;
	public GameObject currencyImage;

	public GameObject titleText;
	public GameObject costText;
	public GameObject buyEquipText;

	public GameObject buyEquipButton;

	public InventoryScript script;


	public void openWindow(string cause, string name){
		buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
		buyEquipButton.GetComponent<Button> ().enabled = true;
		loadingPanel.transform.localScale = new Vector3 (1,1,1);
		this.transform.localScale = new Vector3 (1,1,1);

		switch(cause){
		case "puck":
			squareImage.transform.localScale = new Vector3 (0, 0, 0);
			triangleImage.transform.localScale = new Vector3 (0, 0, 0);
			currencyImage.transform.localScale = new Vector3 (0, 0, 0);
			costText.transform.localScale = new Vector3 (0, 0, 0);
			bgImage.transform.localScale = new Vector3 (0, 0, 0);
			puckImage.transform.localScale = new Vector3 (1, 1, 1);

			titleText.GetComponent<Text> ().text = name.ToUpper ();
			puckImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Pucks/puck_" + name);

			if (PlayerPrefs.GetString ("PuckColor", "").Equals(name)) {
				buyEquipText.GetComponent<Text> ().text="EQUIPPED";
				buyEquipButton.GetComponent<Button> ().enabled = false;
				
			} else {
				buyEquipText.GetComponent<Text> ().text="EQUIP";
				//add listener
				buyEquipButton.GetComponent<Button>().onClick.AddListener (() => {
					PlayerPrefs.SetString("PuckColor", name);
					script.PuckLoader();
					buyEquipText.GetComponent<Text> ().text="EQUIPPED";
					buyEquipButton.GetComponent<Button> ().enabled = false;
					buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
				});

			}
			break;

		case "shape":
			squareImage.transform.localScale = new Vector3 (1, 1, 1);
			triangleImage.transform.localScale = new Vector3 (1, 1, 1);
			currencyImage.transform.localScale = new Vector3 (0, 0, 0);
			costText.transform.localScale = new Vector3 (0, 0, 0);
			bgImage.transform.localScale = new Vector3 (0,0,0);
			puckImage.transform.localScale = new Vector3 (1, 1, 1);

			titleText.GetComponent<Text> ().text = name.ToUpper ();
			puckImage.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Sprites/Shapes/"+name+"/circle_"+name);
			squareImage.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Sprites/Shapes/"+name+"/square_"+name);
			triangleImage.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Sprites/Shapes/"+name+"/triangle_"+name);

			if (PlayerPrefs.GetString ("ShapeColor", "").Equals( name)) {
				buyEquipText.GetComponent<Text> ().text="EQUIPPED";
				buyEquipButton.GetComponent<Button> ().enabled = false;

			} else {
				buyEquipText.GetComponent<Text> ().text="EQUIP";
				//add listener
				buyEquipButton.GetComponent<Button>().onClick.AddListener (() => {
					PlayerPrefs.SetString("ShapeColor", name);
					script.ShapeLoader();
					buyEquipText.GetComponent<Text> ().text="EQUIPPED";
					buyEquipButton.GetComponent<Button> ().enabled = false;
					buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
				});

			}

			break;
		case "bg":
			squareImage.transform.localScale = new Vector3 (0, 0, 0);
			triangleImage.transform.localScale = new Vector3 (0, 0, 0);
			currencyImage.transform.localScale = new Vector3 (0, 0, 0);
			costText.transform.localScale = new Vector3 (0, 0, 0);
			bgImage.transform.localScale = new Vector3 (1,1,1);
			puckImage.transform.localScale = new Vector3 (0, 0, 0);

			titleText.GetComponent<Text> ().text = name.ToUpper ();
			bgImage.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Sprites/BGs/bg"+name);

			if (PlayerPrefs.GetString ("BGColor", "").Equals(name)) {
				buyEquipText.GetComponent<Text> ().text="EQUIPPED";
				buyEquipButton.GetComponent<Button> ().enabled = false;

			} else {
				buyEquipText.GetComponent<Text> ().text="EQUIP";
				//add listener
				buyEquipButton.GetComponent<Button>().onClick.AddListener (() => {
					PlayerPrefs.SetString("BGColor", name);
					script.BGLoader();
					buyEquipText.GetComponent<Text> ().text="EQUIPPED";
					buyEquipButton.GetComponent<Button> ().enabled = false;
					buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
				});

			}

			break;
		}



	}







	public void openWindow(string cause, string name, int price){
		buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
		buyEquipButton.GetComponent<Button> ().enabled = true;
		loadingPanel.transform.localScale = new Vector3 (1,1,1);
		this.transform.localScale = new Vector3 (1,1,1);

		switch(cause){
		case "puck":
			squareImage.transform.localScale = new Vector3 (0, 0, 0);
			triangleImage.transform.localScale = new Vector3 (0, 0, 0);
			currencyImage.transform.localScale = new Vector3 (1, 1, 1);
			costText.transform.localScale = new Vector3 (1, 1, 1);
			bgImage.transform.localScale = new Vector3 (0, 0, 0);
			puckImage.transform.localScale = new Vector3 (1, 1, 1);

			titleText.GetComponent<Text> ().text = name.ToUpper ();
			puckImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Pucks/puck_" + name);
			costText.GetComponent<Text> ().text = price.ToString ();




			if (price <= InventoryScript.d.money) {
				buyEquipText.GetComponent<Text> ().text = "BUY";

				buyEquipButton.GetComponent<Button> ().onClick.AddListener (() => {
					buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
					InventoryScript.d.money -= price;

					Array.Resize (ref InventoryScript.d.puckList, InventoryScript.d.puckList.Length + 1);
					InventoryScript.d.puckList [InventoryScript.d.puckList.Length - 1] = name;

					buyEquipText.GetComponent<Text> ().text = "EQUIP";
					//add listener
					buyEquipButton.GetComponent<Button> ().onClick.AddListener (() => {
						buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
						PlayerPrefs.SetString ("PuckColor", name);
						script.PuckLoader();
						buyEquipText.GetComponent<Text> ().text = "EQUIPPED";
						buyEquipButton.GetComponent<Button> ().enabled = false;
					});

					script.SaveLocal ();
					script.PuckLoader ();
					script.SaveCloud ();

				});

			} else {
					buyEquipText.GetComponent<Text> ().text="NO MONEY";
					buyEquipButton.GetComponent<Button> ().enabled = false;
			}


			break;

		case "shape":
			squareImage.transform.localScale = new Vector3 (1, 1, 1);
			triangleImage.transform.localScale = new Vector3 (1, 1, 1);
			currencyImage.transform.localScale = new Vector3 (1, 1, 1);
			costText.transform.localScale = new Vector3 (1, 1, 1);
			bgImage.transform.localScale = new Vector3 (0,0,0);
			puckImage.transform.localScale = new Vector3 (1, 1, 1);

			titleText.GetComponent<Text> ().text = name.ToUpper ();
			puckImage.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Sprites/Shapes/"+name+"/circle_"+name);
			squareImage.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Sprites/Shapes/"+name+"/square_"+name);
			triangleImage.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Sprites/Shapes/"+name+"/triangle_"+name);
			costText.GetComponent<Text> ().text = price.ToString ();

			if (price <= InventoryScript.d.money) {
				buyEquipText.GetComponent<Text> ().text = "BUY";

				buyEquipButton.GetComponent<Button> ().onClick.AddListener (() => {
					buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
					InventoryScript.d.money -= price;

					Array.Resize (ref InventoryScript.d.shapeList, InventoryScript.d.shapeList.Length + 1);
					InventoryScript.d.shapeList [InventoryScript.d.shapeList.Length - 1] = name;

					buyEquipText.GetComponent<Text> ().text = "EQUIP";
					//add listener
					buyEquipButton.GetComponent<Button> ().onClick.AddListener (() => {
						buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
						PlayerPrefs.SetString ("ShapeColor", name);
						script.ShapeLoader();
						buyEquipText.GetComponent<Text> ().text = "EQUIPPED";
						buyEquipButton.GetComponent<Button> ().enabled = false;
					});

					script.SaveLocal ();
					script.ShapeLoader ();
					script.SaveCloud ();

				});

			} else {
				buyEquipText.GetComponent<Text> ().text="NO MONEY";
				buyEquipButton.GetComponent<Button> ().enabled = false;
			}

			break;
		case "bg":
			squareImage.transform.localScale = new Vector3 (0, 0, 0);
			triangleImage.transform.localScale = new Vector3 (0, 0, 0);
			currencyImage.transform.localScale = new Vector3 (1, 1, 1);
			costText.transform.localScale = new Vector3 (1, 1, 1);
			bgImage.transform.localScale = new Vector3 (1,1,1);
			puckImage.transform.localScale = new Vector3 (0, 0, 0);

			titleText.GetComponent<Text> ().text = name.ToUpper ();
			bgImage.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Sprites/BGs/bg"+name);
			costText.GetComponent<Text> ().text = price.ToString ();

			if (price <= InventoryScript.d.money) {
				buyEquipText.GetComponent<Text> ().text = "BUY";

				buyEquipButton.GetComponent<Button> ().onClick.AddListener (() => {
					buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
					InventoryScript.d.money -= price;

					Array.Resize (ref InventoryScript.d.bgList, InventoryScript.d.bgList.Length + 1);
					InventoryScript.d.bgList [InventoryScript.d.bgList.Length - 1] = name;

					buyEquipText.GetComponent<Text> ().text = "EQUIP";
					//add listener
					buyEquipButton.GetComponent<Button> ().onClick.AddListener (() => {
						buyEquipButton.GetComponent<Button>().onClick.RemoveAllListeners ();
						PlayerPrefs.SetString ("BGColor", name);
						script.BGLoader();
						buyEquipText.GetComponent<Text> ().text = "NO MONEY";
						buyEquipButton.GetComponent<Button> ().enabled = false;
					});

					script.SaveLocal ();
					script.BGLoader ();
					script.SaveCloud ();

				});

			} else {
				buyEquipText.GetComponent<Text> ().text="EQUIPPED";
				buyEquipButton.GetComponent<Button> ().enabled = false;
			}

			break;
		}
	}


	public void closeWindow(){
		loadingPanel.transform.localScale = new Vector3 (0,0,0);
		this.transform.localScale = new Vector3 (0,0,0);
	}
}
