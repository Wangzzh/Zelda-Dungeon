using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMasterGenerator : MonoBehaviour {

	// Use this for initialization
	public GameObject wallMaster;

	private GameObject myWallMaster;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider others)
	{
		
		if (others.gameObject.GetComponent<Player> ()) {
			Debug.Log ("wall master coming!");
			Instantiate (wallMaster, transform.position - 1 * transform.right - 3 * transform.up, transform.rotation);
		}
	}
}
