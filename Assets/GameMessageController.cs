using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMessageController : MonoBehaviour {
	public GameObject player;
	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<GoalCollector>().hasGoal) {
			text.text = "Victory! Press Enter to replay!";
			text.enabled = true;
			player.GetComponent<Mover> ().movable = false;
			player.GetComponent<Attacker> ().inAttack = true;
		} else if (player.GetComponent<Player> ().isDead) {
			text.text = "You are DEAD! Press Enter to replay!";
			text.enabled = true;
		}
	}
}
