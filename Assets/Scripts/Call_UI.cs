using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Call_UI : MonoBehaviour {
	private Ray ray;
	private RaycastHit hit;
	private Image[] images;
	private bool isFadein;
	private float alpha;

	public GameObject player;
	public GameObject canvas;
	public MakeSound makeSound;

	void Start()
	{
		alpha = 0.0f;
		isFadein = true;
		images = canvas.GetComponentsInChildren<Image> ();
		makeSound =  GameObject.FindWithTag("MusicManager").GetComponent<MakeSound> ();
	}
	// Update is called once per frame
	void Update () {
		if(isFadein && alpha <= 1)
		{
			alpha += 0.01f;
			for (int i = 0; i < images.Length; i++) {
				images [i].color = new Color(1,1,1,alpha);
			}	
		}

		if (alpha >= 1) {
			isFadein = false;
		}
	

		if (makeSound.ifFinish == true) {

			alpha -= 0.01f;
			for (int i = 0; i < images.Length; i++) {
				images [i].color = new Color(1,1,1,alpha);

			}

		}
	
	
	}
}
