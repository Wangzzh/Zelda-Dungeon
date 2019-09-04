using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallMasterMover : MonoBehaviour {

	public float moveSpeed;
	public AudioClip wallMasterMusic;

	private Animator animator;
	private bool disappeared;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		disappeared = false;
		StartCoroutine (Move());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Move()
	{
		// go right
		AudioSource.PlayClipAtPoint(wallMasterMusic, Camera.main.transform.position);
		for (float i = 0; i < 1f; i += moveSpeed * Time.deltaTime) { 
			transform.position = transform.position + moveSpeed * transform.right * Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		// go up
		for (float i = 0; i < 3f; i += moveSpeed * Time.deltaTime) { 
			transform.position = transform.position + moveSpeed * transform.up * Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		// go left
		for (float i = 0; i < 1.5f; i += moveSpeed * Time.deltaTime) { 
			transform.position = transform.position - moveSpeed * transform.right * Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		disappeared = true;
		yield return new WaitForSeconds (2f);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Player> ()) {
			// Animation for wall master
			animator.SetTrigger("catch_player");
			StartCoroutine(MovePlayer(other.gameObject));
		}
	}

	IEnumerator MovePlayer(GameObject player)
	{
		player.GetComponent<Mover> ().movable = false;
		player.GetComponent<Health> ().Hurt (1);
		player.transform.position = new Vector3(39f, 6f, 0f);
		while (!disappeared)
			yield return new WaitForSeconds (Time.deltaTime);
		yield return new WaitForSeconds (0.5f);

		Camera.main.transform.position = new Vector3 (39.5f, 7f, -10f);
		player.GetComponent<Mover> ().movable = true;
	}

}
