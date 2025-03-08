using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Pet
{
    [RequireComponent(typeof(Collider2D))]
    public class ButtonMove : MonoBehaviour
    {
        [SerializeField]
        private Direction direction;
		private bool ishold = false;
		[SerializeField]
		public UnityEvent<Direction> OnHold;
		// Start is called before the first frame update
		private void OnMouseDown()
		{
			ishold = true;
		}

		private void OnMouseOver()
		{
			if (ishold && OnHold!= null)
			{
				OnHold?.Invoke(direction);
			}
		}

		private void OnMouseExit()
		{
			if (ishold && OnHold != null)
			{
				OnHold?.Invoke(Direction.None);
				ishold = false;
			}
		}

		private void OnMouseUp()
		{
			if (ishold && OnHold != null)
			{
				OnHold?.Invoke(Direction.None);
				ishold = false;
			}
		}
	}
}