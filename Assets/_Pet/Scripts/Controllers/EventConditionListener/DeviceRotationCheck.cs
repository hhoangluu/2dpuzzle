using System.Collections;
using System.Collections.Generic;
using _Pet;
using EzBoost;
using UnityEngine;

struct DeviceRotationEvent
{
    public GameObject gameObject;
  
    public DeviceRotationEvent(GameObject gameObject)
    {
        this.gameObject = gameObject;
    
    }
}

public class DeviceRotationCheck : EventConditionChecker
{
    bool checking = true;
    [SerializeField]
    private DeviceOrientation deviceOrientation;

    protected override void FireEvent()
    {
        EventCenter.TriggerEvent(new DeviceRotationEvent(this.gameObject));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("UPDASSID " + Input.deviceOrientation.ToString());
        if (checking)
        {
            if (Input.deviceOrientation == deviceOrientation)
            {
                
                ConditionTrue();
                checking = false;
            }
        }
    }
}
