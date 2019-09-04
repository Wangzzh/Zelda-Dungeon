using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool isDead;

	Inventory inventory;
	Injurer injurer;
	WeaponInventory weaponInventory;
	Rigidbody rb;
	bool lastIsDead;

	public AudioClip deadSound;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		inventory = GetComponent<Inventory> ();
		injurer = GetComponent<Injurer> ();
		weaponInventory = GetComponent<WeaponInventory> ();
		isDead = false;
		lastIsDead = false;

	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) 
		{
			inventory.numKeys = 999;
			inventory.numBombs = 999;
			inventory.numRupees = 999;
			inventory.hasMap = true;
			inventory.hasCompass = true;
			injurer.invulnerable = true;
			weaponInventory.ObtainWeapon (Weapon.BOMB);
			weaponInventory.ObtainWeapon (Weapon.BOW);
			weaponInventory.ObtainWeapon (Weapon.BOOMERANG);
		}	

		if (!lastIsDead && isDead) {
			rb.velocity = Vector3.zero;
			lastIsDead = true;
			transform.position += Vector3.forward;
			AudioSource.PlayClipAtPoint (deadSound, Camera.main.transform.position);
		}
	}
}
