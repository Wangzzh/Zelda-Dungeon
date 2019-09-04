using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player> ()) {
			Destroy (this.gameObject);
		}
	}
}
