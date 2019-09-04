using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour {

	public GameObject player;
	public float moveSpeed;
	public bool pushable;

	private int playerLayer = 1 << 10;
	private Vector3 originalPos;
	private Rigidbody playerRB;
	private Mover playerMover;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
		playerRB = player.GetComponent<Rigidbody> ();
		playerMover = player.GetComponent<Mover> ();
		pushable = true;
	}

	public AudioClip pushSound;

	// Update is called once per frame
	void Update () {
		if (pushable) {
			int playerDirection = DetectPlayerDirection ();

			if (playerDirection != Direction.NONE) {
				
				if (playerRB.velocity != Vector3.zero && Direction.IsOpposite(playerDirection, playerMover.direction)) {
					if (transform.position == originalPos 
						|| Direction.GetVector3ByDirection(playerMover.direction) -  Vector3.Normalize(transform.position - originalPos) == Vector3.zero)
						transform.position = transform.position + moveSpeed * Direction.GetVector3ByDirection(playerMover.direction) * Time.deltaTime;
				}
				if (Vector3.Distance (transform.position, originalPos) >= 1) {
					pushable = false;
					AudioSource.PlayClipAtPoint (pushSound, Camera.main.transform.position);
				}
			}
		}

		if (!Room.IsInRoom (transform)) {
			transform.position = originalPos;
			pushable = true;
		}
	}

	int DetectPlayerDirection()
	{
		if (Physics.Raycast (transform.position - transform.up * 0.25f, transform.right, 0.6f, playerLayer)) {
			return Direction.RIGHT;
		}
		if (Physics.Raycast (transform.position - transform.up * 0.25f, - transform.right, 0.6f, playerLayer)) {
			return Direction.LEFT;
		}
		if (Physics.Raycast (transform.position, - transform.up, 0.6f, playerLayer)) {
			return Direction.DOWN;
		}
		if (Physics.Raycast (transform.position, transform.up, 0.6f, playerLayer)) {
			return Direction.UP;
		}
		Debug.DrawLine (transform.position, transform.position - transform.right * 0.6f);
		return Direction.NONE;
	}


}
