using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapMover : MonoBehaviour {
	private int playerLayer = 1 << 10;
	private bool trapInStatic; 
	private Vector3 velocity;

	// Use this for initialization
	private Vector3 originalPosition;

	public float trapSpeed;
	public float returnSpeed;

	public AudioClip hitPlayer;
	void Start () 
	{
		transform.position = RandomDirVelo.Round2NearestPosFullTile (transform);
		originalPosition = transform.position;
		trapInStatic = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (trapInStatic && Room.IsInRoom(transform)) {
			int playerDir = DetectPlayer ();
			if (playerDir != Direction.NONE) {
				StartCoroutine (HitInDirection(playerDir));
			}
		}
	}

	IEnumerator HitInDirection(int playerDir)
	{
		trapInStatic = false;
		velocity = Direction.GetVector3ByDirection(playerDir);

		if (Direction.IsHorizontal (playerDir)) {
			
			for (float i = 0f; i < (Room.roomWidth - 1f) / 2f; i += trapSpeed * Time.deltaTime) {
				transform.position = transform.position + velocity * trapSpeed * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}

			velocity = Direction.GetVector3ByDirection(playerDir);
			yield return new WaitForSeconds (0.2f);

			for (float i = 0f; i < (Room.roomWidth - 1f) / 2f; i += returnSpeed * Time.deltaTime) {
				transform.position = transform.position - velocity * returnSpeed * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}
					
		} else {
			for (float i = 0f; i < (Room.roomHeight - 1f) / 2f; i += trapSpeed * Time.deltaTime) {
				transform.position = transform.position + velocity * trapSpeed * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}

			velocity = Direction.GetVector3ByDirection(playerDir);
			yield return new WaitForSeconds (0.2f);

			for (float i = 0f; i < (Room.roomHeight - 1f) / 2f; i += returnSpeed * Time.deltaTime) {
				transform.position = transform.position - velocity * returnSpeed * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}

		}
		velocity = Vector3.zero;
		transform.position = originalPosition;
		trapInStatic = true;
	}

		
	int DetectPlayer()
	{
		if (Physics.Raycast (transform.position, transform.right, Room.roomWidth, playerLayer)) {
			return Direction.RIGHT;
		}
		if (Physics.Raycast (transform.position, - transform.right, Room.roomWidth, playerLayer)) {
			return Direction.LEFT;
		}
		if (Physics.Raycast (transform.position, - transform.up, Room.roomHeight, playerLayer)) {
			return Direction.DOWN;
		}
		if (Physics.Raycast (transform.position, transform.up, Room.roomHeight, playerLayer)) {
			return Direction.UP;
		}

		return Direction.NONE;
	}

	void OnCollisionEnter()
	{
		velocity = Vector3.zero;
	}
}
