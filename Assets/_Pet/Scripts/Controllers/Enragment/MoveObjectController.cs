using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Pet {
	public enum Direction {
		Up,
		Down,
		Right,
		Left,
		None
	}
	public class MoveObjectController : MonoBehaviour
	{
		[SerializeField]
		private GameObject target;
		[SerializeField]
		private float interval_seconds;
		[SerializeField]
		private float step;
		private Direction moveDirection;
		private void Start()
		{
			moveDirection = Direction.None;
			StartCoroutine(CheckActionInterval());

		}
		public void Move(Direction direction)
		{
			switch (direction)
			{
				case Direction.Up:
					moveDirection = Direction.Up;
					break;
				case Direction.Down:
					moveDirection = Direction.Down;
					break;
				case Direction.Right:
					moveDirection = Direction.Right;
					break;
				case Direction.Left:
					moveDirection = Direction.Left;
					break;
				default:
					moveDirection = Direction.None;
					break;
			}
		
	
		}

		IEnumerator CheckActionInterval()
		{
			while (true)
			{
				Debug.Log("Move " + moveDirection.ToString());

				switch (moveDirection)
				{
					case Direction.Down:
						target.transform.position += Vector3.down * step;
						break;
					case Direction.Up:
						target.transform.position += Vector3.up * step;
						break;
					case Direction.Left:
						target.transform.position += Vector3.left * step;
						break;
					case Direction.Right:
						target.transform.position += Vector3.right * step;
						break;
					default:
						break;
				}
				yield return new WaitForSeconds(interval_seconds);
				if (GameManager.Instance.GameState != GameState.Playing && GameManager.Instance.GameState != GameState.Paused)
				{
					break;
				}
			}
		}
	}
}