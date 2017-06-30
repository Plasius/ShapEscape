using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;
 
[Serializable]
class PlayerData{
	public string[] shapeList;
	public string[] puckList;
	public string[] bgList;
	public int money;

	public PlayerData(string[] puck, string[]  shape, string[]  bg, int m){
		shapeList=shape;
		puckList=puck;
		bgList=bg;
		money = m;
	}
}

public class InventoryScript : MonoBehaviour {
	static PlayerData d=null;
	static string[] mainPuckList= new string[3] {"neon", "black", "bullet"};
	static string[] mainShapeList= new string[6] {"cyan", "lemon", "lime", "magenta", "vulkan", "black"};
	static string[] mainBGList= new string[4] {"black", "blue", "purple", "hills"};


	GridLayoutGroup grid;
	public GameObject button;

	void Start () {
		grid = GetComponent<GridLayoutGroup> ();
		restoreSave ();
		PuckLoader ();

	}

	public void GoBack(){
		SaveCloud ();
		SceneManager.LoadScene ("MenuScene");

	}

	void restoreSave(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open(Application.persistentDataPath+ "/playerinfo.dat", FileMode.Open);
		d =(PlayerData) bf.Deserialize (file);
		file.Close ();

	}















	public void PuckLoader(){
		grid.transform.DetachChildren ();
		for(int i=0; i< d.puckList.Length; i++){
			string color = d.puckList [i];

			GameObject b = Instantiate (button) as GameObject;
			b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Pucks/"+"puck_"+color);
			b.GetComponent<Button>().name = color;

			b.GetComponent<Button> ().onClick.AddListener (() => {
				PlayerPrefs.SetString ("PuckColor", b.GetComponent<Button>().name);
			});

			b.transform.SetParent (grid.transform, false);

		}

		for(int i=0; i<mainPuckList.Length; i++){
			int times=0;
			for (int j = 0; j < d.puckList.Length; j++) {
				if (mainPuckList [i] == d.puckList [j])
					times++;
			}

			if (times == 0) {
				//creating buyable puck
				string color=mainPuckList[i];

				GameObject b = Instantiate (button) as GameObject;
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Menu/currency");
				b.GetComponent<Button>().name = color;

				b.GetComponent<Button> ().onClick.AddListener (() => {
					if(d.money>10){
						PlayerPrefs.SetString ("PuckColor", b.GetComponent<Button>().name);
						d.money-=10;

						Array.Resize(ref d.puckList, d.puckList.Length + 1);
						d.puckList[d.puckList.Length - 1] = color;

						SaveLocal();
						PuckLoader();
						SaveCloud();
					}
				});

				b.transform.SetParent (grid.transform, false);
			}

		}

	}

	public void ShapeLoader(){
		grid.transform.DetachChildren ();
		for(int i=0; i< d.shapeList.Length; i++){
			string color = d.shapeList [i];

			GameObject b = Instantiate (button) as GameObject;
			b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+color+"/circle_"+color);
			b.GetComponent<Button>().name = color;

			b.GetComponent<Button> ().onClick.AddListener (() => {
				PlayerPrefs.SetString ("ShapeColor", b.GetComponent<Button>().name);
			});

			b.transform.SetParent (grid.transform, false);

		}

		for(int i=0; i<mainShapeList.Length; i++){
			int times=0;
			for (int j = 0; j < d.shapeList.Length; j++) {
				if (mainShapeList [i] == d.shapeList [j])
					times++;
			}

			if (times == 0) {
				//creating buyable puck
				string color=mainShapeList[i];

				GameObject b = Instantiate (button) as GameObject;
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Menu/currency");
				b.GetComponent<Button>().name = color;

				b.GetComponent<Button> ().onClick.AddListener (() => {
					if(d.money>10){
						PlayerPrefs.SetString ("ShapeColor", b.GetComponent<Button>().name);
						d.money-=10;

						Array.Resize(ref d.shapeList, d.shapeList.Length + 1);
						d.shapeList[d.shapeList.Length - 1] = color;

						SaveLocal();
						ShapeLoader();
						SaveCloud();
					}
				});

				b.transform.SetParent (grid.transform, false);
			}

		}
	}

	public void BGLoader(){
		grid.transform.DetachChildren ();
		for(int i=0; i< d.bgList.Length; i++){
			string color = d.bgList [i];

			GameObject b = Instantiate (button) as GameObject;
			b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg"+color);
			b.GetComponent<Button>().name = color;

			b.GetComponent<Button> ().onClick.AddListener (() => {
				PlayerPrefs.SetString ("BGColor", b.GetComponent<Button>().name);
			});

			b.transform.SetParent (grid.transform, false);

		}

		for(int i=0; i<mainBGList.Length; i++){
			int times=0;
			for (int j = 0; j < d.bgList.Length; j++) {
				if (mainBGList [i] == d.bgList [j])
					times++;
			}

			if (times == 0) {
				//creating buyable puck
				string color=mainBGList[i];

				GameObject b = Instantiate (button) as GameObject;
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Menu/currency");
				b.GetComponent<Button>().name = color;

				b.GetComponent<Button> ().onClick.AddListener (() => {
					if(d.money>10){
						PlayerPrefs.SetString ("BGColor", b.GetComponent<Button>().name);
						d.money-=10;

						Array.Resize(ref d.bgList, d.bgList.Length + 1);
						d.bgList[d.bgList.Length - 1] = color;

						SaveLocal();
						BGLoader();
						SaveCloud();
					}
				});

				b.transform.SetParent (grid.transform, false);
			}

		}

	}











	void SaveCloud(){
		OpenSavedGame ("ShapEscapeData1");
	}

	void OpenSavedGame(string filename) {
		Debug.Log ("opening saved data");
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(filename, GooglePlayGames.BasicApi.DataSource.ReadNetworkOnly,
			ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
	}

	public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game) {
		Debug.Log ("got status");
		if (status == SavedGameRequestStatus.Success) {
			// handle reading or writing of saved game.
				SaveGame (game,ObjectToByteArray(d));	
		} else {
			// handle error
			Debug.Log ("error2");
		}
	}

	void SaveGame (ISavedGameMetadata game, byte[] savedData) {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

		SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
		SavedGameMetadataUpdate updatedMetadata = builder.Build();
		savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
	}

	public void OnSavedGameWritten (SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
			// handle reading or writing of saved game.
			//LoadCloud(); WHY
			SaveLocal ();
		} else {
			// handle error
			Debug.Log ("error3");
		}
	}




	void SaveLocal(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+ "/playerinfo.dat");
		bf.Serialize (file, d);
		file.Close ();

		GameObject.Find ("CurrencyText").GetComponent<Text>().text=d.money.ToString();
	}













	//conver a byte array to an object
	public static object ByteArrayToObject(byte[] arrBytes){
		using(var memStream= new MemoryStream()){
			var binForm = new BinaryFormatter ();
			memStream.Write (arrBytes, 0, arrBytes.Length);
			memStream.Seek (0, SeekOrigin.Begin);
			var obj = binForm.Deserialize (memStream);
			return obj;
		}
	}


	// Convert an object to a byte array
	public static byte[] ObjectToByteArray(object obj)
	{
		BinaryFormatter bf = new BinaryFormatter();
		using (var ms = new MemoryStream())
		{
			bf.Serialize(ms, obj);
			return ms.ToArray();
		}
	}




}
