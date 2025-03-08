using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField]
    private float aspect;
    private float sizeCamera;
    [SerializeField]
    private Camera cameraObj;

    // Start is called before the first frame update
    void Start()
    {
        sizeCamera = cameraObj.orthographicSize;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Initialize();
    }
    private void Initialize()
    {
        aspect = (float)Screen.width / (float)Screen.height;
        //aspect = (float)Math.Round(aspect, 2);
        float size = sizeCamera;
        //if (aspect > 1.1f && aspect <= 1.5f)
        //{
        //    size = (sizeCamera + aspect + 0.5f) - .8f;
        //}
        //else if (aspect <= 1.1f)
        //{
        //    size = sizeCamera + aspect + 0.5f;
        //    size += 2;
        //}
        if (aspect < 1)
        {
            cameraObj.orthographicSize = 0.5625f / aspect * size;

        }
        else
        {
            cameraObj.orthographicSize = 1.78f / aspect * size;
        }
    }
}
