using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	
	public const int NONE = 0;
	public const int SWORD = 1;
	public const int BOW = 2; 
	public const int BOMB = 3;
	public const int BOOMERANG = 4;

	public const int FLYING_SWORD = 10;

	public static string WeaponString(int weapon) {
		if (weapon == NONE)
			return "none";
		else if (weapon == SWORD)
			return "sword";
		else if (weapon == BOW)
			return "bow";
		else if (weapon == BOMB)
			return "bomb";
		else if (weapon == BOOMERANG)
			return "boomerang";
		return "";
	}
}
