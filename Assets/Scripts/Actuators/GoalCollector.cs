using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollector : MonoBehaviour {

	public bool hasGoal;
	Mover mover;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		hasGoal = false;
		mover = GetComponent<Mover> ();
		rb = GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Goal> ()) {
			hasGoal = true;
			mover.movable = false;
			rb.velocity = Vector3.zero;
			gameObject.GetComponent<Injurer> ().invulnerable = true;
		}
	}
}
