using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdGenerator : MonoBehaviour {
	public class Bird {
		public GameObject bird;
		int[] triplet;

		public Bird(int[] noteTriplet, GameObject birdPrefab, Vector3 spawnPosition){

			bird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity) as GameObject;

			//trying to have different colors for birds... not working atm
			Renderer rend = bird.GetComponentInChildren<Renderer>();
			/*rend.material.SetColor("_Color", 
				new Color(Random.Range (0f,1f), Random.Range (0f, 1f), Random.Range (0f, 1f))); //albedo
			DynamicGI.SetEmissive(rend,new Color(1f,0.1f,0.5f,1.0f)*5);
			//_emission?*/

			triplet = noteTriplet;
		}

	}

	public List<Bird> birdList;
	public GameObject birdPrefab;
	public Vector3[] birdLocations;

	// Use this for initialization
	void Start () {
	}

	public void AddNewBird(int[] triplet){
		if (birdList == null) {
			birdList = new List<Bird> ();
		}
		Bird newBird;
		newBird = new Bird (triplet, birdPrefab, transform.position);


		birdList.Add (newBird);
		Vector3 targetLocation = birdLocations [birdList.Count - 1];
		Vector3 spotAboveTree = new Vector3 (transform.position.x, 20, transform.position.z);
		Vector3[] path = new Vector3[2]{ spotAboveTree, targetLocation };
		iTween.MoveTo(newBird.bird, iTween.Hash("path", path, "time", 2, "easetype", "easeOutCubic"));
	}

	// Update is called once per frame
	void Update () {
	
	}
}
