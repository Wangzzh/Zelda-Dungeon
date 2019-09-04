using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public AudioClip mapSound;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Inventory> ()) {
			AudioSource.PlayClipAtPoint (mapSound, Camera.main.transform.position);
			Destroy (this.gameObject);
		}
	}
}
