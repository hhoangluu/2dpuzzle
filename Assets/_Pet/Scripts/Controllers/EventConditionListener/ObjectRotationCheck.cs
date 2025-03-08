using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet
{
    struct RotationCheckEvent
    {
        public GameObject gameObject;

        public RotationCheckEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    public class ObjectRotationCheck : EventConditionChecker
    {
        [SerializeField]
        private Vector3 rotation;
        [SerializeField]
        private float threshold = 0.2f;
        private bool checking = true;
        protected override void FireEvent()
        {
            EventCenter.TriggerEvent<RotationCheckEvent>(new RotationCheckEvent(this.gameObject));
        }

        
        void Update()
        {
            if (checking)
            {
                if (checkRotationTrue(objectTarget.transform.eulerAngles, rotation))
                {
                    ConditionTrue();
                    checking = false;
                }
            }
        }

        private bool checkRotationTrue(Vector3 x , Vector3 y) 
        {
            if (Mathf.Abs(objectTarget.transform.rotation.eulerAngles.x  - rotation.x) > threshold)
            {
                Debug.Log("Rotation X Wrong");
                return false;
            }
            if (Mathf.Abs(objectTarget.transform.rotation.eulerAngles.y  - rotation.y)  > threshold)
            {
                Debug.Log("Rotation y Wrong");
                return false;
            }
            if (Mathf.Abs(objectTarget.transform.rotation.eulerAngles.z - rotation.z)  > threshold)
            {
                Debug.Log("Rotation z Wrong "+ (objectTarget.transform.rotation.eulerAngles.z  ));
                return false;
            }
            return true;
        }
    }
}