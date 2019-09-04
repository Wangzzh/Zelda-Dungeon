using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpdate : MonoBehaviour {

	public Sprite swordSprite;
	public Sprite nullSprite;
	public Sprite bowSprite;
	public Sprite boomerangSprite;
	public Sprite bombSprite;

	public WeaponInventory weaponInventory;
	public int weaponId;

	Image image;

	void Start() {
		image = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		int weapon = weaponInventory.weapon [weaponId];
		if (weapon == Weapon.SWORD) {
			image.sprite = swordSprite;
		} else if (weapon == Weapon.BOW) {
			image.sprite = bowSprite;
		} else if (weapon == Weapon.BOOMERANG) {
			image.sprite = boomerangSprite;
		} else if (weapon == Weapon.BOMB) {
			image.sprite = bombSprite;
		} else {
			image.sprite = nullSprite;
		}
	}
}
