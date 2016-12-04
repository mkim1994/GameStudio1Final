using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour {
	public string scenename;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
		
	public void GoToNextScene(){
		SceneManager.LoadScene (scenename);
	}
}
