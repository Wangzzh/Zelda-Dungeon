using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour {

	public int[] weapon;
	public WeaponInventory weaponInventory;
	private Hashtable weaponWeHave;

	void Start() {
		weapon = new int[2];
		weapon [0] = Weapon.NONE;
		weapon [1] = Weapon.SWORD;
		weaponWeHave = new Hashtable ();
		if (weaponInventory) {
			weapon [0] = Weapon.BOW;
			weapon [1] = Weapon.NONE;
			ObtainWeapon (Weapon.BOW);
		}
	}

	public void SwitchWeapon(int weaponIndex)
	{
		if (weaponWeHave.ContainsKey (weaponIndex)) {
			weapon [0] = weaponIndex;
		}
	}

	public void ObtainWeapon(int weaponIndex)
	{
		if (weapon [0] == Weapon.NONE) {
			weapon [0] = weaponIndex;
		}
		if (!weaponWeHave.ContainsKey (weaponIndex)) {
			weaponWeHave.Add(weaponIndex, 1);
		}	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Item> ()) {
			ObtainWeapon(other.gameObject.GetComponent<Item> ().itemID);
			Destroy (other.gameObject);
		}
	}

	public void StartMirrorMode()
	{
		weapon [0] = Weapon.BOW;
		weapon[1] = Weapon.NONE;
		weaponWeHave.Clear();
		weaponWeHave.Add (Weapon.BOW, 1);
	}

}
