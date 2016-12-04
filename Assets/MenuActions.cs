using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayGame(){
		SceneManager.LoadScene ("IntroScene");
	}

	public void CreditsPage(){
		print ("credits");
	}

	public void QuitGame(){

		Application.Quit ();
	}
}
