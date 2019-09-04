using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeller : MonoBehaviour {

	public bool repelling;
	public int direction;

	public float repelSpeed;

	public float flashDeltaTime;
	public int flashTimes;


	Mover mover;
	Player player;
	Rigidbody rb;
	SpriteRenderer spriteRenderer;
	SkeletonMover skeletonMover;
	GoriyaMover goriyaMover;

	void Start() {
		repelling = false;
		rb = GetComponent<Rigidbody> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		player = GetComponent<Player> ();
		goriyaMover = GetComponent<GoriyaMover> ();
		skeletonMover = GetComponent<SkeletonMover> ();

		if (player) {
			mover = GetComponent<Mover> ();
		}
	}

	public void Repel(int repelDirection) {
		direction = repelDirection;
		if (mover)
			mover.movable = false;
		StartCoroutine (StartRepelling ());
		StartCoroutine (StopRepelling (0.25f));
		StartCoroutine (Flashing (flashDeltaTime, flashTimes));
	}

	IEnumerator StartRepelling()
	{
		yield return new WaitForFixedUpdate ();
		yield return new WaitForFixedUpdate ();
		yield return new WaitForFixedUpdate ();

		repelling = true;
	}

	IEnumerator StopRepelling(float stopTime) {
		yield return new WaitForSeconds (stopTime);
		repelling = false;
		if (mover)
			mover.movable = true;
		rb.velocity = Vector3.zero;
	}

	IEnumerator Flashing(float _flashDeltaTime, int _flashTimes) {
		for (int i = 0; i < _flashTimes; i++) {
			spriteRenderer.enabled = false;
			yield return new WaitForSeconds (_flashDeltaTime);
			spriteRenderer.enabled = true;
			yield return new WaitForSeconds (_flashDeltaTime);
		}
	}

	void Update() {
		if (repelling) {
			if (player && player.isDead)
				rb.velocity = Vector3.zero;
			else {
				if (skeletonMover) {
					if (Room.IsInRoom (rb.transform))
						skeletonMover.velocity = Direction.GetVector2ByDirection (direction) * repelSpeed;
					else
						repelling = false;
				} else if (goriyaMover) {
					if (Room.IsInRoom (rb.transform))
						goriyaMover.velocity = Direction.GetVector2ByDirection (direction) * repelSpeed;
					else
						repelling = false;
				} else {
					rb.velocity = Direction.GetVector2ByDirection (direction) * repelSpeed;
				}
			}
		}
	}


	void OnCollisionEnter()
	{
		if (skeletonMover && repelling) {
			repelling = false;
		} else if (goriyaMover && repelling) {
			repelling = false;
		}

	}
}
