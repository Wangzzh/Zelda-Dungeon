using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	Health health;
	bool isPlayer;
    public bool dropWeaponOrKey;
	public float dropGemProbability;

	public GameObject weaponOrKey;
	public Heart heart;
	public Rupee rupee;
	public GameObject disapperAnimation;

	// Use this for initialization

	void Start () {
		health = GetComponent<Health> ();
		if (GetComponent<Player> ())
			isPlayer = true;
		else
			isPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (health.health == 0) {
			if (isPlayer) {
				Player player = GetComponent<Player> ();
				player.isDead = true;
				Mover mover = GetComponent<Mover> ();
				mover.movable = false;
			} else {
				if (disapperAnimation) Instantiate (disapperAnimation, transform.position, Quaternion.identity);
				if (dropWeaponOrKey) Instantiate (weaponOrKey, transform.position, Quaternion.identity);
                else {
				    float randomValue = Random.value;
				    if (randomValue < dropGemProbability / 1.5f) {
					   Instantiate (rupee, transform.position, Quaternion.identity);	
				    } else if (dropGemProbability / 1.5f < randomValue && randomValue < dropGemProbability) {
					   Instantiate (heart, transform.position, Quaternion.identity);
				    }
                }
				Destroy (this.gameObject);
			}
		}
	}
}
