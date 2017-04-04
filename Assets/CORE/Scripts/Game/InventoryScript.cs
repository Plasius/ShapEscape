using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
 
[Serializable]
class PlayerData{
	public string[] shapeList;
	public string[] puckList;
	public string[] bgList;

	public PlayerData(string[] puck, string[]  shape, string[]  bg){
		shapeList=shape;
		puckList=puck;
		bgList=bg;
	}
}

public class InventoryScript : MonoBehaviour {
	static PlayerData data=null;
	GridLayoutGroup grid;
	public GameObject button;

	void Start () {
		grid = GetComponent<GridLayoutGroup> ();
		restoreSave ();

	}

	public void GoBack(){
		SceneManager.LoadScene ("MenuScene");

	}

	void restoreSave(){
		if (!File.Exists (Application.persistentDataPath + "/playerinfo.dat")) {
			data = new PlayerData (new string[]  {"Puck"}, new string[]  {"cyan", "lemon", "lime", "Xmagenta", "Yvulkan" }, new string[]   {"black", "Xblue", "Ypurple"});
			return;
		}


		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open(Application.persistentDataPath+ "/playerinfo.dat", FileMode.Open);
		data =(PlayerData) bf.Deserialize (file);
		file.Close ();

	}


	void saveChanges(){

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+ "/playerinfo.dat");
		bf.Serialize (file, data);
		file.Close ();
	
		restoreSave ();

	}



	public void PuckLoader(){
		grid.transform.DetachChildren ();
		for(int i=0; i< data.puckList.Length; i++){
			string color = data.puckList [i];

			GameObject b = Instantiate (button) as GameObject;
			b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Pucks/"+color);
			//b.GetComponent<Button> ().onClick.AddListener (() => {});
			b.transform.SetParent (grid.transform, false);

		}

	}

	public void ShapeLoader(){
		grid.transform.DetachChildren ();

		for(int i=0; i< data.shapeList.Length; i++){

			GameObject b = Instantiate (button) as GameObject;
			b.GetComponent<Button> ().name = data.shapeList [i];

			if (data.shapeList[i].StartsWith ("X") || data.shapeList[i].StartsWith ("Y")) {
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Menu/currency");

				b.GetComponent<Button>().onClick.AddListener (() => {
					
					for(int j=0; j<data.shapeList.Length; j++){
						if(data.shapeList[j]==b.name){
							data.shapeList[j]=data.shapeList[j].Substring(1);
						}
					}
					PlayerPrefs.SetString ("ShapeColor", this.name.Substring(1));
					b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + b.name.Substring(1) + "/circle_" + b.name.Substring(1));
					saveChanges();
				});

			} else {
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + data.shapeList[i] + "/circle_" + data.shapeList[i]);
				b.GetComponent<Button> ().onClick.AddListener (() => {
					PlayerPrefs.SetString ("ShapeColor", b.name);

				});
			}

			b.transform.SetParent (grid.transform, false);

		}

	}

	public void BGLoader(){
		grid.transform.DetachChildren ();
		for(int i=0; i< data.bgList.Length; i++){
			GameObject b = Instantiate (button) as GameObject;
			b.GetComponent<Button>().name= data.bgList[i];

			if (data.bgList[i].StartsWith ("X") || data.bgList[i].StartsWith ("Y")) {
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Menu/currency");

				b.GetComponent<Button> ().onClick.AddListener (() => {
					for(int j=0; j<data.bgList.Length; j++){
						if(data.bgList[j]==b.name){
							data.bgList[j]=data.bgList[j].Substring(1);
						}
					}

					PlayerPrefs.SetString ("BGColor", b.name.Substring(1));
					b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg" + b.name.Substring(1));
					Debug.Log("showing: "+ b.name.Substring(1));
					saveChanges();
				});

			}else {
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg" + data.bgList[i]);
				b.GetComponent<Button> ().onClick.AddListener (() => {
					PlayerPrefs.SetString ("BGColor", b.name);
				});
			}

			b.transform.SetParent (grid.transform, false);
		}

	}

}
