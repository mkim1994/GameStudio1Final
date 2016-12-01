using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;


public class MakeSound : MonoBehaviour {
	public AudioClip[] sounds;

	public AudioSource[] soundsources;
	public AudioSource[] shortsounds;

	public KeyCode[] music_keys;
	public Image[] ButtonImage;
	public Sprite DeactivateImage;
	public Sprite activateImage;

	public TextAsset transitionMatrixFile;

	public GameObject mainCamera;
	//private ClickBird clickBird;
	public GameObject birdGeneratorObj;
	private BirdGenerator birdGenerator;

	private int[,] transitionMatrix;
	private int matrixSize;
	private int currentLastNote;
	private int currentActiveTripletIndex;
	private int currentActiveNoteIndex;
	private int currentMaxTripletIndex;
	public int maxSongLength;

	private KeyCode[] inputKeys;


	private Animator endBk;
	private Animator treeLight;

	private AudioSource audioSource;
	private Animator characterani;
	private Songlist song;
	public List<int[]> tripletList;
	private int note;
	public bool ifFinish;
	public GameObject player;
	private ParticleSystem playerParticles;

	private int prevIndex;


	// Use this for initialization
	void Start () {
		song = GetComponent<Songlist> ();
		characterani = GameObject.FindWithTag("PlayerCharacter").GetComponent<Animator> ();
		endBk = GameObject.FindWithTag ("SunriseCanvas").GetComponent<Animator> ();
		treeLight = GameObject.FindWithTag("Tree").GetComponent<Animator> ();
		birdGenerator = birdGeneratorObj.GetComponent<BirdGenerator> ();
		playerParticles = player.GetComponentInChildren<ParticleSystem> ();

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
		//clickBird = birdGenerator.birdList[0].bird.GetComponent<ClickBird> ();

		prevIndex = 0;


	}

	// Update is called once per frame
	void Update () {

		SoundLogic ();
		//CheckSourcesToStopPlayerParticles ();

		/*if (audioSource.isPlaying == false) {
			characterani.SetBool ("isplaying", false); 
		}*/

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
		//StartCoroutine (birdGenerator.birdList[birdGenerator.birdList.Count-1].bird.GetComponent<ClickBird>().playSong (true));
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

	void CheckSourcesToStopPlayerParticles(){
		bool playing = false;
		for (int i = 0; i < 5; i++) {
			if (soundsources [i].isPlaying) {
				playing = true;
			}
		}
		if (!playing) {
			playerParticles.Stop ();
		}
	}


	void SoundKeyPressed(int i, bool octaveShift){

		playerParticles.Emit (1);
		playerParticles.Stop ();

		prevIndex = i;

		if (!shortsounds [i].isPlaying) {

			/*soundsources [i].volume = 0.0f;
			soundsources [i].Play ();*/
			//StartCoroutine (fadeAudio (i, true));
			//shortsounds[i].PlayOneShot(shortsounds[i].clip);
			shortsounds [i].volume = 1.0f;
			shortsounds[i].Play();

			characterani.SetBool ("isplaying", true);
			//ButtonImage [i].gameObject.GetComponent<Animator> ().SetBool ("ifPressed", true);
			activeButton(i, true);

			/*audioSource.clip = sounds [i];
		audioSource.Play ();*/

			int j = i;

			if (octaveShift) {
				j += 5;
			}

			if (!ifFinish) {
				if (j == tripletList[currentActiveTripletIndex][currentActiveNoteIndex]){
					//ButtonImage [i].gameObject.GetComponent<Animator> ().SetBool ("ifRight", true);
					birdGenerator.birdList [currentActiveTripletIndex].GetComponent<ClickBird> ().SetHappyParticles (true, currentActiveNoteIndex);
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
					//ButtonImage [i].gameObject.GetComponent<Animator> ().SetBool ("ifRight", false);
					for (int k = 0; k < birdGenerator.birdList.Count; k++){
						birdGenerator.birdList[k].GetComponent<ClickBird>().SetHappyParticles(false, 0);
					}
				} 
			}
		}



	}

	void SoundLogic()
	{

		bool currentlyplaying = false;
		//check if there are any sounds currently playing



		for (int i = 0; i < sounds.Length; i++) {
			if (!currentlyplaying) {
				if (Input.GetKey (KeyCode.J)) {
					activeButton (5,true);
					if (Input.GetKeyDown (music_keys [i])) {
					
						shortsounds [i].pitch = 2;
						SoundKeyPressed (i, true);
						//StartCoroutine (fadeAudio (i, true));
						//soundsources[i].pitch = 2;
						//soundsources [i].Play ();
						//StartCoroutine (fadeAudio (i, true));
						currentlyplaying = true;
					}
				} else if (Input.GetKeyDown (music_keys [i])) {
					shortsounds [i].pitch = 1;
					SoundKeyPressed (i, false);
					//StartCoroutine (fadeAudio (i, true));
					//	soundsources[i].pitch = 1;
					//soundsources [i].Play ();
					//StartCoroutine (fadeAudio (i, true));
					currentlyplaying = true;
				}

				if (Input.GetKeyUp (music_keys [i])) {
					activeButton(i, false);
					//StartCoroutine(fadeAudio(i,false));
				} else if(Input.GetKeyUp(KeyCode.J)){
					activeButton(5,false);
				}
			}

			/*if (Input.GetKeyDown (music_keys [i]) && prevIndex != i) {
			StartCoroutine (fadeAudio (prevIndex));
		}*/

			/*if (Input.GetKey (music_keys [i]) && Input.GetKey (KeyCode.Space)) {
			soundsources [i].pitch = 2;
			soundsources [i].volume = 1.0f;
			SoundKeyPressed (i, true);
		} else if (Input.GetKey (music_keys [i])) {
			//else if (Input.GetKeyDown (music_keys [i])) {
			soundsources [i].pitch = 1;
			soundsources [i].volume = 1.0f;
			SoundKeyPressed (i, false);
		} else if (Input.GetKeyUp (music_keys [i])) {
			StartCoroutine (fadeAudio (i, false));
		}*/

		}
	}
	
	void activeButton(int index, bool held){
		ButtonImage[index].gameObject.SetActive(held);
		
	}


	IEnumerator fadeAudio(int i, bool fadein){
		float startVolume;
		float endVolume;
		float duration;
		float startTime = Time.time;
		if (fadein) {
			startVolume = 0.0f;
			endVolume = 1.0f;
			duration = 0.5f;
		} else { //fadeout
			//startVolume = soundsources [i].volume;
			startVolume = 1.0f;
			endVolume = 0.0f;
			duration = 0.5f;
		}
		float inverseDuration = 1.0f / duration;
		float lerpFactor = 0.0f;
		while (lerpFactor <= 1.0f && ((fadein && soundsources[i].volume < 1.0f) ||
			!fadein && soundsources[i].volume > 0.0f)) {

			//soundsources [i].volume = Mathf.Lerp (startVolume, endVolume, lerpFactor);

			/*
			soundsources[i].volume = Mathf.SmoothDamp(startVolume, endVolume, lerpFactor, duration);
			lerpFactor = lerpFactor + Time.deltaTime * inverseDuration;
			yield return 1.0f;*/

			lerpFactor += Time.deltaTime / duration;
			soundsources [i].volume = Mathf.Lerp (startVolume, endVolume, lerpFactor);
			yield return null;
		}



		if (!fadein) {
			soundsources [i].volume = 0.0f;

			soundsources [i].Stop ();


		} else {
			soundsources [i].volume = 1.0f;
		}
	}

}
