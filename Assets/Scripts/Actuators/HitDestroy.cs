using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDestroy : MonoBehaviour {

	public float disableTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyMySelf());
	}

	IEnumerator DestroyMySelf()
	{
		yield return new WaitForSeconds (disableTime);
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
