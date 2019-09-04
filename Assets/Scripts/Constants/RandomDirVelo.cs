using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirVelo : MonoBehaviour {
	public static int backgroundLayer = 1 << 9;

	public static int GetPossibleNewDir(Transform _transform)
	{
		int[] directionBackUp = { 0, 1, 2, 3 };
		int backUpLength = 3;

		if (Physics.Raycast (_transform.position, _transform.right, 0.6f, backgroundLayer)) 
			-- backUpLength;

		if (Physics.Raycast (_transform.position, - _transform.right, 0.6f, backgroundLayer)) {
			directionBackUp [Direction.LEFT] = directionBackUp [backUpLength];
			-- backUpLength;
		}
		if (Physics.Raycast (_transform.position, - _transform.up, 0.6f, backgroundLayer)) {
			directionBackUp [Direction.DOWN] = directionBackUp [backUpLength];
			--backUpLength;
		}
		if (Physics.Raycast (_transform.position, _transform.up, 0.6f, backgroundLayer)) {
			directionBackUp [Direction.UP] = directionBackUp [backUpLength];
			--backUpLength;
		}

		int curDir = directionBackUp [Mathf.FloorToInt (Random.Range (0, backUpLength + 1))];
		return curDir;
	}

	public static Vector3 Round2NearestPosFullTile(Transform _transform)
	{
		return new Vector3 (Mathf.Round (_transform.position.x), Mathf.Round (_transform.position.y), _transform.position.z);
	}

	public static Vector3 Round2NearestPosHalfTile(Transform _transform)
	{
		return new Vector3 (Mathf.Round (_transform.position.x * 2.0f) / 2.0f, Mathf.Round (_transform.position.y * 2.0f) / 2.0f, _transform.position.z);
	}
}
