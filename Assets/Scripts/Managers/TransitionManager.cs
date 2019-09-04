using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {

	public GameObject cameraObject;
	public GameObject levelObject;
	public GameObject playerObject;
	GameObject levelObjectClone;

	public int RoomWidth = 16;
	public int RoomHeight = 11;

	private Vector3 offset = new Vector3(1000f, 1000f, 1000f);

	void Start() {
		cameraObject = GameObject.Find ("Main Camera");
		levelObject = GameObject.Find ("Level");
		playerObject = GameObject.Find ("Player");
	}

	public void Transition(int direction) {
		if (direction == Direction.LEFT)
			StartCoroutine (TransitionLeftCoroutine ());
		else if (direction == Direction.RIGHT)
			StartCoroutine (TransitionRightCoroutine ());
		else if (direction == Direction.UP)
			StartCoroutine (TransitionUpCoroutine ());
		else if (direction == Direction.DOWN)
			StartCoroutine (TransitionDownCoroutine ());
		else if (direction == Direction.ENTER_BOWROOM) {
			StartCoroutine (TransitionEnterBowroomCoroutine ());
		} else if (direction == Direction.EXIT_BOWROOM) {
			StartCoroutine(TransitionExitBowroomCoroutine());
		}
	}

	IEnumerator TransitionEnterBowroomCoroutine() {
		DisablePlayer ();
		cameraObject.transform.position = new Vector3 (23.5f, 51f, -10f) + offset;
		yield return new WaitForSeconds (0.5f);
		playerObject.transform.position = new Vector3 (19f, 53.5f, 0f);
		EnablePlayer ();
		yield return null;
	}

	IEnumerator TransitionExitBowroomCoroutine() {
		DisablePlayer ();
		cameraObject.transform.position = new Vector3 (23.5f, 62f, -10f) + offset;
		yield return new WaitForSeconds (0.5f);
		playerObject.transform.position = new Vector3 (21f, 59f, 0f);
		EnablePlayer ();
		yield return null;
	}

	IEnumerator TransitionLeftCoroutine() {
		DisablePlayer ();
		//yield return new WaitForSeconds (0.5f);
		for (int i = 0; i < RoomWidth; i++) {
			Vector3 Position = new Vector3 (cameraObject.transform.position.x - 1f, cameraObject.transform.position.y, cameraObject.transform.position.z);
			cameraObject.transform.position = Position;
			yield return new WaitForSeconds (0.1f);
		}
		//yield return new WaitForSeconds (0.5f);
		playerObject.transform.position += new Vector3 (-2f, 0f, 0f);
		EnablePlayer ();
	}

	IEnumerator TransitionRightCoroutine() {
		DisablePlayer ();
		//yield return new WaitForSeconds (0.5f);
		for (int i = 0; i < RoomWidth; i++) {
			Vector3 Position = new Vector3 (cameraObject.transform.position.x + 1f, cameraObject.transform.position.y, cameraObject.transform.position.z);
			cameraObject.transform.position = Position;
			yield return new WaitForSeconds (0.1f);
		}
		//yield return new WaitForSeconds (0.5f);
		playerObject.transform.position += new Vector3 (2f, 0f, 0f);
		EnablePlayer ();
	}

	IEnumerator TransitionUpCoroutine() {
		DisablePlayer ();
		//yield return new WaitForSeconds (0.5f);
		for (int i = 0; i < RoomHeight; i++) {
			Vector3 Position = new Vector3 (cameraObject.transform.position.x, cameraObject.transform.position.y + 1f, cameraObject.transform.position.z);
			cameraObject.transform.position = Position;
			yield return new WaitForSeconds (0.1f);
		}
		//yield return new WaitForSeconds (0.5f);
		playerObject.transform.position += new Vector3 (0f, 1.5f, 0f);
		EnablePlayer ();
	}

	IEnumerator TransitionDownCoroutine() {
		DisablePlayer ();
		//yield return new WaitForSeconds (0.5f);
		for (int i = 0; i < RoomHeight; i++) {
			Vector3 Position = new Vector3 (cameraObject.transform.position.x, cameraObject.transform.position.y - 1f, cameraObject.transform.position.z);
			cameraObject.transform.position = Position;
			yield return new WaitForSeconds (0.1f);
		}
		//yield return new WaitForSeconds (0.5f);
		playerObject.transform.position += new Vector3 (0f, -2f, 0f);
		EnablePlayer ();
	}

	void DisablePlayer() {
		// Move camera and level out
		// so that we can pretend all entities are removed
		playerObject.GetComponent<Mover>().movable = false;
		cameraObject.transform.position += offset;
		levelObjectClone = Instantiate (levelObject, levelObject.transform.position + offset, Quaternion.identity);
	}

	void EnablePlayer() {
		playerObject.GetComponent<Mover>().movable = true;
		cameraObject.transform.position -= offset;
		Destroy(levelObjectClone);

		// TODO: Spawn all other entities
	}
}
