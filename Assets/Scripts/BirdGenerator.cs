using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdGenerator : MonoBehaviour {
	
	public List<GameObject> birdList;
	public GameObject birdPrefab;
	public Vector3[] birdLocations;
	public KeyCode[] birdKeys;
	public Material[] birdMaterials;
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
		birdColors [0] = Color.red;
		birdColors [1] = Color.green;
		birdColors [2] = Color.blue;
		birdColors [3] = Color.yellow;
	}

	public void AddNewBird(int[] triplet){
		GameObject newBird;
		Vector3 targetLocation = birdLocations [birdList.Count];
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
		clickBird.particleColor = birdColors [index];

		Vector3[] path = new Vector3[2]{ spotAboveTree, targetLocation };
		iTween.MoveTo(newBird, iTween.Hash("path", path, "time", 2, "easetype", "easeOutCubic"));
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
