using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public int direction;
	public bool isIdle;
	public bool movable;

	public float speed;
	public float modSpeed;

	public Vector2 axisInputs;

	Rigidbody rb;

	struct MoveCommand {
		public float value;
		public int diretion;

		public MoveCommand(float val, int dir) {
			value = val;
			diretion = dir;
		}
	}

	// Use this for initialization
	void Start () {
		direction = Direction.DOWN;
		isIdle = true;
		rb = GetComponent<Rigidbody> ();
		movable = true;
	}

	MoveCommand GetInputCommand() {
		isIdle = false;
		Vector2 input = axisInputs;
		if (input.x > 0.0f) {
			return new MoveCommand ( Mathf.Abs(input.x), Direction.RIGHT );
		} else if (input.x < 0.0f) {
			return new MoveCommand ( Mathf.Abs(input.x), Direction.LEFT );
		} else if (input.y > 0.0f) {
			return new MoveCommand ( Mathf.Abs(input.y), Direction.UP );
		} else if (input.y < 0.0f) {
			return new MoveCommand ( Mathf.Abs(input.y), Direction.DOWN );
		} else {
			isIdle = true;
			return new MoveCommand ( 0.0f, direction);
		}
	}

	void Update() {
		if (movable) {
			MoveCommand moveCommand = GetInputCommand ();
			// Debug.Log (Direction.DirectionString(moveCommand.diretion) + " " + moveCommand.value.ToString());
			if (Direction.IsSameAxis (moveCommand.diretion, direction)) {
				rb.velocity = Direction.GetVector2ByDirection (moveCommand.diretion) * moveCommand.value * speed;
				direction = moveCommand.diretion;
			} else { // Input direction differ from moving direction
				float distanceToGrid;
				float closestGrid;
				if (Direction.IsVertical (direction)) {
					closestGrid = Mathf.Round (transform.position.y * 2.0f) / 2.0f;
					distanceToGrid = Mathf.Abs (transform.position.y - closestGrid);
				} else {
					closestGrid = Mathf.Round (transform.position.x * 2.0f) / 2.0f;
					distanceToGrid = Mathf.Abs (transform.position.x - closestGrid);
				}

				// Align to grid
				// if distance to grid is small, just ignore
				if (distanceToGrid <= 0.1f) {
					transform.position = new Vector3 (
						Mathf.Round (transform.position.x * 2.0f) / 2.0f,
						Mathf.Round (transform.position.y * 2.0f) / 2.0f,
						0.0f
					);
					rb.velocity = Direction.GetVector2ByDirection (moveCommand.diretion) * moveCommand.value * speed;
					direction = moveCommand.diretion;
				} else {
					// Far from grid, move to grid
					if (Direction.IsVertical (direction)) {
						if (closestGrid > transform.position.y) {
							direction = Direction.UP;
							rb.velocity = Direction.GetVector2ByDirection (Direction.UP) * modSpeed;
						} else {
							direction = Direction.DOWN;
							rb.velocity = Direction.GetVector2ByDirection (Direction.DOWN) * modSpeed;
						}
					} else {
						if (closestGrid > transform.position.x) {
							direction = Direction.RIGHT;
							rb.velocity = Direction.GetVector2ByDirection (Direction.RIGHT) * modSpeed;
						} else {
							direction = Direction.LEFT;
							rb.velocity = Direction.GetVector2ByDirection (Direction.LEFT) * modSpeed;
						}	
					}
				}
			}
		}
	}

}

