using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour {
	public GameObject attackRegion;
	public GameObject spawnAnim;
	public AudioClip bombAudio;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		StartCoroutine (Boom());	
	}

	IEnumerator Boom()
	{
		yield return new WaitForSeconds (2f);
		GameObject bombRegion = Instantiate(attackRegion, transform.position, Quaternion.identity);
		bombRegion.GetComponent<BoxCollider> ().size = new Vector3 (2.8f, 3f, 0f);
		bombRegion.GetComponent<AttackRegion> ().attacker = AttackRegion.PLAYER;
		bombRegion.GetComponent<AttackRegion> ().damage = 3;

		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		BoomAnimation ();
		yield return new WaitForFixedUpdate ();
		yield return new WaitForFixedUpdate ();
		Destroy (bombRegion);
		Destroy (this.gameObject);
	}
		
	void BoomAnimation()
	{
		AudioSource.PlayClipAtPoint (bombAudio, Camera.main.transform.position);
		AudioSource.PlayClipAtPoint (bombAudio, Camera.main.transform.position);
		AudioSource.PlayClipAtPoint (bombAudio, Camera.main.transform.position);
		Instantiate (spawnAnim, transform.position, Quaternion.identity);
		Instantiate (spawnAnim, transform.position + Vector3.up + 0.5f * Vector3.left, Quaternion.identity);
		Instantiate (spawnAnim, transform.position + Vector3.up + 0.5f * Vector3.right, Quaternion.identity);
		Instantiate (spawnAnim, transform.position + Vector3.right, Quaternion.identity);
		Instantiate (spawnAnim, transform.position + Vector3.left, Quaternion.identity);
		Instantiate (spawnAnim, transform.position - Vector3.up + 0.5f * Vector3.left, Quaternion.identity);
		Instantiate (spawnAnim, transform.position - Vector3.up + 0.5f * Vector3.right, Quaternion.identity);
	}
}
