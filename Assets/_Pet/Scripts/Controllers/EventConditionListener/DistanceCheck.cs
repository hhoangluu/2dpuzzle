using System.Collections;
using System.Collections.Generic;
using EzBoost;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Pet
{
    struct DistanceCheckEvent
    {
        public GameObject gameObject;

        public DistanceCheckEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
    public class DistanceCheck : EventConditionChecker
    {
      
        [SerializeField]
        private float threshold = 1;

        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new DistanceCheckEvent(gameObject));

        }


        // Update is called once per frame
        void Update()
        {
            if (Vector2.Distance(objectTarget.transform.position, transform.position) <= threshold )
            {
                ConditionTrue();
            }
        }

        


    }
}