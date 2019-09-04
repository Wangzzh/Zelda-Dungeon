using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdate : MonoBehaviour {

	public Inventory inventory;

	public Text bombText;
	public Text rupeeText;
	public Text keyText;

	void Update() {
		bombText.text = inventory.numBombs.ToString ();
		rupeeText.text = inventory.numRupees.ToString ();
		keyText.text = inventory.numKeys.ToString ();
	}
}
