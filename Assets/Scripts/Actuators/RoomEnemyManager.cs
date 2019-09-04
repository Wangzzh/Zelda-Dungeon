using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemyManager : MonoBehaviour {
	public GameObject[] enemies;

	public GameObject spawnAni;

	private GameObject tempGameObj;
	private Vector3[] enemiesPos;
	private bool inRoom;
	// Use this for initialization
	void Start () {
		inRoom = false;
	
		enemiesPos = new Vector3[enemies.Length];
		for (int i = 0; i < enemies.Length; ++i)
			if (enemies [i]) {
				enemiesPos [i] = enemies [i].transform.position;
				enemies [i].SetActive (false);
			}
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if (Room.IsInRoom (transform.position + Vector3.right * 3f + Vector3.up * 2f) && !inRoom) {
			inRoom = true;	
			StartCoroutine (WakeUpEnemies());
		} else if (!Room.IsInRoom(transform.position + Vector3.right * 3f + Vector3.up * 2f)) {
			inRoom = false;
			for (int i = 0; i < enemies.Length; ++i)
				if (enemies [i])
					enemies [i].SetActive (false);			
		}
	}

	IEnumerator WakeUpEnemies()
	{
		for (int i = 0; i < enemies.Length; ++i)
			if (enemies [i])
				Instantiate (spawnAni, enemiesPos[i], Quaternion.identity);

		yield return new WaitForSeconds (0.5f);
		for (int i = 0; i < enemies.Length; ++i)
			if (enemies [i]) {
				enemies [i].SetActive (true);
				enemies [i].transform.position = enemiesPos [i];
			}
		
	}

	public bool AllEnemyDestroyed() 
	{
		for (int i = 0; i < enemies.Length; ++i)
			if (enemies [i] && enemies[i].GetComponent<Enemy>())
				return false;
		return true;
	}
}
