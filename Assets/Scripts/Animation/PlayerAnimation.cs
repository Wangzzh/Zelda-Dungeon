using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	Mover mover;
	Animator animator;
	Player player;
	Attacker attacker;
	GoalCollector goalCollector;
	int lastWeaponInUse;

	void Start() {
		mover = GetComponent<Mover> ();
		animator = GetComponent<Animator> ();
		attacker = GetComponent<Attacker> ();
		player = GetComponent<Player> ();
		lastWeaponInUse = Weapon.NONE;
		goalCollector = GetComponent<GoalCollector> ();
	}

	// Update is called once per frame
	void Update () {

		// reached goal
		animator.SetBool("hasGoal", goalCollector.hasGoal);

		// dead
		animator.SetBool("isDead", player.isDead);

		if (!player.isDead && !goalCollector.hasGoal) {

			// initiate attack
			if (attacker.weaponInUse != lastWeaponInUse) {
				animator.SetInteger ("weaponInUse", attacker.weaponInUse);
				animator.speed = 1.0f;
			}

			lastWeaponInUse = attacker.weaponInUse;

			// move
			animator.SetInteger ("direction", mover.direction);
			if (mover.isIdle && attacker.weaponInUse == Weapon.NONE) {
				animator.speed = 0.0f;
			} else {
				animator.speed = 1.0f;
			}
		
		} else {
			animator.speed = 1.0f;
		}
	}
}
