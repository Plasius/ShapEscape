using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class casualScript : MonoBehaviour {
    string ScoreBoardID;
	string gamemode;
    int money;
    public static casualScript instance;
    public  ADMoneyScript adscript;

    void Start () {
        //download and save highscore
        RestoreHighScore();
        gamemode = PlayerPrefs.GetString("GameMode");
        SetScoreBoardID();

        instance = this;

		string shapeColor= PlayerPrefs.GetString("ShapeColor", "white");
		string bgColor=PlayerPrefs.GetString("BGColor", "black");
		string puckColor= PlayerPrefs.GetString("PuckColor", "white");




		GameObject.Find ("Puck").GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite> ("Sprites/Pucks/puck_"+ puckColor);

		GameObject.Find ("Square").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+shapeColor+"/square_"+ shapeColor);
		GameObject.Find ("Triangle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+shapeColor+"/triangle_"+ shapeColor);
		GameObject.Find ("Circle").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Shapes/"+shapeColor+"/circle_"+ shapeColor);

		GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite>("Sprites/BGs/bg"+bgColor);

	}

	void RestoreHighScore(){
		GameObject.Find("ScoreText").GetComponent<Text>().text="Best: "+ PlayerPrefs.GetFloat (PlayerPrefs.GetString("GameMode")+"Score");

	}
    

    void SetScoreBoardID()
    {
        switch (gamemode)
        {
            case "casual":
                ScoreBoardID = "CgkIxs2M-tEfEAIQCA";
                break;

            case "lives":
                ScoreBoardID = "CgkIxs2M-tEfEAIQCQ";
                break;

        }

    }

    public void ReturnToMenu(){
		SceneManager.LoadScene ("GameModeScene");

	}

    public void RestartGame() {
        SceneManager.LoadScene(gamemode + "Scene");
    }


    public IEnumerator poster(long data)
    {
        Social.ReportScore(data, ScoreBoardID, (bool success) => { });
        yield return new WaitForSeconds(0);

    }

    public void ShowLeaderBoard() {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(ScoreBoardID);
    }

    public void ShowAchievements() {
        Social.ShowAchievementsUI();
    }

    public void dealWithSession(float cTime) {
        StartCoroutine(poster((long)(cTime * 100) + 1));

        if (PlayerPrefs.GetFloat(gamemode + "Score") < float.Parse(cTime.ToString("f2")))
        {
            PlayerPrefs.SetFloat(gamemode + "Score", float.Parse(cTime.ToString("f2")));
        }
        
        money = (int)(((int)cTime)/12);
        if (money == 0)
            GameObject.Find("DoubleButton").transform.localScale= new Vector3(0,0,0);

        GameObject.Find("MoneyText").GetComponent<Text>().text="earned: "+money;
        SaveScript.instance.SaveSession(gamemode, float.Parse(cTime.ToString("f2")), money);
    }

    public void DoubleMoney()
    {
        adscript.launchAD(10);
    }


}
