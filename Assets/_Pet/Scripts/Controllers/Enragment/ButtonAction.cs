using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Pet
{
	[RequireComponent(typeof(Collider2D))]
	public class ButtonAction : MonoBehaviour
	{
		
		private bool ishold = false;
		[SerializeField]
		public UnityEvent OnHold;
		[SerializeField]
		public UnityEvent OnPress;
		[SerializeField]
		public UnityEvent OnRelease;

		private int fingerId = -1;
		// Start is called before the first frame update

		private void Update()
		{
			for (int i = 0; i < Input.touchCount; ++i)
			{
				Debug.Log("Touch");
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					// Construct a ray from the current touch coordinates
					Debug.Log("Touchbegan " + i + " " + Input.GetTouch(i).position);
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
					RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

					if (hit)
					{

						Debug.Log("HIT " + hit.transform.name + " " + transform.name + " " + hit.transform.gameObject.GetInstanceID()+ " " + gameObject.GetInstanceID());
						if (hit.transform.gameObject.GetInstanceID() == gameObject.GetInstanceID())
						{
							Debug.Log("HIT " + hit.transform.name);
							OnPress?.Invoke();
							fingerId = Input.GetTouch(i).fingerId;
						}
					}
				}
				if (Input.GetTouch(i).phase == TouchPhase.Ended && fingerId == Input.GetTouch(i).fingerId)
				{
					OnRelease?.Invoke();
				}
			}
		}

    }
}