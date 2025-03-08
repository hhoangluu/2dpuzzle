using EzBoost;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Pet
{
    [RequireComponent(typeof(Collider2D))]
    public class TouchToMove : MonoBehaviour
    {
        // Start is called before the first frame update
     

      

        [GUIColor(1, 1, 1)]
        [SerializeField]
        private Direction direction;

        [GUIColor(1, 1, 1)]
        [SerializeField]
        private float duration;

        [SerializeField]
        private float distance;
		private bool checking;
		private bool canMove;

        private float startTime;
        private Vector2 startPosition;
        private Vector2 targetPosition;
        private void OnMouseUpAsButton()
        {
            if (canMove == true) return;
            canMove = true;
            startPosition = transform.position;
            startTime = Time.time;
			switch (direction)
			{
				case Direction.Up:
                    targetPosition = (Vector2) transform.position + Vector2.up * distance;
                    direction = Direction.Down;
                    break;
				case Direction.Down:
                    targetPosition = (Vector2)transform.position + Vector2.down * distance;
                    direction = Direction.Up;
                    break;
				case Direction.Right:
                    targetPosition = (Vector2)transform.position + Vector2.right * distance;
                    direction = Direction.Left;
                    break;
				case Direction.Left:
                    targetPosition = (Vector2)transform.position + Vector2.left * distance;
                    direction = Direction.Right;
                    break;
				default:
					break;
			}
		}
   

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                if ((Time.time - startTime) / duration >= 1)
                {
                    transform.position = targetPosition;
                    canMove = false;
                }
                else
                {
                    transform.position = Vector2.Lerp(startPosition, targetPosition, (Time.time - startTime) / duration);
                }
            }
        }
    }
}