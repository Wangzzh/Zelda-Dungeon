using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	public Text text;
	public Player player;
	public Camera mainCamera;
	public AudioClip winAudio;

	bool playingAudio;

	// Use this for initialization
	void Start () {
		playingAudio = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (text.enabled && Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		if (player.GetComponent<GoalCollector> ().hasGoal && !playingAudio) {
			AudioSource.PlayClipAtPoint (winAudio, Camera.main.transform.position);
			Camera.main.GetComponent<AudioSource> ().Stop ();
			playingAudio = true;
		}
	}

	public void StartMirrorMode()
	{
		player.GetComponent<Attacker> ().inMirrorMode = true;
		player.GetComponent<WeaponInventory> ().StartMirrorMode ();
		player.GetComponent<Inventory> ().StartMirrorMode ();
	}
}
