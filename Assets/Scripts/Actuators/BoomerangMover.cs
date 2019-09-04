using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMover : MonoBehaviour {

	public float speed;
	public float maxDistance;
	public AudioClip boomerangSound;

	// Use this for initialization
	private GameObject creator;
	private bool reflect;

	private bool inSlowDown;
	private Animator animator;
	private Rigidbody rb;

	void Start () 
	{
		StartCoroutine (PlayMusic());
		if (!rb) rb = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		inSlowDown = false;
		reflect = false;
	}
	IEnumerator PlayMusic()
	{
		while (GetComponentInChildren<AttackRegion>().attacker == AttackRegion.PLAYER) {
			AudioSource.PlayClipAtPoint (boomerangSound, Camera.main.transform.position);
			yield return new WaitForSeconds (0.3f);
		}
	}

	void FixedUpdate () 
	{

		if (!inSlowDown) {
			if (!Room.IsInRoom (transform) && !reflect) {
				reflect = true;

			} else if (!reflect && Vector3.Distance (transform.position, creator.transform.position) >= maxDistance) {
				StartCoroutine (SlowDown ());
			} else if (reflect) {
				MoveTowardsCreator ();
			}
		}
	}

	IEnumerator SlowDown()
	{
		reflect = true;
		inSlowDown = true;
		animator.speed = 0.5f;
		rb.velocity = 0.1f * rb.velocity;
		yield return new WaitForSeconds (0.1f);
		rb.velocity = -rb.velocity;
		yield return new WaitForSeconds (0.1f);
		animator.speed = 1f;
		inSlowDown = false;
	}

	void MoveTowardsCreator()
	{
		if (creator)
			SetVelocity (Vector3.Normalize(creator.gameObject.transform.position - transform.position));
	}

	public void SetCreator(GameObject _creator)
	{
		creator = _creator;
	}

	public void SetVelocity(Vector3 dirVelocity)
	{
		if (rb)
			rb.velocity = dirVelocity * speed;
		else {
			rb = GetComponent<Rigidbody> ();
			rb.velocity = dirVelocity * speed;
		}
	}
}
