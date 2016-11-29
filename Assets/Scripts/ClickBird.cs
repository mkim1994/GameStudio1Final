using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickBird : MonoBehaviour {
	private Ray ray;
	private RaycastHit hit;
	public GameObject soundManager;
	private MakeSound makeSound;
	private List<int[]> song;
	private AudioSource audioSource;
	private AudioClip[] sounds;

	public GameObject bird;
	public float timeBetweenBirdNotes; 
	public float timeBetweenFades; //should always be shorter than timeBetweenBirdNotes
	public float birdSongDelayForNewTriplet;
	// Update is called once per frame

	void Start(){
		soundManager = GameObject.FindWithTag ("MusicManager");
		makeSound = soundManager.GetComponent<MakeSound> ();
		audioSource = bird.GetComponent<AudioSource> ();
		sounds = makeSound.sounds;

	}
	void Update () {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0)) {
			if(Physics.Raycast(ray, out hit)) //Physics.Raycast(someRay, out someHit)
			{
				if (hit.collider.gameObject == bird) {
					StartCoroutine (playSong (false));
				}
			}	
		}
	}

	public IEnumerator playSong(bool initialWait){
		if (initialWait) {
			yield return new WaitForSeconds(birdSongDelayForNewTriplet);
		}
		song = makeSound.tripletList;
		for (int i = 0; i < song.Count; i++) {
			for (int j = 0; j < 3; j++) {
				int note = song [i] [j];
				if (note >= 5) {
					audioSource.pitch = 2;
				} else {
					audioSource.pitch = 1;
				}
				audioSource.volume = 1.0f;
				audioSource.clip = sounds [note%5];
				audioSource.Play ();

				float startVolume = audioSource.volume;
				float duration = 0.25f;
				float inverseDuration = 1.0f / duration;
				float lerpFactor = 0.0f;

				yield return new WaitForSeconds(timeBetweenFades);

				/*fade */
				/*for (int t = 9; t > 0; t--) {
					audioSource.volume = t * 0.1f;
					yield return new WaitForSeconds (timeBetweenBirdNotes);
				}*/
				while (lerpFactor <= 1.0f) {
					audioSource.volume = Mathf.Lerp (startVolume, 0.0f, lerpFactor);
					lerpFactor = lerpFactor + Time.deltaTime * inverseDuration;
					yield return new WaitForSeconds (timeBetweenBirdNotes);
					//yield return 1.0f;
				}

				/* fade */
				audioSource.volume = 0f;
				audioSource.Stop ();
			}
		}
	}

}
