using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelMover : MonoBehaviour {
	public float moveSpeed;
	public float changeDirectionPoss;
	public int backgroundLayer;
	public float waitingTime;

	private Vector3 velocity;

	private int curDir;
	private bool inCollision;
	// Use this for initialization

	void Start () 
	{
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
			if (!inCollision) {

				float moveTileNum = Mathf.Ceil (Random.Range (0, 3));

				curDir = RandomDirVelo.GetPossibleNewDir (transform);
				velocity = Direction.GetVector3ByDirection (curDir);

				for (float i = 0; i < moveTileNum; i += Time.deltaTime * moveSpeed) {
					yield return new WaitForFixedUpdate ();
				}
				velocity = Vector3.zero;
				transform.position = RandomDirVelo.Round2NearestPosFullTile (transform);

				yield return new WaitForSeconds (waitingTime);
			}
		}
	}


	void FixedUpdate()
	{
		transform.position = transform.position + velocity * moveSpeed * Time.deltaTime;
	}


	IEnumerator Detour (GameObject collisionGameObject)
	{
		inCollision = true;

		transform.position = RandomDirVelo.Round2NearestPosFullTile (transform);
		curDir = RandomDirVelo.GetPossibleNewDir (transform);
		velocity = Direction.GetVector3ByDirection (curDir);

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
