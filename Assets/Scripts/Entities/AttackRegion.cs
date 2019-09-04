using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRegion : MonoBehaviour {

	public const int ENEMY = 0;
	public const int PLAYER = 1;

	public int attacker; // either PLAYER or ENEMY
	public int direction;

	public int damage;
	public bool isMirror;

	void Start()
	{
		if (GetComponentInParent<Enemy>() )
		{
			isMirror = GetComponentInParent<Mirror> ().isMirror;
		}
	}
}
