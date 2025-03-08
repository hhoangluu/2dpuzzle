using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet
{
    public class TwoFingerHoldCheck : EventConditionChecker
    {

        private int fingerHoldCount = 0;
        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new CheckerEvent(gameObject));
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddFingerCount()
        {
            fingerHoldCount++;
            Debug.Log("FINGERCOUNT "+ fingerHoldCount);
            if (fingerHoldCount == 2)
            {
                ConditionTrue();
            }
        }

        public void RemoveFingerCount()
        {
            fingerHoldCount--;
            Debug.Log("FINGERCOUNT " + fingerHoldCount);

        }
    }
}