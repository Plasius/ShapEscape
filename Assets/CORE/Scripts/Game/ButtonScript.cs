using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	public void leave(){
		GetComponent<Animator> ().SetBool ("leave", true);


	}

}
