using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdGenerator : MonoBehaviour {
	
	public List<GameObject> birdList;
	public GameObject birdPrefab;
	public Vector3[] birdLocations;
	public GameObject[] birdLocationObjects;
	public GameObject blueBirdLocationObject;
	public KeyCode[] birdKeys;
	public Material[] birdMaterials;
	public Material blueBirdMaterial;
	public Material[] confusedMaterials;
	public Material[] playingMaterials;

	private Color[] birdColors;

	// Use this for initialization

	void Awake(){
		birdList = new List<GameObject> ();
		InitializeColorArray ();

	}

	void Start () {

	}

	void InitializeColorArray(){
		birdColors = new Color[4];
		birdColors [0] = new Color(0.42f,0.15f,0.75f); //purple #6a26be
		birdColors [1] = new Color(0.6f,0.64f,0.2f); //green #9ca40b
		birdColors [2] = new Color(0.71f,0.34f,0.05f); //orange #b5570e
		birdColors [3] = new Color(0.71f,0.15f,0.11f); //red #b3160d
		/*birdColors [0] = Color.white;
		birdColors [1] = Color.white;
		birdColors [2] = Color.white;
		birdColors [3] = Color.white;*/

	}

	public void AddNewBird(int[] triplet){
		GameObject newBird;
		//Vector3 targetLocation = birdLocations [birdList.Count]; //birdlocationobjects transform.position

		Vector3 targetLocation = birdLocationObjects [birdList.Count].transform.position;
		Vector3 spawnLocation = new Vector3 (targetLocation.x - 10, targetLocation.y - 10, targetLocation.z);
		Vector3 spotAboveTree = new Vector3 (spawnLocation.x, spawnLocation.y + 20, spawnLocation.z);

		newBird = Instantiate(birdPrefab, spawnLocation, Quaternion.identity) as GameObject;
		ClickBird clickBird;
		clickBird = newBird.GetComponent<ClickBird> ();
		clickBird.song = triplet;
		birdList.Add(newBird);
		int index = birdList.Count - 1;
		clickBird.birdIndex = index;
		clickBird.birdMaterial = birdMaterials [index];
		clickBird.confusedMaterial = confusedMaterials [index];
		clickBird.playingMaterial = playingMaterials [index];

		//clickBird.particleColor = birdColors [index];

		Vector3[] path = new Vector3[2]{ spotAboveTree, targetLocation };
		iTween.MoveTo(newBird, iTween.Hash("path", path, "time", 2, "easetype", "easeOutCubic"));
	}

	public void AddFinalBlueBird(){
		GameObject blueBird;
		Vector3 targetLocation = blueBirdLocationObject.transform.position;
		Vector3 spawnLocation = new Vector3 (targetLocation.x - 10, targetLocation.y - 10, targetLocation.z - 10);
		Vector3 tweenPathMidpoint = new Vector3 (spawnLocation.x, spawnLocation.y + 20, spawnLocation.z);
		Quaternion birdRotation = Quaternion.Euler (0, -90, 0);
		blueBird = Instantiate (birdPrefab, spawnLocation, birdRotation) as GameObject;

		blueBird.GetComponent<ClickBird> ().birdMaterial = blueBirdMaterial;

		Vector3[] path = new Vector3[2]{ tweenPathMidpoint, targetLocation };

		iTween.MoveTo(blueBird, iTween.Hash("path", path, "time", 4, "easetype", "easeOutCubic"));
	}

	// Update is called once per frame
	void Update () {
	}

	public void SetSongPlayingPrivilege(bool canPlay){
		for (int i = 0; i < birdList.Count; i++) {
			birdList [i].GetComponent<ClickBird> ().allowedToPlay = canPlay;
		}
	}
}
