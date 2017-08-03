using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;


public class LogoScript : MonoBehaviour {

	string scene;
	bool auting=false;
	PlayerData d;

	void Start(){
		d = new PlayerData (new string[1]  {"white"}, new string[1]  {"white"}, new string[1]   {"black"}, 0);
		GooglePlayGames.BasicApi.PlayGamesClientConfiguration config = new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
		PlayGamesPlatform.InitializeInstance(config);
		GooglePlayGames.PlayGamesPlatform.Activate ();
		
	}



	void OnGUI(){
		if(!auting)
			Social.localUser.Authenticate ((bool success) => {
				if (success){
					
					OpenSavedGame ("ShapEscapeData4");
				}else{ 
					auting=false;
					return;
				}
			});
		auting = true;
	}



	bool saving;

	void OpenSavedGame(string filename) {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(filename, GooglePlayGames.BasicApi.DataSource.ReadNetworkOnly,
			ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
	}

	public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
			// handle reading or writing of saved game.
			if(!saving)
				LoadGameData (game);
			else
				SaveGame (game,ObjectToByteArray(d));	
			
		} else {



			// handle error FIRST LOGIN
			Debug.Log("RUNNINGFUICK");
			switch(status){
			case SavedGameRequestStatus.BadInputError:
				Debug.Log ("badinputerror");
				break;
			case SavedGameRequestStatus.InternalError:
				Debug.Log ("internalerror");
				break;
			case SavedGameRequestStatus.TimeoutError:
				Debug.Log ("timeout");
				break;
			}
		}
        
	}

	void LoadGameData (ISavedGameMetadata game) {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
	}

	public void OnSavedGameDataRead (SavedGameRequestStatus status, byte[] data) {
		if (status == SavedGameRequestStatus.Success) {
			// handle processing the byte array data
			if (data == null || data.Length == 0) {
				Debug.Log ("null");

				saving = true;
				OpenSavedGame ("ShapEscapeData4");

				return;
			}

			
			d=(PlayerData) ByteArrayToObject(data);
			SaveLocal ();
		} else {
			// handle error

			Debug.Log ("error4");
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
			SaveLocal();
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


		SceneManager.LoadScene ("MenuScene");
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
