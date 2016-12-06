using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class MakeSound : MonoBehaviour {

	private bool currentlyplaying;
	private float defaultvolume = 0.3f;

	public AudioSource[] soundsources;
	public AudioSource[] shortsounds;

	public KeyCode[] music_keys;
	public Image[] ButtonImage;

	public TextAsset transitionMatrixFile;

	public GameObject mainCamera;
	//private ClickBird clickBird;
	public GameObject birdGeneratorObj;
	private BirdGenerator birdGenerator;
	private Call_UI callUI;

	private int[,] transitionMatrix;
	private int matrixSize;
	private int currentLastNote;
	public int currentActiveTripletIndex;
	public int currentActiveNoteIndex;
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
	private bool birdsPresent;
	public GameObject player;
	private ParticleSystem playerParticles;

	private int prevIndex;

	private GameObject UpperBodyTurn;


	// Use this for initialization
	void Start () {
		song = GetComponent<Songlist> ();
		characterani = GameObject.FindWithTag("PlayerCharacter").GetComponent<Animator> ();
		endBk = GameObject.FindWithTag ("SunriseCanvas").GetComponent<Animator> ();
		treeLight = GameObject.FindWithTag("Tree").GetComponent<Animator> ();
		birdGenerator = birdGeneratorObj.GetComponent<BirdGenerator> ();
		playerParticles = player.GetComponentInChildren<ParticleSystem> ();
		callUI = mainCamera.GetComponent<Call_UI> ();

		UpperBodyTurn = GameObject.FindWithTag ("UpperBodyTurn");

		audioSource = GetComponent<AudioSource> ();
		note = 0;
		ifFinish = false;
		birdsPresent = false;

		matrixSize = 10;
		transitionMatrix = new int[matrixSize,matrixSize];
		tripletList = new List<int[]> ();
		ParseTransitionMatrix ();

		currentActiveTripletIndex = 0;
		currentActiveNoteIndex = 0;
		currentMaxTripletIndex = 0;
		prevIndex = 0;
	}

	// Update is called once per frame
	void Update () {

		Cheat ();
		SoundLogic ();
		CheckIfSoundsPlaying ();
	}


	void Cheat(){
		if (Input.GetKeyDown(KeyCode.Space)){
			int currentBirds = birdGenerator.birdList.Count;
			if (currentLastNote == null) {
				currentLastNote = 0;
			}
			for (int i = 0; i < 4-currentBirds; i++){
				GenerateTriplet(currentLastNote);
			}
			GameWin();
		}
	}

	void CheckIfSoundsPlaying(){
		bool soundPlaying = false;
		for (int i = 0; i < shortsounds.Length; i++) {
			if (shortsounds [i].isPlaying) {
				soundPlaying = true;
				break;
			}
		}
		if (!soundPlaying) {
			for (int j = 0; j < soundsources.Length; j++) {
				if (soundsources [j].isPlaying) {
					soundPlaying = true;
					break;
				}
			}
		}
		if (!soundPlaying) {
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
		callUI.ActivateBirdUIButton (currentMaxTripletIndex);
		//StartCoroutine (birdGenerator.birdList[birdGenerator.birdList.Count-1].bird.GetComponent<ClickBird>().playSong (true));
	}

	int GenerateNote(int lastNote){
		int note = 0;
		int markovOrRandom = 0;
		markovOrRandom = UnityEngine.Random.Range (0, 10);
		if (markovOrRandom < 3) {
			note = UnityEngine.Random.Range (0, 10);
		} else {
			int noteRange = 0;
			for (int i = 0; i < matrixSize; i++) {
				noteRange += transitionMatrix [lastNote, i];
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
		}

		return note;
	}

	public void ResetPlaceInSong(bool onNewBird){
		currentActiveTripletIndex = 0;
		currentActiveNoteIndex = 0;
		//ButtonImage [i].gameObject.GetComponent<Animator> ().SetBool ("ifRight", false);
		int count = birdGenerator.birdList.Count;
		if (onNewBird) {
			count -= 1;
		}
		for (int k = 0; k < count; k++){
			birdGenerator.birdList[k].GetComponent<ClickBird>().SetHappyParticles(false, 0, onNewBird);
		}
	}

	void GameWin(){
		ifFinish = true;
		characterani.SetBool ("iscorrect", true);
		endBk.SetBool ("isSunrise", true);
		treeLight.SetBool ("isTreelight", true);
		callUI.InitiateGameEndFadeOut ();
		StartCoroutine (GameWinSong());
	}

	IEnumerator GameWinSong(){
		yield return new WaitForSeconds (1.5f);
		for (int j = 0; j < birdGenerator.birdList.Count; j++) {
			ClickBird clickBird = birdGenerator.birdList [j].GetComponent<ClickBird> ();
			yield return StartCoroutine(clickBird.playSong(false));
		}

		birdGenerator.AddFinalBlueBird ();
		yield return new WaitForSeconds(1.5f);
		characterani.enabled = false;
		iTween.RotateAdd (UpperBodyTurn, new Vector3(-25f,0f,0f), 4f);
		yield return new WaitForSeconds(2f);
		GameObject.FindWithTag ("OcarinaCanvas").GetComponent<Animator> ().SetTrigger("fadeouttime");
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene ("EndMenu");

	}


	void SoundKeyPressed(int i){

		playerParticles.Emit (1);
		playerParticles.Stop ();

		prevIndex = i;

		//if (!shortsounds [i].isPlaying) {
			shortsounds [i].volume = 1.0f;
			shortsounds[i].Play();

			characterani.SetBool ("isplaying", true);

			if (i > 4) {
				activeButton (i - 5, true);
			} else {
				activeButton (i, true);
			}

			if (!birdsPresent) {
				int noteSeed = UnityEngine.Random.Range (0, matrixSize);
				GenerateTriplet (noteSeed);
				birdsPresent = true;
			}
			else if (!ifFinish) {
				if (i == tripletList[currentActiveTripletIndex][currentActiveNoteIndex]){
					//ButtonImage [i].gameObject.GetComponent<Animator> ().SetBool ("ifRight", true);
					birdGenerator.birdList [currentActiveTripletIndex].GetComponent<ClickBird> ().SetHappyParticles (true, currentActiveNoteIndex, false);
					if (currentActiveNoteIndex < 2) {
						currentActiveNoteIndex += 1;
					} else {
						currentActiveNoteIndex = 0;
						currentActiveTripletIndex += 1;
						if (currentActiveTripletIndex > currentMaxTripletIndex) {
							if (currentActiveTripletIndex < maxSongLength) {
								currentMaxTripletIndex += 1;
								GenerateTriplet (currentLastNote);
								ResetPlaceInSong (true);
							} else {
								GameWin ();
							}
						}
					}
				} else {
					birdGenerator.birdList [currentActiveTripletIndex].GetComponent<ClickBird> ().EmitConfusedParticle ();
					ResetPlaceInSong (false);
				} 
			}
		//}
	}


	void activeButton(int index, bool held){
		ButtonImage[index].gameObject.SetActive(held);
	}

	void SoundLogic(){
		for (int i = 0; i < music_keys.Length; i++) {
			
			//toggle octave
			if (Input.GetKey (KeyCode.J)) { //higher octave
				activeButton(5,true);

				//make sure all the sounds from regular octave are not playing
				for(int j = 0; j < music_keys.Length; j++){
					soundsources [j].Stop ();
				}

				if (Input.GetKey (music_keys [i])) {
					if (!soundsources[i+5].isPlaying) {
						SoundKeyPressed (i + 5);
						soundsources [i + 5].Play ();
					}
				} else { //keys are no longer held
					activeButton(i,false);
					soundsources [i + 5].Stop ();
				}

			} else { //regular octave
				activeButton(5,false);

				//make sure all the sounds from octave higher are not playing
				for(int j = 0; j < music_keys.Length; j++){
					soundsources [j + 5].Stop ();
				}

				if (Input.GetKey (music_keys [i])) {
					if (!soundsources [i].isPlaying) {
						SoundKeyPressed (i);
						soundsources [i].Play ();
					}
				} else { //keys are no longer held
					activeButton(i, false);
					soundsources [i].Stop ();
				}
			}


		}

	}

/*	void SoundLogic()
	{

		currentlyplaying = false;

		for (int i = 0; i < shortsounds.Length/2; i++) {
			if (!currentlyplaying) {
				if (Input.GetKey (KeyCode.J)) {
					activeButton (5,true);
					if (Input.GetKeyDown (music_keys [i])) {
						SoundKeyPressed (i+5);
						StartCoroutine (fadeAudio (i, true));
						//soundsources [i+5].Play ();
						currentlyplaying = true;
					}
				}  else if (Input.GetKeyDown (music_keys [i])) {
					SoundKeyPressed (i);
					StartCoroutine (fadeAudio (i, true));
					soundsources [i].Play ();
					StartCoroutine (fadeAudio (i, true));
					currentlyplaying = true;
				}
			}
			if (Input.GetKeyUp (music_keys [i])) {
				activeButton(i, false);
				if (Input.GetKey (KeyCode.J)) {
					StartCoroutine (fadeAudio (i+5, false));
				}  else {
					StartCoroutine (fadeAudio (i, false));
				}
				currentlyplaying = false;
			} 
			if(Input.GetKeyUp(KeyCode.J)){
				activeButton(5,false);
			}
		}
	}
*/
	IEnumerator fadeAudio(int i, bool fadein){
		float startVolume;
		float endVolume;
		float duration;
		float startTime = Time.time;
		if (fadein) {
			soundsources [i].Play ();
			startVolume = 0.0f;
			endVolume = defaultvolume;
			duration = 0.06f;
		}  else { //fadeout
			startVolume = defaultvolume;
			endVolume = 0.0f;
			duration = 0.05f;
		}
		float inverseDuration = 1.0f / duration;
		float lerpFactor = 0.0f;
		while (lerpFactor <= 1.0f && ((fadein && soundsources[i].volume < 1.0f) ||
			!fadein && soundsources[i].volume > 0.0f)) {
			lerpFactor += Time.deltaTime / duration;
			soundsources [i].volume = Mathf.Lerp (startVolume, endVolume, lerpFactor);
			yield return null;
		}

		if (!fadein) {
			soundsources [i].volume = 0.0f;
			soundsources [i].Stop ();
		}  else {
			soundsources [i].volume = defaultvolume;
		}
	}
}
