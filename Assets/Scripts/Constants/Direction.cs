using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour {
	
	public const int UP = 0;
	public const int DOWN = 1;
	public const int LEFT = 2;
	public const int RIGHT = 3;
	public const int NONE = 4;

	public const int UP_RIGHT = 5;
	public const int UP_LEFT = 6;

	public const int ENTER_BOWROOM = 100;
	public const int EXIT_BOWROOM = 101;

	public static bool IsVertical(int direction) {
		if (direction == UP || direction == DOWN)
			return true;
		else
			return false;
	}

	public static bool IsHorizontal(int direction) {
		if (direction == LEFT || direction == RIGHT)
			return true;
		else
			return false;
	}

	public static bool IsSameAxis(int directionA, int directionB) {
		if (IsVertical (directionA) && IsVertical (directionB))
			return true;
		if (IsHorizontal (directionB) && IsHorizontal (directionA))
			return true;
		return false;
	}

	public static bool IsOpposite(int directionA, int directionB) {
		return IsSameAxis (directionA, directionB) && (directionA != directionB);
	}

	public static Vector2 GetVector2ByDirection(int direction) {
		if (direction == UP) {
			return Vector2.up;
		} else if (direction == DOWN) {
			return Vector2.down;
		} else if (direction == LEFT) {
			return Vector2.left;
		} else if (direction == RIGHT) {
			return Vector2.right;
		} else {
			return Vector2.zero;
		}
	}


	public static Vector3 GetVector3ByDirection(int direction) {
		if (direction == UP) {
			return Vector3.up;
		} else if (direction == DOWN) {
			return Vector3.down;
		} else if (direction == LEFT) {
			return Vector3.left;
		} else if (direction == RIGHT) {
			return Vector3.right;
		} else {
			return Vector3.zero;
		}
	}

	public static string DirectionString(int direction) {
		if (direction == UP)
			return "up";
		if (direction == DOWN)
			return "down";
		if (direction == LEFT)
			return "left";
		if (direction == RIGHT)
			return "right";
		else
			return "none";
	}

}
