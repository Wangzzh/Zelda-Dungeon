using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalIndicator : MonoBehaviour {

	public Inventory inventory;
	Image image;
	BlinkSynchronizer blinker;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		image.enabled = false;
		blinker = GetComponent<BlinkSynchronizer> ();
		blinker.synchronized = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (inventory.hasCompass && blinker.synchronized == false)
			blinker.synchronized = true;
	}
}
