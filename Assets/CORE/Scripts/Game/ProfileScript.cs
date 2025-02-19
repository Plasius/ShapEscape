﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour {
    PlayerData d;

	// Use this for initialization
	void Start () {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
        d = (PlayerData)bf.Deserialize(file);
        file.Close();

        GameObject.Find("GamesPlayedText").GetComponent<Text>().text = "Games played: "+d.gamesPlayed;
        GameObject.Find("TimeSpentText").GetComponent<Text>().text = "Time spent in-game: " + d.totalSeconds + "s";
        GameObject.Find("UnlockedText").GetComponent<Text>().text = "Unlocked items: " + d.unlocked +"/15";
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            goBack();

    }


    public void showAchievementsUI(){
		Social.ShowAchievementsUI ();
		
	}

	public void goBack(){
		Debug.Log ("returning");
		SceneManager.LoadScene ("MenuScene");
	}

    public void showLeader() {
        Social.ShowLeaderboardUI();
    }


}
