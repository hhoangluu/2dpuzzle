using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Pet
{
    [RequireComponent(typeof(Collider2D))]
    public class Rotate : MonoBehaviour
    {
        [SerializeField]
        private GameObject root;

        private float euler;
        private Vector3 offset;
        private bool hold = false;
        private void Start()
        {
            euler = Vector2.Angle(Vector2.left, Vector2.right);
        }
        private void OnMouseDown()
        {
            hold = true;
          
  
        }
        private void Update()
        {
            
            Debug.Log(euler);
            if (hold)
            {
                Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                var coefficient = (root.transform.position.y - curPosition.y)/(Mathf.Abs((root.transform.position.y - curPosition.y)));
                euler = Vector2.Angle(Vector2.left, new Vector2(curPosition.x - root.transform.position.x, curPosition.y - root.transform.position.y)) * coefficient;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, euler);
            }
        }

        private void OnMouseUp()
        {
            hold = false;
        }


    }
}