using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour {
	
	public float throwProb;
	public GameObject boomerang;
	public bool inThrowing;
	public bool isPlayer;

	private bool boomerangLeaver;
	private GameObject myBoomerang;
	private Animator animator;
	private GameObject parentBoomer;
	// Use this for initialization
	void Start () 
	{
		inThrowing = false;
		animator = GetComponent<Animator> ();
	}

	void OnDisable()
	{
		if (myBoomerang) {
			inThrowing = false;
			if (!isPlayer) animator.speed = 1f;
			Destroy (myBoomerang);
		}
	}

	public void ThrowBoomerang(int curDir, int attackerType)
	{

		if (Random.value < throwProb && animator) {
			inThrowing = true;
			myBoomerang = Instantiate(boomerang, transform.position, Quaternion.identity);
			myBoomerang.GetComponent<BoomerangMover>().SetCreator (gameObject);
			myBoomerang.GetComponent<BoomerangMover>().SetVelocity (Direction.GetVector3ByDirection(curDir));
			myBoomerang.GetComponentInChildren<AttackRegion> ().attacker = attackerType;

			if (!isPlayer)
				animator.speed = 0f;
			else 
				myBoomerang.GetComponentInChildren<AttackRegion> ().damage = 0;
			
			StartCoroutine (WaitLeaving());
		}

	}

	IEnumerator WaitLeaving()
	{
		boomerangLeaver = false;
		yield return new WaitForSeconds (0.5f);
		boomerangLeaver = true;
	}

	void OnTriggerStay(Collider other)
	{	
		if (other.gameObject.transform.parent)
			parentBoomer = other.gameObject.transform.parent.gameObject;
		if (boomerangLeaver && parentBoomer && parentBoomer == myBoomerang && animator) {
			inThrowing = false;
			if (!isPlayer) animator.speed = 1f;
			Destroy (parentBoomer);
		}
	}
}
