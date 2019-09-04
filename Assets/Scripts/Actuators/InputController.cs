using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	Mover mover;
	WeaponInventory weaponInventory;
	Attacker attacker;

	private Mirror mirror;

	void Start() {
		mirror = GetComponent<Mirror> ();
		mover = GetComponent<Mover> ();
		weaponInventory = GetComponent<WeaponInventory> ();
		attacker = GetComponent<Attacker> ();
	}

	void Update () {
		
		// Send to mover
		if (!mirror.isMirror)
			mover.axisInputs = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		else {
			mover.axisInputs = new Vector2 (-Input.GetAxisRaw ("Horizontal"), -Input.GetAxisRaw ("Vertical"));
		}
		// Send to attacker
		if (Input.GetAxisRaw ("A") > 0.0f) {
			if (weaponInventory.weapon [0] != Weapon.NONE) {
				attacker.attack (weaponInventory.weapon [0]);
			}
		} else if (Input.GetAxisRaw ("B") > 0.0f) {
			if (weaponInventory.weapon [1] != Weapon.NONE) {
				attacker.attack (weaponInventory.weapon [1]);
			}
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			weaponInventory.SwitchWeapon (Weapon.BOMB);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			weaponInventory.SwitchWeapon (Weapon.BOOMERANG);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			weaponInventory.SwitchWeapon (Weapon.BOW);
		}
	}
}
