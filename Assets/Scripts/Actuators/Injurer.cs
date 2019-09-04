using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Injurer : MonoBehaviour {
	
	private Health health;
	private Repeller repeller;
	private Mirror mirror;

	public float invincibilityTime;
    public bool invulnerable;
	public bool fixedBehavior;
	public Text text;

	public AudioClip hitEnemy;
	public AudioClip hitEnemyToDeath;
	public AudioClip bladeTrapMusic;
	public AudioClip enemyAttackMusic;

	void Start() {
		mirror = GetComponent<Mirror> ();
		health = GetComponent<Health> ();
		repeller = GetComponent<Repeller> ();
        invulnerable = false;
	}

    public void GetInvincibility()
    {
        invulnerable = true;
        StartCoroutine (RemoveInvincibility());
    }

    IEnumerator RemoveInvincibility()
    {
        yield return new WaitForSeconds (invincibilityTime);
        invulnerable = false;
    }

	int calculateRepelDir(Vector3 otherPosition)
	{
		Vector3 deltaTrans = otherPosition - transform.position;
		if (Mathf.Abs (deltaTrans.x) > Mathf.Abs (deltaTrans.y)) {
			if (deltaTrans.x < 0) {
				return Direction.RIGHT;
			}	
			return Direction.LEFT;
		}
		else {
			if (deltaTrans.y < 0) {
				return Direction.UP;
			}
			return Direction.DOWN;
		}
	}

	void OnTriggerStay(Collider other) {
		
		if (other.gameObject.GetComponent<AttackRegion> () && mirror.isMirror != other.gameObject.GetComponent<AttackRegion> ().isMirror)
			return;
		
		if (other.gameObject.GetComponent<AttackRegion> () && !invulnerable) {
			AttackRegion attackRegion = other.gameObject.GetComponent<AttackRegion> ();

			if (attackRegion.attacker == AttackRegion.ENEMY && GetComponent<Player> () && !GetComponent<Player> ().isDead) {
				if (other.transform.parent && other.transform.parent.gameObject.GetComponent<BladeTrapMover> ()) {
					AudioSource.PlayClipAtPoint (bladeTrapMusic, Camera.main.transform.position);
				}
				else if (other.transform.parent && other.transform.parent.gameObject.GetComponent<Enemy> ()){
					AudioSource.PlayClipAtPoint (enemyAttackMusic, Camera.main.transform.position);
				}
                
				health.Hurt (attackRegion.damage);
				int repelDir = calculateRepelDir (other.gameObject.GetComponent<Transform>().position);
				if (repeller) {
					repeller.Repel (repelDir);
				}
				GetInvincibility ();
			} else if (attackRegion.attacker == AttackRegion.PLAYER && GetComponent<Enemy> ()) {


				if (health.health == 1 && attackRegion.damage != 0)
					AudioSource.PlayClipAtPoint (hitEnemyToDeath, Camera.main.transform.position);
				else
					AudioSource.PlayClipAtPoint (hitEnemy, Camera.main.transform.position);


				int repelDir;
				if (attackRegion.damage == 0 && health.maxHealth != 1) {
					// Boomerang attacks the enemies with health greater than 1
					repelDir = Direction.NONE;
					StartCoroutine (fixedMoving());
				} else {
					if (attackRegion.damage == 0) attackRegion.damage = 1;

					health.Hurt (attackRegion.damage);
					GetInvincibility ();
					repelDir = calculateRepelDir (other.gameObject.GetComponent<Transform> ().position);
					if (repeller) {
						repeller.Repel (repelDir);
					}

				}
			}
		}
	}

	void Update()
	{
		if (text && text.enabled)
			invulnerable = true;
	}

	IEnumerator fixedMoving()
	{
		if (!fixedBehavior) {
			fixedBehavior = true;
			yield return new WaitForSeconds (1f);
			fixedBehavior = false;
		}
		yield return null;
	}
}
