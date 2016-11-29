using UnityEngine;
using System.Collections;

public class WinState : MonoBehaviour {
	private Animator characterani;


	void Start (){
		characterani = GameObject.FindWithTag("PlayerCharacter").GetComponent<Animator> ();

	}

	public void playOnce()
	{
		characterani.SetBool ("iscorrect", false);
	
	}
}
