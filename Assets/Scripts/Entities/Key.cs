using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	public AudioClip keyCollectSound;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Inventory> ()) {
			AudioSource.PlayClipAtPoint (keyCollectSound, Camera.main.transform.position);
			Destroy (this.gameObject);
		}
	}
}
