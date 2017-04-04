using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScript : MonoBehaviour {
		void OnGUI(){
			
			StartCoroutine ("MyWaiter");

		}


		IEnumerator MyWaiter(){
			yield return new WaitForSeconds (2);
			SceneManager.LoadScene ("MenuScene");
		}
}
