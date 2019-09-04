using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkSynchronizer : MonoBehaviour {

	public Image other;
	Image image;

	public bool synchronized;

	void Start() {
		image = GetComponent<Image> ();
		synchronized = false;
	}

	void Update() {
		if (synchronized) {
			image.enabled = other.enabled;
		}
	}
}
