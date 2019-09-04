using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour {

	Inventory inventory;

	public GameObject unlockedLeft;
	public GameObject unlockedRight;
	public GameObject unlockedUpLeft;
	public GameObject unlockedUpRight;

	bool hasUnlockedUpLeft;
	bool hasUnlockedUpRight;

	public AudioClip unlockSound;

	// Use this for initialization
	void Start () {
		inventory = GetComponent<Inventory> ();
		hasUnlockedUpLeft = false;
		hasUnlockedUpRight = false;
	}

	void OnCollisionEnter(Collision collision) {
		GameObject collisionObject = collision.collider.gameObject;
		if (collisionObject.GetComponent<LockedDoor> () != null) {
			if (hasUnlockedUpLeft) { 
				// Debug.Log ("Unlocking right! Left unlocked");
				LockedDoor lockedDoor = collisionObject.GetComponent<LockedDoor> ();
				if (lockedDoor.lockedDirection == Direction.UP_RIGHT) {
					GameObject newDoor = Instantiate (unlockedUpRight, lockedDoor.transform.position, lockedDoor.transform.rotation);
					newDoor.transform.SetParent (collisionObject.transform.parent);
					Destroy (collisionObject);
					hasUnlockedUpLeft = false;
					return;
				}
			}
			if (hasUnlockedUpRight) {
				// Debug.Log ("Unlocking left! Right unlocked!");
				LockedDoor lockedDoor = collisionObject.GetComponent<LockedDoor> ();
				if (lockedDoor.lockedDirection == Direction.UP_LEFT) {
					GameObject newDoor = Instantiate (unlockedUpLeft, lockedDoor.transform.position, lockedDoor.transform.rotation);
					newDoor.transform.SetParent (collisionObject.transform.parent);
					Destroy (collisionObject);
					hasUnlockedUpRight = false;
					return;
				}
			}
			if (inventory.numKeys >= 1) {
				LockedDoor lockedDoor = collisionObject.GetComponent<LockedDoor> ();
				if (lockedDoor.lockedDirection == Direction.UP_LEFT) {
					// Debug.Log ("Unlocking left!");
					GameObject newDoor = Instantiate (unlockedUpLeft, lockedDoor.transform.position, lockedDoor.transform.rotation);
					newDoor.transform.SetParent (collisionObject.transform.parent);
					Destroy (collisionObject);
					inventory.numKeys--;
					hasUnlockedUpLeft = true;
					AudioSource.PlayClipAtPoint (unlockSound, Camera.main.transform.position);
				} else if (lockedDoor.lockedDirection == Direction.UP_RIGHT) {
					// Debug.Log ("Unlocking right!");
					GameObject newDoor = Instantiate (unlockedUpRight, lockedDoor.transform.position, lockedDoor.transform.rotation);
					newDoor.transform.SetParent (collisionObject.transform.parent);
					Destroy (collisionObject);
					inventory.numKeys--;
					hasUnlockedUpRight = true;
					AudioSource.PlayClipAtPoint (unlockSound, Camera.main.transform.position);
				} else if (lockedDoor.lockedDirection == Direction.LEFT) {
					GameObject newDoor = Instantiate (unlockedLeft, lockedDoor.transform.position, Quaternion.identity);
					newDoor.transform.SetParent (collisionObject.transform.parent);
					Destroy (collisionObject);
					inventory.numKeys--;
					AudioSource.PlayClipAtPoint (unlockSound, Camera.main.transform.position);
				} else if (lockedDoor.lockedDirection == Direction.RIGHT) {
					GameObject newDoor = Instantiate (unlockedRight, lockedDoor.transform.position, Quaternion.identity);
					newDoor.transform.SetParent (collisionObject.transform.parent);
					Destroy (collisionObject);
					inventory.numKeys--;
					AudioSource.PlayClipAtPoint (unlockSound, Camera.main.transform.position);
				}
			}
		}
	}
}
