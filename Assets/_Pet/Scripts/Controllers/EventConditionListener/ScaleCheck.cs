using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet
{
    public class ScaleCheck : EventConditionChecker
    {
        [SerializeField]
        private Vector3 scaleTarget;
        private bool checking;
        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new CheckerEvent(gameObject));
        }

        // Start is called before the first frame update
        void Start()
        {
            checking = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (checking)
            {
                if (objectTarget.transform.localScale.x >= scaleTarget.x && objectTarget.transform.localScale.y >= scaleTarget.y && objectTarget.transform.localScale.z >= scaleTarget.z)
                {
                    ConditionTrue();
                    checking = false;
                }
            }
        }
    }
}