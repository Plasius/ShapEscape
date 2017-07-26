using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;

using GoogleMobileAds;
using GoogleMobileAds.Api;

public class SaveScript : MonoBehaviour {
    string gamemode;
    float time;
    int money;
    PlayerData d;
    public static SaveScript instance;

	// Use this for initialization
	void Start () {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
        d = (PlayerData)bf.Deserialize(file);
        file.Close();

        instance = this;


    }

    public void SaveSession(string g, float t, int m) {
        gamemode = g;
        time = t;
        d.money += m;


        SaveTimes();
        StartCoroutine(saver());
        SaveAchievements();
    }



    void SaveAchievements() {
        //achievements
        //nr of games
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIxs2M-tEfEAIQAw", 1, (bool success) => {
            // handle success or failure

        });

        PlayGamesPlatform.Instance.IncrementAchievement("CgkIxs2M-tEfEAIQBA", 1, (bool success) => {
            // handle success or failure
        });

        PlayGamesPlatform.Instance.IncrementAchievement("CgkIxs2M-tEfEAIQBQ", 1, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIxs2M-tEfEAIQBg", 1, (bool success) => {
            // handle success or failure
        });


        //total seconds
        //achievements
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIxs2M-tEfEAIQBw", (int)time, (bool success) => {
            // handle success or failure
        });

        PlayGamesPlatform.Instance.IncrementAchievement("CgkIxs2M-tEfEAIQCg", (int)time, (bool success) => {
            // handle success or failure
        });

        PlayGamesPlatform.Instance.IncrementAchievement("CgkIxs2M-tEfEAIQCw", (int)time, (bool success) => {
            // handle success or failure
        });
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIxs2M-tEfEAIQDA", (int)time, (bool success) => {
            // handle success or failure
        });


        //in-game seconds
        //achievements
        if (time > 30)
            Social.ReportProgress("CgkIxs2M-tEfEAIQDQ", 100.0f, (bool success) => {
                // handle success or failure
            });

        if (time > 69)
            Social.ReportProgress("CgkIxs2M-tEfEAIQDg", 100.0f, (bool success) => {
                // handle success or failure
            });
        if (time > 100)
            Social.ReportProgress("CgkIxs2M-tEfEAIQDw", 100.0f, (bool success) => {
                // handle success or failure
            });
        if (time > 420)
            Social.ReportProgress("CgkIxs2M-tEfEAIQEA", 100.0f, (bool success) => {
                // handle success or failure
            });

    }

    void SaveTimes() {
        d.gamesPlayed++;
        d.totalSeconds += (int)time;
    }






















    public IEnumerator saver()
    {
        OpenSavedGame("ShapEscapeData3");
        yield return new WaitForSeconds(0);

    }
    

    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, GooglePlayGames.BasicApi.DataSource.ReadNetworkOnly,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            SaveGame(game, ObjectToByteArray(d));
        }
        else
        {
            // handle error
            Debug.Log("fail");
        }
    }

    void SaveGame(ISavedGameMetadata game, byte[] savedData)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            //we got the money
            SaveLocal();
        }
        else
        {
            // handle error
            Debug.Log("fail2");
        }
    }

    void SaveLocal()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.dat");
        bf.Serialize(file, d);
        file.Close();
    }






    //conver a byte array to an object
    public static object ByteArrayToObject(byte[] arrBytes)
    {
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = binForm.Deserialize(memStream);
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
