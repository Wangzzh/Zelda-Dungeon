using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnemyDetector : MonoBehaviour {
	public RoomEnemyManager roomEnemyManager;
	public AudioClip doorAudio;

	Animator animator;
	bool notOpenDoor;
	bool closeDoor;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		notOpenDoor = true;
		closeDoor = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (roomEnemyManager.AllEnemyDestroyed () && notOpenDoor) {
			animator.SetTrigger ("OpenDoor");
			GetComponent<BoxCollider> ().size = new Vector3 (0.5f, 0.5f, 0f);
			GetComponent<BoxCollider> ().transform.position += 0.25f * Vector3.up;
			notOpenDoor = false;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		Mover playerMover = other.gameObject.GetComponent<Mover> ();
		if (playerMover && playerMover.direction == Direction.LEFT) {
			other.gameObject.transform.position += 1.1f * Vector3.left;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Player> () && !roomEnemyManager.AllEnemyDestroyed () && !closeDoor) {
			animator.SetTrigger ("CloseDoor");
			AudioSource.PlayClipAtPoint (doorAudio, Camera.main.transform.position);
			closeDoor = true;
		
		}
	}
}
