using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdGenerator : MonoBehaviour {
	
	public List<GameObject> birdList;
	public GameObject birdPrefab;
	public Vector3[] birdLocations;
	public KeyCode[] birdKeys;

	// Use this for initialization

	void Awake(){
		birdList = new List<GameObject> ();
	}

	void Start () {
	}

	public void AddNewBird(int[] triplet){
		GameObject newBird;
		Vector3 targetLocation = birdLocations [birdList.Count];
		Vector3 spawnLocation = new Vector3 (targetLocation.x - 10, targetLocation.y - 10, targetLocation.z);
		Vector3 spotAboveTree = new Vector3 (spawnLocation.x, spawnLocation.y + 20, spawnLocation.z);

		newBird = Instantiate(birdPrefab, spawnLocation, Quaternion.identity) as GameObject;
		newBird.GetComponent<ClickBird>().song = triplet;
		birdList.Add(newBird);
		newBird.GetComponent<ClickBird> ().birdIndex = birdList.Count - 1;

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
