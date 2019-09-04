using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;

public class FlyingAndDestroy : MonoBehaviour {

	AttackRegion myAttackRegion;
	public GameObject hitDestroy;
	public bool isArrow;
	// Use this for initialization


	void Start () 
	{
		myAttackRegion = GetComponent<AttackRegion>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (!Room.IsInRoom(transform)) { 
			if (hitDestroy) Instantiate (hitDestroy, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy> () && !isArrow) {
			if (hitDestroy) Instantiate (hitDestroy, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
