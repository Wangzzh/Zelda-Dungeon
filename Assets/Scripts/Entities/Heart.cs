using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
	public float existingTime;
	public float flashDeltaTime;

	public AudioClip heartSound;

	// Use this for initialization
	void Start () {
		StartCoroutine (flashAndDestroy());
	}

	IEnumerator flashAndDestroy()
	{
		yield return new WaitForSeconds (existingTime);
		Destroy (this.gameObject);
	}

	 
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player> ()) {
			AudioSource.PlayClipAtPoint (heartSound, Camera.main.transform.position);
			Destroy (this.gameObject);
		}
	}
}
