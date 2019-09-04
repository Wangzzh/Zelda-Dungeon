using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracer : MonoBehaviour {

	public Player player;
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPosition = player.transform.position;
		int roomX = ((int)playerPosition.x) / 16;
		int roomY = ((int)playerPosition.y) / 11;
		if (roomY < 0)
			roomY = 0;
		// Debug.Log ("In room " + roomX.ToString () + " " + roomY.ToString ());
		float imageX = -16.1f + 32.1f * (float)roomX;
		float imageY = -119.3f + 15.85f * (float)roomY;
		GetComponent<RectTransform>().localPosition = new Vector3 (imageX, imageY, 0f);
	}
}
