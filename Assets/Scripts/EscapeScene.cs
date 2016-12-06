using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscapeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad (transform.gameObject);
		//Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name != "Menu" && Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("Menu");
		}

		if (SceneManager.GetActiveScene ().name == "MelScene" && Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene ("MelScene");
		}
	}
}
