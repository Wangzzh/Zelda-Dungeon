using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDetecter : MonoBehaviour {
	public GameController gameController;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Player> ()) {
			gameController.StartMirrorMode ();
		}
	}
}
