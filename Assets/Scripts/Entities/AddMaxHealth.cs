using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMaxHealth : MonoBehaviour {

	public AudioClip addMaxSound;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player> ()) {
			Health health = other.gameObject.GetComponent<Health> ();
			health.addMaxHealth (2);
			AudioSource.PlayClipAtPoint (addMaxSound, Camera.main.transform.position);
			Destroy (this.gameObject);
		}
	}
}
