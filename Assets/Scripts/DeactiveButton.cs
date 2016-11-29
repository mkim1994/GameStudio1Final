using UnityEngine;
using System.Collections;

public class DeactiveButton : MonoBehaviour {

	public void deactive()
	{
		GetComponent<Animator> ().SetBool ("ifPressed", false);
	}
}
