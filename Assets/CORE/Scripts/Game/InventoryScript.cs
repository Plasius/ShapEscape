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
	GridLayoutGroup grid;
	public GameObject button;

	void Start () {
		grid = GetComponent<GridLayoutGroup> ();
		restoreSave ();
		PuckLoader ();

	}

	public void GoBack(){
		SceneManager.LoadScene ("MenuScene");

	}

	void restoreSave(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open(Application.persistentDataPath+ "/playerinfo.dat", FileMode.Open);
		d =(PlayerData) bf.Deserialize (file);
		file.Close ();

	}








	void SaveChanges(){

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+ "/playerinfo.dat");
		bf.Serialize (file, d);
		file.Close ();

		restoreSave ();

	}










	public void PuckLoader(){
		grid.transform.DetachChildren ();
		for(int i=0; i< d.puckList.Length; i++){
			string color = d.puckList [i];

			GameObject b = Instantiate (button) as GameObject;
			b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Pucks/"+color);
			//b.GetComponent<Button> ().onClick.AddListener (() => {});
			b.transform.SetParent (grid.transform, false);

		}

	}

	public void ShapeLoader(){
		grid.transform.DetachChildren ();

		for(int i=0; i< d.shapeList.Length; i++){

			GameObject b = Instantiate (button) as GameObject;
			b.GetComponent<Button> ().name = d.shapeList [i];

			if (d.shapeList[i].StartsWith ("X") || d.shapeList[i].StartsWith ("Y")) {
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Menu/currency");

				b.GetComponent<Button>().onClick.AddListener (() => {
					if((b.name.StartsWith("X") && d.money>=10) || (b.name.StartsWith("Y") && d.money>=20)){
						for(int j=0; j<d.shapeList.Length; j++){
							if(d.shapeList[j]==b.name){
								d.shapeList[j]=d.shapeList[j].Substring(1);
							}
						}
						PlayerPrefs.SetString ("ShapeColor", this.name.Substring(1));
						b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + b.name.Substring(1) + "/circle_" + b.name.Substring(1));
						SaveCloud(b.name.Substring(0,1));
					}
				});

			} else {
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/" + d.shapeList[i] + "/circle_" + d.shapeList[i]);
				b.GetComponent<Button> ().onClick.AddListener (() => {
					PlayerPrefs.SetString ("ShapeColor", b.name);

				});
			}

			b.transform.SetParent (grid.transform, false);

		}

	}

	public void BGLoader(){
		grid.transform.DetachChildren ();
		for(int i=0; i< d.bgList.Length; i++){
			GameObject b = Instantiate (button) as GameObject;
			b.GetComponent<Button>().name= d.bgList[i];

			if (d.bgList[i].StartsWith ("X") || d.bgList[i].StartsWith ("Y")) {
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Menu/currency");

				b.GetComponent<Button> ().onClick.AddListener (() => {
					if((b.name.StartsWith("X") && d.money>=10) || (b.name.StartsWith("Y") && d.money>=20)){
						for(int j=0; j<d.bgList.Length; j++){
							if(d.bgList[j]==b.name){
								d.bgList[j]=d.bgList[j].Substring(1);
							}
						}

						PlayerPrefs.SetString ("BGColor", b.name.Substring(1));
						b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg" + b.name.Substring(1));
						Debug.Log("showing: "+ b.name.Substring(1));
						SaveCloud(b.name.Substring(0,1));
					}
				});

			}else {
				b.transform.GetChild(0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/BGs/bg" + d.bgList[i]);
				b.GetComponent<Button> ().onClick.AddListener (() => {
					PlayerPrefs.SetString ("BGColor", b.name);
				});
			}

			b.transform.SetParent (grid.transform, false);
		}

	}











	void SaveCloud(string type){
		GameObject.Find ("LoadingPanel").transform.localScale= new Vector3(1,1,1);
		if (type == "X") {
			d.money -= 10;
		}else {
			d.money -= 20;
		}
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

		restoreSave ();
		PuckLoader ();
		GameObject.Find ("CurrencyText").GetComponent<Text>().text=d.money.ToString();
		GameObject.Find ("LoadingPanel").transform.localScale= new Vector3(0,0,0);
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
