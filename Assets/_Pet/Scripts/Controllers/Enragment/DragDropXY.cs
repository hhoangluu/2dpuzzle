using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Pet
{
    [Serializable]
    public struct Axis
    {
        [SerializeField]
        public bool x;
        [SerializeField]
        public bool y;
    }
    [RequireComponent(typeof(Collider2D))]
    public class DragDropXY : MonoBehaviour
    {
     
        public Axis Axis;
       
        private Vector3 screenPoint;
        private Vector3 offset;
        private Vector3 origin;
        [SerializeField]
        private float maxDistance;


        private void Start()
        {

        }

        void OnMouseDown()
        {
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            if (Axis.x)
            {
                if (Mathf.Abs(curPosition.x - origin.x )<= maxDistance)
                {
                    transform.position = new Vector2(curPosition.x, transform.position.y);
                }
            }
            if (Axis.y)
            {
                if (Mathf.Abs(curPosition.y - origin.y )<= maxDistance)
                {
                    transform.position = new Vector2(transform.position.y, curPosition.y);
                }
            }
        }
    }
}