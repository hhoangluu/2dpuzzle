using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzBoost;
using UnityEngine.Events;

namespace _Pet {
    
    [RequireComponent(typeof(Collider2D))]
    public class Touch : MonoBehaviour
    {
        public UnityEvent OnTouch;
        private Vector2 startPosition;
        // Start is called before the first frame update
        private void OnMouseUpAsButton()
        {
            if (Vector2.Distance(Input.mousePosition, startPosition) <0.3)
            {
                EventCenter.TriggerEvent(new CheckEvent(gameObject));
                Debug.Log(gameObject.name + " touch FIRE event");
                OnTouch?.Invoke();
            }
        }

        private void OnMouseDown()
        {
            startPosition = Input.mousePosition;
        }
       
    }
}
