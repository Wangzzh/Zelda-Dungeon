using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapIndicator : MonoBehaviour {

	public Inventory inventory;
	Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		image.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (!image.enabled && inventory.hasMap)
			image.enabled = true;
	}
}
