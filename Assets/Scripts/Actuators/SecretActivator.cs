using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretActivator : MonoBehaviour {

	public GameObject mask1;
	public GameObject mask2;

	public AudioClip characterSound;
	public AudioClip enterSound;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player> ()) {
			StartCoroutine (OpenMasks ());
		}
	}

	IEnumerator OpenMasks() {
		StartCoroutine(PlayAudio ());
		resetMask (mask1);
		resetMask (mask2);
		yield return OpenMask (mask1);
		yield return OpenMask (mask2);
	}

	void resetMask(GameObject mask) {
		mask.transform.localScale = new Vector3 (
			3.5f,
			mask.transform.localScale.y,
			mask.transform.localScale.z
		);
		mask.transform.localPosition = new Vector3 (
			0.0f,
			mask.transform.localPosition.y,
			mask.transform.localPosition.z
		);
	}

	IEnumerator OpenMask(GameObject mask) {
		while (mask.transform.localScale.x >= 0.0f) {
			mask.transform.localScale = new Vector3 (
				mask.transform.localScale.x - 0.1f,
				mask.transform.localScale.y,
				mask.transform.localScale.z
			);
			mask.transform.localPosition += 0.16f * Vector3.right;
			yield return new WaitForSeconds (0.1f);
		}
	}

	IEnumerator PlayAudio() {
		AudioSource.PlayClipAtPoint (enterSound, Camera.main.transform.position);
		yield return new WaitForSeconds (0.4f);
		for (int i = 0; i < 39; i++) {
			AudioSource.PlayClipAtPoint (characterSound, Camera.main.transform.position);
			yield return new WaitForSeconds (0.16f);
		}
	}
}
