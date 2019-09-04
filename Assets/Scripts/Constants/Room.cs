using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	// Use this for initialization
	public static float roomWidth = 11f;
	public static float roomHeight = 6f;

	private static GameObject mainCamera;
	public static bool IsInRoom(Transform _transform)
	{
		mainCamera = Camera.main.gameObject;

		if (_transform.position.y - mainCamera.transform.position.y <=  -5.5f
			|| _transform.position.y - mainCamera.transform.position.y >= 2.5f
			|| Mathf.Abs (_transform.position.x - mainCamera.transform.position.x) >= 7f)
			return false;

		return true;
	}

	public static bool IsInRoom(Vector3 _position)
	{
		mainCamera = Camera.main.gameObject;

		if (_position.y - mainCamera.transform.position.y <=  -5.5f
			|| _position.y - mainCamera.transform.position.y >= 2.5f
			|| Mathf.Abs (_position.x - mainCamera.transform.position.x) >= 7f)
			return false;

		return true;
	}
}
