using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinker : MonoBehaviour {

	Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		StartCoroutine (Blinking());
	}

	IEnumerator Blinking() {
		while (true) {
			yield return new WaitForSeconds (0.5f);
			image.enabled = false;
			yield return new WaitForSeconds (0.5f);
			image.enabled = true;
		}
	}
}
