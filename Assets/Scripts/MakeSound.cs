﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;


public class MakeSound : MonoBehaviour {
	public AudioClip[] sounds;

	public AudioSource[] soundsources;

	public KeyCode[] music_keys;
	public Image[] ButtonImage;
	public Sprite DeactivateImage;
	public Sprite activateImage;

	public TextAsset transitionMatrixFile;

	public GameObject mainCamera;
	private ClickBird clickBird;
	public GameObject birdGeneratorObj;
	private BirdGenerator birdGenerator;

	private int[,] transitionMatrix;
	private int matrixSize;
	private int currentLastNote;
	private int currentActiveTripletIndex;
	private int currentActiveNoteIndex;
	private int currentMaxTripletIndex;
	public int maxSongLength;


	private Animator endBk;
	private Animator treeLight;

	private AudioSource audioSource;
	private Animator characterani;
	private Songlist song;
	public List<int[]> tripletList;
	private int note;
	public bool ifFinish;
	// Use this for initialization
	void Start () {
		song = GetComponent<Songlist> ();
		characterani = GameObject.FindWithTag("PlayerCharacter").GetComponent<Animator> ();
		endBk = GameObject.FindWithTag ("SunriseCanvas").GetComponent<Animator> ();
		treeLight = GameObject.FindWithTag("Tree").GetComponent<Animator> ();
		birdGenerator = birdGeneratorObj.GetComponent<BirdGenerator> ();

		audioSource = GetComponent<AudioSource> ();
		note = 0;
		ifFinish = false;

		matrixSize = 10;
		transitionMatrix = new int[matrixSize,matrixSize];
		tripletList = new List<int[]> ();
		ParseTransitionMatrix ();
		int noteSeed = UnityEngine.Random.Range (0, matrixSize);
		GenerateTriplet (noteSeed);
		currentActiveTripletIndex = 0;
		currentActiveNoteIndex = 0;
		currentMaxTripletIndex = 0;
		clickBird = birdGenerator.birdList[0].bird.GetComponent<ClickBird> ();

	}

	// Update is called once per frame
	void Update () {

		SoundLogic ();

		if (audioSource.isPlaying == false) {
			characterani.SetBool ("isplaying", false); 
		}

	}

	void ParseTransitionMatrix(){
		string fileFullString;
		string[] fileLines;
		string[] lineEntries;
		string fileLine;
		string[] lineSeparator = new string[] { "\r\n", "\r", "\n" };
		char[] entrySeparator = new char[] { ',' };
		fileFullString = transitionMatrixFile.text;
		fileLines = fileFullString.Split (lineSeparator, StringSplitOptions.None);
		for (int i = 0; i < matrixSize; i++) {
			fileLine = fileLines [i];
			lineEntries = fileLine.Split (entrySeparator, StringSplitOptions.None);
			for (int j = 0; j < matrixSize; j++) {
				transitionMatrix [i,j] = Convert.ToInt32(lineEntries[j]);
			}
		}
	}

	void GenerateTriplet(int lastNote){
		int[] triplet = new int[3];
		int firstNote = GenerateNote (lastNote);
		int secondNote = GenerateNote (firstNote);
		int thirdNote = GenerateNote (secondNote);
		triplet [0] = firstNote;
		triplet [1] = secondNote;
		triplet [2] = thirdNote;
		currentLastNote = thirdNote;
		tripletList.Add (triplet);
		birdGenerator.AddNewBird (triplet);
	}

	int GenerateNote(int lastNote){
		int note = 0;
		int noteRange = 0;
		for (int i = 0; i < matrixSize; i++) {
			noteRange += transitionMatrix [lastNote,i];
		} 
		int nextNoteLocation = UnityEngine.Random.Range (0, noteRange);
		int currentLocation = 0;
		for (int j = 0; j < matrixSize; j++) {
			currentLocation += transitionMatrix [lastNote, j];
			if (currentLocation > nextNoteLocation) {
				note = j;
				break;
			}
		}

		return note;
	}

	void SoundKeyPressed(int i, bool octaveShift){

		if (!soundsources [i].isPlaying) {

			soundsources [i].Play ();

			characterani.SetBool ("isplaying", true);
			ButtonImage [i].gameObject.GetComponent<Animator> ().SetBool ("ifPressed", true);

			/*audioSource.clip = sounds [i];
		audioSource.Play ();*/

			int j = i;

			if (octaveShift) {
				j += 5;
			}

			if (!ifFinish) {
				if (j == tripletList[currentActiveTripletIndex][currentActiveNoteIndex]){
					ButtonImage [i].gameObject.GetComponent<Animator> ().SetBool ("ifRight", true);
					if (currentActiveNoteIndex < 2) {
						currentActiveNoteIndex += 1;
					} else {
						currentActiveNoteIndex = 0;
						currentActiveTripletIndex += 1;
						if (currentActiveTripletIndex > currentMaxTripletIndex) {
							if (currentActiveTripletIndex < maxSongLength) {
								GenerateTriplet (currentLastNote);
								currentMaxTripletIndex += 1;
								currentActiveNoteIndex = 0;
								currentActiveTripletIndex = 0;
								StartCoroutine (clickBird.playSong (true));
							} else {
								ifFinish = true;
								characterani.SetBool ("iscorrect", true);
								endBk.SetBool ("isSunrise", true);
								treeLight.SetBool ("isTreelight", true);
							}
						}
					}
				} else {
					currentActiveTripletIndex = 0;
					currentActiveNoteIndex = 0;
					ButtonImage [i].gameObject.GetComponent<Animator> ().SetBool ("ifRight", false);
				} 
			}
		}



	}

	void SoundLogic()
	{
		for (int i = 0; i < sounds.Length; i++) {
			//if (Input.GetKeyDown (music_keys [i]) && Input.GetKey (KeyCode.Space)) {
			if (Input.GetKey(music_keys [i]) && Input.GetKey (KeyCode.Space)) {
				//audioSource.pitch = 2;
				//audioSource.volume = 1.0f;
				soundsources[i].pitch = 2;
				soundsources [i].volume = 1.0f;
				SoundKeyPressed (i, true);
			} else if (Input.GetKey(music_keys [i])) {
				//else if (Input.GetKeyDown (music_keys [i])) {
				//audioSource.pitch = 1;
				//audioSource.volume = 1.0f;
				soundsources[i].pitch = 1;
				soundsources [i].volume = 1.0f;
				SoundKeyPressed (i, false);
			} else if(Input.GetKeyUp(music_keys[i])){
				StartCoroutine (fadeAudio (i));
			}
		}
	}

	/*IEnumerator fadeAudio(int i){
		for (int t = 9; t > 0; t--) {
			soundsources[i].volume = t * 0.1f;
			yield return new WaitForSeconds (0.01f);
		}
		soundsources [i].volume -= 0.1f * Time.deltaTime;
		yield return new WaitForSeconds (1.0f);
		soundsources[i].Stop();
	}*/

	IEnumerator fadeAudio(int i){
		float startVolume = soundsources [i].volume;
		float duration = 0.1f;
		float inverseDuration = 1.0f / duration;
		float lerpFactor = 0.0f;
		while (lerpFactor <= 1.0f) {
			soundsources [i].volume = Mathf.Lerp (startVolume, 0.0f, lerpFactor);
			lerpFactor = lerpFactor + Time.deltaTime * inverseDuration;
			yield return 1.0f;
		}
		soundsources [i].volume = 0.0f;
		soundsources [i].Stop ();
	}
}
