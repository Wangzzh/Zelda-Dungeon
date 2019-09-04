using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMover : MonoBehaviour {

	Vector3 velocity;


	public bool collisionWithWall = false;
	public float userDefinedVelocity;
	public float oneTravelTime;
	public float sleepProbability;
	bool isSleep;
	float velocityFactor = 1f;
	Animator animator;

	float roundTime;
	float reduceFactor;

	// Use this for initialization
	void Start () 
	{
		roundTime = (oneTravelTime - 0.2f) / 60f;
		reduceFactor = velocityFactor / 30f;

		isSleep = false;
		collisionWithWall = false;
		animator = GetComponent<Animator> ();
		getRandomVelocity ();	
	}

	void OnEnable()
	{
		roundTime = (oneTravelTime - 0.2f) / 60f;
		velocityFactor = 1f;
		if (animator) animator.speed = 1f;

		reduceFactor = velocityFactor / 30f;

		isSleep = false;
		collisionWithWall = false;

		StartCoroutine (BatMoving());	
	}

	void getRandomVelocity()
	{
		do {
			int randomDir = Mathf.FloorToInt (Random.Range (0f, 4f));
			velocity = Direction.GetVector3ByDirection (randomDir);

			randomDir = Mathf.FloorToInt (Random.Range (0f, 4f));
			velocity += Direction.GetVector3ByDirection (randomDir);
			velocity /= 2f;
		} while (velocity == Vector3.zero);
	}

	void generateRandomReduceSpeed()
	{
		if (Random.value < sleepProbability)
		{
			StartCoroutine (ReduceSpeed());
		}
	}


	IEnumerator BatMoving()
	{
		while (true) {
			if (!collisionWithWall && !isSleep) {
				getRandomVelocity ();
				generateRandomReduceSpeed ();
			}
			yield return new WaitForSeconds (oneTravelTime);
		}
	}

	// Update is called once per frame
	void Update () 
	{	
		if (Room.IsInRoom(transform.position + velocity * velocityFactor * userDefinedVelocity * Time.deltaTime))
			transform.position += velocity * velocityFactor * userDefinedVelocity * Time.deltaTime;
	}

	IEnumerator RecoverChangingDirection()
	{
		yield return new WaitForSeconds (0.5f * oneTravelTime);
		collisionWithWall = false;
	}

	IEnumerator RecoverSpeed()
	{
		for (float i = 0f; i < 1f; i += reduceFactor) {
			velocityFactor += reduceFactor;
			animator.speed += reduceFactor;
			yield return new WaitForSeconds (roundTime);
		}
		velocityFactor = 1f;
		animator.speed = 1f;
		isSleep = false;
	}

	IEnumerator ReduceSpeed()
	{
		isSleep = true;
		for (float i = 0f; i < 1f; i += reduceFactor) {
			velocityFactor -= reduceFactor;
			if (animator) animator.speed -= reduceFactor;
			yield return new WaitForSeconds (roundTime);
		}
		velocityFactor = 0f;
		animator.speed = 0f;
		yield return new WaitForSeconds (1f);
		StartCoroutine(RecoverSpeed ());
	}

	void OnCollisionEnter()
	{
		if (!collisionWithWall) {
			velocity *= -1;
			collisionWithWall = true;
			StartCoroutine (RecoverChangingDirection ());
		}
	}
}
