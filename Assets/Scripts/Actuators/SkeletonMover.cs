using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 - Down
// 1 - Up 
// 2 - Left
// 3 - Right

public class SkeletonMover : MonoBehaviour {
	public float moveSpeed;
	public float changeDirectionPoss;
	public int backgroundLayer;

	public Vector3 velocity;

	private Injurer injurer;
	private int curDir;
	private bool inCollision;
	// Use this for initialization

	void Start () 
	{
		injurer = GetComponent<Injurer> ();
		curDir = Mathf.FloorToInt (4 * Random.value);
		velocity = Direction.GetVector3ByDirection (curDir);
		inCollision = false;
	}

	// Update is called once per frame
	bool FinishingGrid()
	{
		return (Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x)) < moveSpeed * Time.deltaTime / 2f)
			&& (Mathf.Abs (transform.position.y - Mathf.Round (transform.position.y)) < moveSpeed * Time.deltaTime / 2f);
	}


	void Update () 
	{	
		if (FinishingGrid() && !inCollision) {
			transform.position = RandomDirVelo.Round2NearestPosFullTile (transform);
			curDir = RandomDirVelo.GetPossibleNewDir (transform);
			velocity = Direction.GetVector3ByDirection (curDir);
		}
	}

	void FixedUpdate()
	{
		if (!injurer.fixedBehavior)
			transform.position = transform.position + moveSpeed * velocity * Time.deltaTime;
	}


	void PursuePlayer(GameObject collisionGameObject)
	{
		Vector2 playerTFPos = collisionGameObject.GetComponent<Transform> ().position;
		int newDir = 0;
		if (Mathf.Abs (playerTFPos.x - transform.position.x) > Mathf.Abs (playerTFPos.y - transform.position.y)) {
			newDir += 2;
			if (playerTFPos.x > transform.position.x)
				newDir += 1;
		} else if (playerTFPos.y > transform.position.y) {
			newDir += 1;
		}
		curDir = newDir;
	}



	IEnumerator Detour (GameObject collisionGameObject)
	{
		inCollision = true;
		transform.position = RandomDirVelo.Round2NearestPosFullTile (transform);

		curDir = RandomDirVelo.GetPossibleNewDir (transform);
		velocity = Direction.GetVector3ByDirection (curDir);

		for (float i = 0; i < 1; i += moveSpeed * Time.deltaTime) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		
		inCollision = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("Player")) {
			//PursuePlayer (collision.gameObject);
		} else {
			StartCoroutine (Detour (collision.gameObject));
		}
	}
}
