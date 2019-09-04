using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidder : MonoBehaviour {
	public RoomEnemyManager roomEnemyManager;
	public bool hidden;
	public AudioClip keyRevealMusic;

	private Vector3 originalPos;
	// Use this for initialization
	void Start () {
		if (hidden) {
			originalPos = transform.position;
			transform.position = new Vector3 (100f, 100f, 100f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hidden && roomEnemyManager && roomEnemyManager.AllEnemyDestroyed () ) {
			transform.position = originalPos;
			hidden = false;
			AudioSource.PlayClipAtPoint (keyRevealMusic, Camera.main.transform.position);
		}
	}
}
