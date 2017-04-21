using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;


public class ADMoneyScript : MonoBehaviour {
	PlayerData d;
	int money;
	// Use this for initialization
	void Start () {
		Refresh ();
	}

	void Refresh(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open(Application.persistentDataPath+ "/playerinfo.dat", FileMode.Open);
		d=(PlayerData) bf.Deserialize (file);
		file.Close ();

		GetComponent<Text>().text=d.money.ToString();
	}



	void addMoney(){
		d.money += 10;
		OpenSavedGame ("ShapEscapeData1");
	}

	void launchAD(){
		
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
			Debug.Log ("fail");
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
			//we got the money
			GetComponent<Text>().text=d.money.ToString();

		} else {
			// handle error
			Debug.Log ("fail2");
		}
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
