using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour {

	private int lastHealth;
	private int lastMaxHealth;

	public Health health;
	//public GameObject HP;

	public Sprite fullHeart;
	public Sprite halfHeart;
	public Sprite emptyHeart;

	public float offset = 100.0f;

	public Image[] hearts;

	public AudioClip lowHealthSound;
	bool isLowHealth;

	// Use this for initialization
	void Start () {
		lastHealth = 0; 
		isLowHealth = false;
		StartCoroutine (LowHealthWarning ());
	}
	
	// Update is called once per frame
	void Update () {
		if (lastHealth != health.health || lastMaxHealth != health.maxHealth) {
			int numHearts = (int) Mathf.Ceil (health.maxHealth / 2.0f - 0.1f);
			//Debug.Log ("num hearts = " + numHearts.ToString ());
			for (int i = 0; i < hearts.Length; i++) {
				if (2 * i + 2 <= health.health) {
					hearts [i].enabled = true;
					hearts [i].sprite = fullHeart;
				} else if (2 * i + 1 == health.health) {
					hearts [i].enabled = true;
					hearts [i].sprite = halfHeart;
				} else if (2 * i >= health.maxHealth) {
					hearts [i].enabled = false;
				} else {
					hearts [i].enabled = true;
					hearts [i].sprite = emptyHeart;
				}
			}
			lastHealth = health.health;
			lastMaxHealth = health.maxHealth;

			isLowHealth = lastHealth <= 2;
		}
	}

	IEnumerator LowHealthWarning() {
		while (true) {
			if (isLowHealth) {
				AudioSource.PlayClipAtPoint (lowHealthSound, Camera.main.transform.position);
			}
			yield return new WaitForSeconds (0.5f);
		}
	}
}
