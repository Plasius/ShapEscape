using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;

public class MenuMasterScript : MonoBehaviour {
	string scene;
	bool auting=false;
	bool loading=false;
	PlayerData d;

	void Start(){
		d = new PlayerData (new string[]  {"Puck"}, new string[]  {"cyan", "lemon", "lime", "magenta", "Yvulkan" }, new string[]   {"black", "blue", "Ypurple"});
		GooglePlayGames.BasicApi.PlayGamesClientConfiguration config = new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
		PlayGamesPlatform.InitializeInstance(config);
		GooglePlayGames.PlayGamesPlatform.Activate ();
		Debug.Log ("starting");
	}


	void OnGUI(){
		if(!auting)
		Social.localUser.Authenticate ((bool success) => {
				if (success){
					//DEBUG
					SaveCloud();
				}else{ 
					auting=false;
					return;
				}
			});
		auting = true;
	}
	
	public void ChangeScene(string s){
		GetComponent<Animator> ().SetTrigger ("Exiting");
		scene = s;
		StartCoroutine ("MyWaiter");

	}


	IEnumerator MyWaiter(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (scene);

	}








	bool saving=true;


	void SaveCloud(){
		OpenSavedGame ("ShapEscapeData");
	}

	void OpenSavedGame(string filename) {
		Debug.Log ("opening saved data");
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(filename, GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
			ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
	}

	public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game) {
		Debug.Log ("got status");
		if (status == SavedGameRequestStatus.Success) {
			// handle reading or writing of saved game.
			if (saving) {
				SaveGame (game,ObjectToByteArray(d));	
			} else {
				Debug.Log ("loading game data");
				LoadGameData (game);
			}
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
			LoadCloud();
		} else {
			// handle error
			Debug.Log ("error3");
		}
	}



	void LoadCloud(){
		saving = false;
		OpenSavedGame ("ShapEscapeData");
	
	}


	void LoadGameData (ISavedGameMetadata game) {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
	}

	public void OnSavedGameDataRead (SavedGameRequestStatus status, byte[] data) {
		if (status == SavedGameRequestStatus.Success) {
			// handle processing the byte array data
			d=(PlayerData) ByteArrayToObject(data);
			Debug.Log ("starting local save");
			Savelocal ();
		} else {
			// handle error

			Debug.Log ("error4");
		}
	}



	void Savelocal(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+ "/playerinfo.dat");
		bf.Serialize (file, d);
		file.Close ();


		GetComponent<Animator> ().SetTrigger ("LoggedIn");
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
