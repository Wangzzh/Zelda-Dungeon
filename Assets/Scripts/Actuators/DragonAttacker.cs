using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttacker : MonoBehaviour {

	public GameObject dragonLightBall;
	public GameObject player;
	public float ballSpeed;
	public Vector3 deltaDirection;

	// Use this for initialization
	void Start () {
		StartCoroutine (DragonAttack());
	}

	void OnEnable()
	{
		StartCoroutine (DragonAttack());
	}

	IEnumerator DragonAttack()
	{
		while (true) {
			LightBallAttack ();
			yield return new WaitForSeconds (2f);
		}
	}

	void LightBallAttack()
	{
		Vector3 direction = CalculatePlayerDirection ();
		GameObject ball1 = Instantiate (dragonLightBall, transform.position, Quaternion.identity);
		ball1.GetComponent<Rigidbody> ().velocity = ballSpeed * (direction + deltaDirection);
		GameObject ball2 = Instantiate (dragonLightBall, transform.position, Quaternion.identity);
		ball2.GetComponent<Rigidbody> ().velocity = direction * ballSpeed;
		GameObject ball3 = Instantiate (dragonLightBall, transform.position, Quaternion.identity);
		ball3.GetComponent<Rigidbody> ().velocity = ballSpeed * (direction - deltaDirection);
	}
		
	Vector3 CalculatePlayerDirection()
	{
		return Vector3.Normalize(player.transform.position - transform.position);	
	}

	// Update is called once per frame
	void Update () {
			
	}
}
