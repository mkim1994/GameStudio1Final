using UnityEngine;
using System.Collections;

public class BirdSing : MonoBehaviour {
	private Animator anime;
	private AudioSource audioSource;
	private bool ifDone;

	public AudioClip audio;
	public bool startSing;
	// Use this for initialization
	void Start () {
		anime = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
		startSing = false;
		ifDone = false;
	}

	// Update is called once per frame
	void Update () {
		if (startSing == true && ifDone != true) {
			audioSource.PlayOneShot (audio);
			anime.SetBool ("birdsinging", true);
			ifDone = true;
		}
		if (!audioSource.isPlaying) {
			startSing = false;
			ifDone = false;
			anime.SetBool ("birdsinging", false);
		}
	}
}
