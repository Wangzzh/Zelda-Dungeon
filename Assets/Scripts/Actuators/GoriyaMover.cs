using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaMover : MonoBehaviour {
	public float moveSpeed;
	public float changeDirectionPoss;

	public Animator animator;

	private int backgroundLayer = 1 << 9;
	public Vector3 velocity;
	private int curDir;
	private bool inCollision;
	private Thrower thrower;
	private Injurer injurer;
	// Use this for initialization

	void Start () 
	{
		thrower = GetComponent<Thrower> ();
		animator = GetComponent<Animator> ();
		injurer = GetComponent<Injurer> ();

		curDir = Mathf.FloorToInt (4 * Random.value);
		velocity = Direction.GetVector3ByDirection (curDir);
		inCollision = false;

	}

	void OnEnable()
	{
		StartCoroutine (move());
	}

	IEnumerator move()
	{
		while (true)  {

			if (!inCollision && thrower && !thrower.inThrowing) {

				float moveTileNum = Mathf.Ceil (Random.Range (-1, 7)) / 2f;

				curDir = RandomDirVelo.GetPossibleNewDir (transform);
				velocity = Direction.GetVector3ByDirection (curDir);
				animator.SetInteger ("direction", curDir);
				animator.speed = 1f;
				for (float i = 0; i < moveTileNum; i += Time.deltaTime * moveSpeed) {
					yield return new WaitForFixedUpdate ();
				}
				transform.position = RandomDirVelo.Round2NearestPosHalfTile (transform);
				thrower.ThrowBoomerang (curDir, AttackRegion.ENEMY);

			}
			else if (thrower && thrower.inThrowing)
				yield return new WaitForSeconds (0.2f);
			else 
				yield return new WaitForSeconds (0.2f);
		}
	}

	void FixedUpdate()
	{
		if (!thrower.inThrowing && !injurer.fixedBehavior) {
			transform.position = transform.position + velocity * moveSpeed * Time.deltaTime;
		}
	}
		

	IEnumerator Detour (GameObject collisionGameObject)
	{
		inCollision = true;
		transform.position = RandomDirVelo.Round2NearestPosHalfTile (transform);
		curDir = RandomDirVelo.GetPossibleNewDir (transform);
		velocity = Direction.GetVector3ByDirection (curDir);
		animator.SetInteger ("direction", curDir);
		animator.speed = 1f;
		inCollision = false;
		yield return null;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("Player")) {
			//PursuePlayer (collision.gameObject);
		}
		else
			StartCoroutine(Detour (collision.gameObject));
	}
		
}
