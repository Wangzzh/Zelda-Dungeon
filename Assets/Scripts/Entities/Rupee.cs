using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rupee : MonoBehaviour {

	public AudioClip rupeeCollectSound;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Inventory> ()) {
			AudioSource.PlayClipAtPoint (rupeeCollectSound, Camera.main.transform.position);
			Destroy (this.gameObject);
		}
	}
}
