using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzBoost;
using UnityEngine.Events;
using System;

namespace _Pet
{
     struct ObjectTriggerEvent
    {
        public GameObject gameObject;

        public ObjectTriggerEvent (GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class DragToColliderCheck : EventConditionChecker
    {
        


        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (objectTarget)
            {
                if (collision.gameObject.GetInstanceID() == objectTarget.GetInstanceID())
                {
                    ConditionTrue();
                }
            }
        }

        protected override void FireEvent()
        {
            EventCenter.TriggerEvent(new ObjectTriggerEvent(gameObject));

        }
    }
}