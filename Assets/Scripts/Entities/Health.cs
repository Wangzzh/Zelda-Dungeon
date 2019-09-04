using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int health;
	public int maxHealth;

	public Health healthConnector;

	public void Heal(int healedHealth, bool isShared = true) {
		if (healthConnector && isShared)
			healthConnector.Heal (healedHealth, false);
		
		health += healedHealth;
		if (health >= maxHealth) {
			health = maxHealth;
		}
	}

	public void Hurt(int hurtHealth, bool isShared = true) {
		if (healthConnector && isShared)
			healthConnector.Hurt (hurtHealth, false);
		
		health -= hurtHealth;
		if (health <= 0) {
			health = 0;
		}
	}

	public bool IsMaxHealth() {
		return health == maxHealth;
	}

	public void addMaxHealth(int addedHealth) {
		maxHealth += addedHealth;
		health += addedHealth;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Heart> ()) {
			Heal (2);
		}
	}
}
