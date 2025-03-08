using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDeviceRotate : MonoBehaviour
{
    [SerializeField]
    float gravityScale = 1;
    private Vector2 gravity;
    // Start is called before the first frame update
    void Start()
    {
        gravity = new Vector2(0,-8.9f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Portrait:
                Physics2D.gravity = gravity * gravityScale* new Vector2(0, 1); 
                break;
            case DeviceOrientation.PortraitUpsideDown:
                Physics2D.gravity = gravity * gravityScale * new Vector2(0,-1);
                break;
            case DeviceOrientation.LandscapeLeft:
                Physics2D.gravity = gravity * gravityScale * new Vector2(-1, 0);
                break;
            case DeviceOrientation.LandscapeRight:
                Physics2D.gravity = gravity * gravityScale * new Vector2(1, 0);
                break;
            default:
                Physics2D.gravity = gravity * gravityScale * new Vector2(0, 1);
                break;
        }
    }
}
