using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class DragDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public UnityEvent onDrag;
    public UnityEvent onMouseUpAsButton;

    private void Start()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("DƠN " + gameObject.name);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
    private void OnMouseUpAsButton()
    {
        onMouseUpAsButton?.Invoke();
    }
}