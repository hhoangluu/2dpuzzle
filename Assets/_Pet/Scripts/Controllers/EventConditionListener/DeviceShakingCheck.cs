using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet {

    struct DeviceShakingEvent
    {
        public GameObject gameObject;

        public DeviceShakingEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
    public class DeviceShakingCheck : EventConditionChecker
    {
        [SerializeField]
        private float shakeDetectionThreshold;
        [SerializeField]
        private float minInterval;

        private float sqrShakeDetectionThreshold;
        private float timeLastShake;

        private bool checking = true;
        protected override void FireEvent()
        {
            EventCenter.TriggerEvent<DeviceShakingEvent>(new DeviceShakingEvent(gameObject));
        }

        // Start is called before the first frame update
        void Start()
        {
            sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, 2);
        }

        // Update is called once per frame
        void Update()
        {
            if (checking == true);
            if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold && Time.unscaledTime >= timeLastShake + minInterval)
            {
                Debug.Log("Shaking");
                ConditionTrue();
                checking = false;   
            }
        }
    }
}