using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AboutScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Social.ReportProgress("CgkIxs2M-tEfEAIQFw", 100.0f, (bool success) => {
            // handle success or failure
        });

    }
	


	// Update is called once per frame
	void Update () {
		
	}
}
