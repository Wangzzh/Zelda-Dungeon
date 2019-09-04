using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMover : MonoBehaviour {

	public float moveSpeed;

	// Use this for initialization
	void Start () {
		StartCoroutine (DragonMove());
	}

	void OnEnable()
	{
		StartCoroutine(DragonMove ());
	}

	IEnumerator DragonMove()
	{
		while (true) {
			for (float i = 0; i < 2f; i += moveSpeed * Time.deltaTime) { 
				transform.position = transform.position - moveSpeed * transform.right * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}
			// wander
			for (float i = 0; i < 0.5f; i += moveSpeed * Time.deltaTime) { 
				transform.position = transform.position + moveSpeed * transform.right * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}
			for (float i = 0; i < 0.5f; i += moveSpeed * Time.deltaTime) { 
				transform.position = transform.position - moveSpeed * transform.right * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}

			// go right
			for (float i = 0; i < 2f; i += moveSpeed * Time.deltaTime) { 
				transform.position = transform.position + moveSpeed * transform.right * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}

			// wander
			for (float i = 0; i < 0.5f; i += moveSpeed * Time.deltaTime) { 
				transform.position = transform.position + moveSpeed * transform.right * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}
			for (float i = 0; i < 0.5f; i += moveSpeed * Time.deltaTime) { 
				transform.position = transform.position - moveSpeed * transform.right * Time.deltaTime;
				yield return new WaitForSeconds (Time.deltaTime);
			}
		}
	}
}
