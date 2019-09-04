using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public int itemID;
	public AudioClip itemMusic;

	void Start()
	{
	}

	void OnDestroy()
	{
		if (itemMusic)
			AudioSource.PlayClipAtPoint (itemMusic, Camera.main.transform.position);
	}
}
