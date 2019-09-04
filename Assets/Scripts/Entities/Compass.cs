using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

	public AudioClip compassSound;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Inventory> ()) {
			AudioSource.PlayClipAtPoint (compassSound, Camera.main.transform.position);
			Destroy (this.gameObject);
		}
	}
}
