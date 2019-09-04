using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public int numRupees;
	public int numBombs;
	public int numKeys;

	public bool hasCompass;
	public bool hasMap;

	public Inventory inventory;

	void Start() {
		hasCompass = false;
		hasMap = false;
		if (inventory)
			numRupees = 1999;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Rupee> ()) {
			numRupees++;
			if (inventory)
				inventory.numRupees++;
		}
		if (other.gameObject.GetComponent<Key> ()) {
			numKeys++;
			if (inventory)
				inventory.numKeys++;
		}
		if (other.gameObject.GetComponent<DropBomb> ()) {
			numBombs += 3;
		}
		if (other.gameObject.GetComponent<Map> ()) {
			hasMap = true;
		}
		if (other.gameObject.GetComponent<Compass> ()) {
			hasCompass = true;
		}
	}

	public void StartMirrorMode()
	{
		numRupees = 999;
	}
}
