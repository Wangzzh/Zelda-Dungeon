using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

	public int weaponInUse;

	private Mover mover;
	private Rigidbody rb;

	private Inventory inventory;

	public bool inMirrorMode;
	public int attackerType;

	public GameObject attackRegion;
	public GameObject flyingSword;
	public GameObject arrow;
	public GameObject boomerang;
	public GameObject bomb;


	GameObject newAttackRegion;
	GameObject flySword;
	public float flyingSpeed;

	public bool inAttack;
	private Thrower thrower;

	public AudioClip swordSound;
	public AudioClip flyingSwordSound;

	private Mirror mirror;

	void Start() {
		mirror = GetComponent<Mirror> ();	
		mover = GetComponent<Mover> ();
		rb = GetComponent<Rigidbody> ();
		thrower = GetComponent<Thrower> ();
		inventory = GetComponent<Inventory> ();
 		inAttack = false;
	}

	void FlyingSwordAttack(int attackDirection)
	{
		newAttackRegion = Instantiate (flyingSword, transform.position + Direction.GetVector3ByDirection (attackDirection), Quaternion.identity);
		newAttackRegion.transform.up = -Direction.GetVector3ByDirection (attackDirection);
		newAttackRegion.GetComponent<Rigidbody> ().AddForce (flyingSpeed * Direction.GetVector3ByDirection (attackDirection));

		flySword = newAttackRegion;
		SetAttackRegionAttr (newAttackRegion.GetComponent<AttackRegion> (), attackDirection, 1);
	}

	IEnumerator DestroyAfterSeconds(GameObject gameObject, float seconds) {
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject);
	}


	void SetAttackRegionAttr(AttackRegion attackRegion, int attackDirection, int damage)
	{
		attackRegion.damage = damage;
		attackRegion.attacker = attackerType;
		attackRegion.direction = attackDirection;
	}

	void SwordAttack(int attackDirection)
	{
		newAttackRegion = Instantiate (attackRegion, transform.position + Direction.GetVector3ByDirection (attackDirection), Quaternion.identity);

		SetAttackRegionAttr (newAttackRegion.GetComponent<AttackRegion>(), attackDirection, 1);
		StartCoroutine (DestroyAfterSeconds (newAttackRegion, 0.5f));
	}

	void BowAttack(int attackDirection)
	{
		newAttackRegion = Instantiate (arrow, transform.position + Direction.GetVector3ByDirection (attackDirection), Quaternion.identity);
		newAttackRegion.transform.up = -Direction.GetVector3ByDirection (attackDirection);
		newAttackRegion.GetComponent<Rigidbody> ().AddForce (flyingSpeed * Direction.GetVector3ByDirection (attackDirection));
		SetAttackRegionAttr (newAttackRegion.GetComponent<AttackRegion>(), attackDirection, 1);

		if (inMirrorMode) {
			newAttackRegion.GetComponent<AttackRegion> ().isMirror = !mirror.isMirror;
		}
	}

	public void attack(int weapon) 
	{
		if (weaponInUse == Weapon.NONE && !(inAttack || thrower.inThrowing)) {

			inAttack = true;
			weaponInUse = weapon;

			if (weapon == Weapon.SWORD) {
				if (GetComponent<Health> () && GetComponent<Health> ().IsMaxHealth () && !flySword) {
					// Flying sword
					AudioSource.PlayClipAtPoint(flyingSwordSound, Camera.main.transform.position);
					FlyingSwordAttack (mover.direction);
					StartCoroutine (CoolDown (0.5f));
				} else {
					// weaponInUse
					AudioSource.PlayClipAtPoint(swordSound, Camera.main.transform.position);
					SwordAttack (mover.direction);
					StartCoroutine (CoolDown (0.5f));
				}

			} else if (weapon == Weapon.BOW && inventory && inventory.numRupees > 0) {
				// BOW
				AudioSource.PlayClipAtPoint(flyingSwordSound, Camera.main.transform.position);
				inventory.numRupees = inventory.numRupees - 1;
				BowAttack (mover.direction);
				StartCoroutine (CoolDown (0.5f));
			} else if (weapon == Weapon.BOOMERANG) {
				// BOOMERANG
				thrower.ThrowBoomerang (mover.direction, AttackRegion.PLAYER);
				weaponInUse = Weapon.NONE;
				inAttack = false;
			} else if (weapon == Weapon.BOMB && inventory.numBombs > 0) { 
				inventory.numBombs -= 1;
				Instantiate (bomb, transform.position + Direction.GetVector3ByDirection (mover.direction), Quaternion.identity);
				StartCoroutine (CoolDown (0.2f));
			} else {
				weaponInUse = Weapon.NONE;
				inAttack = false;
			}
		}
	}

	IEnumerator CoolDown(float coolDownTime) {
		mover.movable = false;
		rb.velocity = Vector3.zero;
		yield return new WaitForSeconds (0.4f);
		weaponInUse = Weapon.NONE;
		yield return new WaitForSeconds (coolDownTime - 0.4f);
		mover.movable = true;
		inAttack = false;
	}
}
