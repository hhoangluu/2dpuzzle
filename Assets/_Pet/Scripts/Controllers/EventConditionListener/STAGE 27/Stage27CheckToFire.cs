using System.Collections;
using System.Collections.Generic;
using EzBoost;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Pet
{
    public class Stage27CheckToFire : EventConditionChecker
    {
        private bool checkScale;
        [SerializeField]
        private Vector3 scaleTarget;
        private bool checking;
        [Title("Condition")]
        [SerializeField]
        bool higher = true;

        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new CheckerEvent(gameObject));
        }

        void Update()
        {

            if (objectTarget.transform.localScale.x >= scaleTarget.x && objectTarget.transform.localScale.y >= scaleTarget.y && objectTarget.transform.localScale.z >= scaleTarget.z && higher )
            {
                checkScale = true;
            }
            else if (objectTarget.transform.localScale.x <= scaleTarget.x && objectTarget.transform.localScale.y <= scaleTarget.y && objectTarget.transform.localScale.z <= scaleTarget.z && !higher)
            {
                checkScale = true;
            }
            else
            {
                checkScale = false; 
            }

        }

        public void ShootArrow()
        {
            if (checkScale)
            {
                ConditionTrue();
            }
         
        }
    }
}