using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Call_UI : MonoBehaviour {
	private Ray ray;
	private RaycastHit hit;
	private Image[] ocarinaImages;
	public Image[] birdUIImages;
	private bool isFadein;
	private float alpha;

	public GameObject player;
	public GameObject ocarina;
	public GameObject birdUI;
	public Sprite octaveShiftedOcarina;
	public Sprite ocarinaSprite;
	public MakeSound makeSound;

	void Start()
	{
		alpha = 0.0f;
		isFadein = true;
		ocarinaImages = ocarina.GetComponentsInChildren<Image> ();
		birdUIImages = birdUI.GetComponentsInChildren<Image> ();
		makeSound =  GameObject.FindWithTag("MusicManager").GetComponent<MakeSound> ();

		SetUIAlphaToZero ();
		FadeInOcarina ();
	}

	void SetUIAlphaToZero(){
		for (int i = 0; i < birdUIImages.Length; i++) {
			birdUIImages [i].canvasRenderer.SetAlpha (0);
		}
		for (int j = 0; j < ocarinaImages.Length; j++) {
			ocarinaImages [j].canvasRenderer.SetAlpha (0);
		}
	}

	void FadeInOcarina(){
		for (int j = 0; j < ocarinaImages.Length; j++) {
			ocarinaImages [j].CrossFadeAlpha (1, 1, false);
		}
	}

	public void ActivateBirdUIButton(int index){
		birdUIImages [index].CrossFadeAlpha (1, 1, false);
	}

	public void PressBirdUIButton(int index){
		iTween.PunchScale (birdUIImages [index].gameObject, new Vector3 (1.05f, 1.05f, 1.05f), 1f);
	}

	public void InitiateGameEndFadeOut(){
		for (int i = 0; i < birdUIImages.Length; i++) {
			birdUIImages [i].CrossFadeAlpha (0, 1, false);
		}
		for (int j = 0; j < ocarinaImages.Length; j++) {
			ocarinaImages [j].CrossFadeAlpha (0, 1, false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.J)){
			ocarina.GetComponent<Image>().sprite = octaveShiftedOcarina;
		}
		else {
			ocarina.GetComponent<Image>().sprite = ocarinaSprite;
		}
	}
}
