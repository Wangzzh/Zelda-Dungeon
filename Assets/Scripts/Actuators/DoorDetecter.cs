using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetecter : MonoBehaviour {
	public BoxMover boxMover;

	private Animator animator;
	private bool isReset;
	private bool openDoor;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		isReset = true;
		openDoor = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!boxMover.pushable && !openDoor) {
			animator.SetTrigger ("open_door");
			GetComponent<BoxCollider> ().size = new Vector3 (0.5f, 0.5f, 3f);
			GetComponent<BoxCollider> ().center += 0.25f * Vector3.up;
			isReset = false;
			openDoor = true;
		} else if (!isReset && boxMover.pushable){
			animator.SetTrigger ("close_door");
			GetComponent<BoxCollider> ().size = new Vector3 (1f, 1f, 3f);
			GetComponent<BoxCollider> ().center -= 0.25f * Vector3.up;
			isReset = true;
			openDoor = false;
		}
	}

	void OnCollisionEnter(Collision other)
    {
		Mover playerMover = other.gameObject.GetComponent<Mover> ();
		if (playerMover && playerMover.direction == Direction.RIGHT) {
			other.gameObject.transform.position += 1.1f * Vector3.right;
		}
    }
}
