using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionActivator : MonoBehaviour {

	public int direction;
	TransitionManager transitionManager;

	void Start() {
		transitionManager = GameObject.Find ("TransitionManager").GetComponent<TransitionManager> ();
	}

	void OnCollisionEnter(Collision other) {
		if (other.collider.gameObject.GetComponent<Player> ()) {
			transitionManager.Transition (direction);
		}
	}
}
