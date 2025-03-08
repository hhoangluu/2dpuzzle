using System.Collections;
using System.Collections.Generic;
using EzBoost;
using UnityEngine;

namespace _Pet
{
    struct TouchTriggerEvent
    {
        public GameObject gameObject;

        public TouchTriggerEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    public class TouchColliderCheck : EventConditionChecker, IEventListener<CheckEvent>
    {

        // Start is called before the first frame update
        void Start()
        {

        }
        private void OnEnable()
        {
            EventCenter.AddListener<CheckEvent>(this);
        }

        private void OnDisable()
        {
            EventCenter.RemoveListener<CheckEvent>(this);
        }

        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new TouchTriggerEvent(gameObject));
        }

        void IEventListener<CheckEvent>.OnEzEvent(CheckEvent eventType)
        {

            if (eventType.gameObject.GetInstanceID() == objectTarget.GetInstanceID())
            {
                Debug.Log(gameObject.name + " touch recive event");

                ConditionTrue();
            }
        }
    }


}